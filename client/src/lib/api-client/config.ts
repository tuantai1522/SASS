import { env } from "../env.ts";

type AxiosRequestConfig = import("axios").AxiosRequestConfig;

const getBaseURL = (): string => {
  return env.baseApiUrl;
};

const defaultConfig: AxiosRequestConfig = {
  baseURL: getBaseURL(),
  withCredentials: true,
  timeout: 30000,
};

const axiosConfigs: Record<string, AxiosRequestConfig> = {
  default: defaultConfig,
  test: {
    baseURL: getBaseURL(),
    withCredentials: true,
    timeout: 10000,
  },
};

const getAxiosConfig = (): AxiosRequestConfig => {
  const nodeEnv: string = "development";

  return axiosConfigs[nodeEnv] ?? defaultConfig;
};

const axiosConfig = getAxiosConfig();

export default axiosConfig;
