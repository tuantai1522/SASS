import { useCallback } from 'react'
import type { PaginationOptions } from '../types/pagination.ts'

type UsePaginationOptions = PaginationOptions & {
  totalPages: number
}

export function usePagination({
  page,
  pageSize,
  totalPages,
}: UsePaginationOptions) {
  const goToNextPage = useCallback(
    () => Math.min(page + 1, totalPages || page + 1),
    [page, totalPages],
  )

  const goToPreviousPage = useCallback(() => Math.max(page - 1, 1), [page])

  const goToPage = useCallback(
    (targetPage: number) => {
      if (targetPage < 1) {
        return 1
      }

      if (totalPages > 0) {
        return Math.min(targetPage, totalPages)
      }

      return targetPage
    },
    [totalPages],
  )

  return {
    page,
    pageSize,
    goToNextPage,
    goToPreviousPage,
    goToPage,
  }
}
