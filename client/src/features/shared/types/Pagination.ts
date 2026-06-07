export type CursorPagedResponse<T> = {
  items: T[];
  nextCursor?: string;
  hasNextPage: boolean;
};

export type PagedResponse<T> = {
  items: T[];
  page: number;
  pageSize: number;
  totalItems: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
};

export type PagedRequest = {
  page: number;
  pageSize: number;
  order: "asc" | "desc";
};
