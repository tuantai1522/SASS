import axios, { AxiosError, type InternalAxiosRequestConfig } from "axios";

import { useAuthStore } from "@/stores/auths/auth-store.ts";
import { renewAccessToken } from "@/api/auths/auth.ts";

const API_URL = import.meta.env.VITE_API_URL;

type RetryableRequestConfig = InternalAxiosRequestConfig & {
  _retry?: boolean;
};

export const apiClient = axios.create({
  baseURL: API_URL,
  withCredentials: true,
});

apiClient.interceptors.request.use((config) => {
  const accessToken = useAuthStore.getState().accessToken;

  if (accessToken) {
    config.headers.Authorization = `Bearer ${accessToken}`;
  }

  return config;
});

let isRefreshing = false;
let refreshQueue: Array<(token: string | null) => void> = [];

function resolveRefreshQueue(token: string | null) {
  refreshQueue.forEach((callback) => callback(token));
  refreshQueue = [];
}

apiClient.interceptors.response.use(
  (response) => response,

  async (error: AxiosError) => {
    const originalRequest = error.config as RetryableRequestConfig | undefined;

    if (!originalRequest) {
      return Promise.reject(error);
    }

    const isUnauthorized = error.response?.status === 401;

    if (!isUnauthorized || originalRequest._retry) {
      return Promise.reject(error);
    }

    originalRequest._retry = true;

    if (isRefreshing) {
      return new Promise((resolve, reject) => {
        refreshQueue.push((newAccessToken) => {
          if (!newAccessToken) {
            reject(error);
            return;
          }

          originalRequest.headers.Authorization = `Bearer ${newAccessToken}`;
          resolve(apiClient(originalRequest));
        });
      });
    }

    isRefreshing = true;

    try {
      const data = await renewAccessToken();

      useAuthStore.getState().setAuth({
        accessToken: data.token,
      });

      resolveRefreshQueue(data.token);

      originalRequest.headers.Authorization = `Bearer ${data.token}`;

      return apiClient(originalRequest);
    } catch (refreshError) {
      useAuthStore.getState().clearAuth();
      resolveRefreshQueue(null);

      return Promise.reject(refreshError);
    } finally {
      isRefreshing = false;
    }
  },
);
