type Order = 'asc' | 'desc'

export type PaginationOptions = {
  page: number
  pageSize: number
  totalPages: number
}
export type PagedRequest = PaginationOptions & {
  order: Order
}

export type PagedResponse<T> = {
  items: T[]
  page: number
  pageSize: number
  totalItems: number
  totalPages: number
  hasPreviousPage: boolean
  hasNextPage: boolean
}
