import { Button } from '#/shared/ui/button'
import { ChipGroup } from '#/shared/ui/chip-group'

type PaginationControlsProps = {
  page: number
  totalPages: number
  onPageChange: (page: number) => void
}

export function PaginationControls({
  page,
  totalPages,
  onPageChange,
}: PaginationControlsProps) {
  if (totalPages <= 1) {
    return null
  }

  return (
    <div className="flex flex-wrap items-center justify-between gap-3">
      <Button
        type="button"
        variant="ghost"
        size="compact"
        disabled={page === 1}
        onClick={() => onPageChange(page - 1)}
      >
        Previous
      </Button>

      <ChipGroup className="items-center">
        {Array.from({ length: totalPages }, (_, index) => {
          const nextPage = index + 1

          return (
            <Button
              key={nextPage}
              type="button"
              variant="chip"
              active={nextPage === page}
              onClick={() => onPageChange(nextPage)}
            >
              {nextPage}
            </Button>
          )
        })}
      </ChipGroup>

      <Button
        type="button"
        variant="ghost"
        size="compact"
        disabled={page === totalPages}
        onClick={() => onPageChange(page + 1)}
      >
        Next
      </Button>
    </div>
  )
}
