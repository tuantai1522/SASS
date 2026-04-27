import { Button } from '#/shared/ui/button'
import { ChipGroup } from '#/shared/ui/chip-group'

type PaginationControlsProps = {
  page: number
  totalPages: number
  onPageChange: (page: number) => void
  hasPreviousPage: boolean
  hasNextPage: boolean
}

export function PaginationControls({
  page,
  totalPages,
  onPageChange,
  hasPreviousPage,
  hasNextPage,
}: PaginationControlsProps) {
  const pages = Array.from({ length: totalPages }, (_, index) => index + 1)

  return (
    <div className="content-card flex flex-col gap-4 rounded-[24px] p-4 sm:flex-row sm:items-center sm:justify-between">
      <div>
        <p className="m-0 text-sm font-semibold text-(--color-heading)">
          Page {page} of {totalPages}
        </p>
      </div>

      <div className="flex flex-wrap items-center justify-end gap-3">
        <Button
          type="button"
          variant="ghost"
          size="compact"
          disabled={!hasPreviousPage}
          className="min-w-24 justify-center disabled:pointer-events-none disabled:opacity-45"
          onClick={() => onPageChange(page - 1)}
        >
          Previous
        </Button>

        <ChipGroup className="items-center justify-center">
          {pages.map((item) => {
            const isActive = item === page

            return (
              <Button
                key={item}
                type="button"
                variant="chip"
                active={isActive}
                aria-current={isActive ? 'page' : undefined}
                className="min-w-10 justify-center disabled:pointer-events-none"
                onClick={() => onPageChange(item)}
              >
                {item}
              </Button>
            )
          })}
        </ChipGroup>

        <Button
          type="button"
          variant="ghost"
          size="compact"
          disabled={!hasNextPage}
          className="min-w-24 justify-center disabled:pointer-events-none disabled:opacity-45"
          onClick={() => onPageChange(page + 1)}
        >
          Next
        </Button>
      </div>
    </div>
  )
}
