import axios, { AxiosError } from "axios";
import { AppError, type ProblemDetailsResponse } from "./api-error.ts";

export function normalizeApiError(error: unknown): AppError {
  if (!axios.isAxiosError(error)) {
    return new AppError({
      status: 500,
      detail:
        "There is an error. Please check your network connection and try again.",
      title: "Network Error",
    });
  }

  const axiosError = error as AxiosError<ProblemDetailsResponse>;
  const response = axiosError.response;

  if (!response) {
    return new AppError({
      status: 500,
      detail:
        "There is an error. Please check your network connection and try again.",
      title: "Network Error",
    });
  }

  const status = response.status;
  const data = response.data;

  return new AppError({
    status,
    title: data.title ?? "Request Failed",
    detail:
      data.detail ??
      "There is an error. Please check your network connection and try again.",
    traceId: data?.traceId,
    errors: data?.errors,
  });
}
