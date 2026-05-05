export type CursorPagedResponse<T> = {
  items: T[];
  nextCursor?: string;
  hasNextPage: boolean;
};
