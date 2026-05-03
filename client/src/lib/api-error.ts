export type ProblemDetailsResponse = {
  title?: string;
  status?: number;
  detail?: string;
  traceId?: string;
  errors?: Record<string, string[]>;
};

export class AppError extends Error {
  status: number;
  title: string;
  detail: string;
  traceId?: string;
  errors?: Record<string, string[]>;

  constructor(params: {
    status: number;
    title: string;
    detail: string;
    traceId?: string;
    errors?: Record<string, string[]>;
  }) {
    super();
    this.status = params.status;
    this.title = params.title;
    this.detail = params.detail;
    this.traceId = params.traceId;
    this.errors = params.errors;
  }
}
