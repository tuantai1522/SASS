import { useCallback } from 'react'

import { useDebouncedInput, usePagination } from '#/shared/hooks'
import type { GetProjectsRequest } from './types.ts'
import { useQuery } from '@tanstack/react-query'
import { projectListOptions } from './project-list-options.ts'
import { ProjectList } from './project-list.tsx'
import {
  Button,
  ChipGroup,
  PageSection,
  PaginationControls,
  SectionHeader,
  TextInput,
  Toolbar,
} from '#/shared/ui'

type ProjectWorkspaceProps = {
  search: GetProjectsRequest
  updateSearch: (nextSearch: Partial<GetProjectsRequest>) => void
}

export function ProjectWorkspace({
  search,
  updateSearch,
}: ProjectWorkspaceProps) {
  const projectsQuery = useQuery(projectListOptions(search))

  const page = projectsQuery.data?.page ?? 0
  const pageSize = projectsQuery.data?.pageSize ?? 0
  const totalPages = projectsQuery.data?.totalPages ?? 0
  const projects = projectsQuery.data?.items ?? []

  const hasPreviousPage = projectsQuery.data?.hasPreviousPage ?? false
  const hasNextPage = projectsQuery.data?.hasNextPage ?? false

  const pagination = usePagination({
    page: page,
    pageSize: pageSize,
    totalPages,
  })

  const handleDebouncedSearchChange = useCallback(
    (value: string) => {
      updateSearch({
        search: value,
        page: 1,
      })
    },
    [updateSearch],
  )

  const debouncedSearch = useDebouncedInput({
    value: search.search,
    delay: 1000,
    onDebouncedChange: handleDebouncedSearchChange,
  })

  const handlePageChange = (nextPage: number) => {
    const targetPage = pagination.goToPage(nextPage)

    if (targetPage === search.page) {
      return
    }

    updateSearch({ page: targetPage })
  }

  const handleOrderByChange = (orderBy: GetProjectsRequest['orderBy']) => {
    updateSearch({
      orderBy,
      page: 1,
    })
  }

  const handleOrderChange = (order: GetProjectsRequest['order']) => {
    updateSearch({
      order,
      page: 1,
    })
  }

  return (
    <>
      <PageSection>
        <SectionHeader
          eyebrow="Project hub"
          title="Projects"
          code="Portfolio"
          description="Track active work, ownership, and progress from one shared project space."

          // Todo: To add button create project
          // actions={
          //   projects.length > 0 ? (
          //     <Link
          //       to="/projects/$projectId"
          //       params={{ projectId: projects[0].id }}
          //       className="primary-button inline-flex items-center justify-center"
          //     >
          //       Open workspace
          //     </Link>
          //   ) : null
          // }
        />

        <Toolbar
          primary={
            <TextInput
              type="search"
              value={debouncedSearch.value}
              placeholder="Search project title"
              onChange={(event) => debouncedSearch.setValue(event.target.value)}
            />
          }
          secondary={
            <ChipGroup>
              <Button
                type="button"
                variant="chip"
                active={search.orderBy === 'CreatedAt'}
                onClick={() => handleOrderByChange('CreatedAt')}
              >
                Created at
              </Button>
              <Button
                type="button"
                variant="chip"
                active={search.orderBy === 'Title'}
                onClick={() => handleOrderByChange('Title')}
              >
                Title
              </Button>
              <Button
                type="button"
                variant="chip"
                active={search.order === 'Desc'}
                onClick={() => handleOrderChange('Desc')}
              >
                Desc
              </Button>
              <Button
                type="button"
                variant="chip"
                active={search.order === 'Asc'}
                onClick={() => handleOrderChange('Asc')}
              >
                Asc
              </Button>
            </ChipGroup>
          }
        />

        {projectsQuery.isPending ? (
          <p className="support-copy">Loading your projects...</p>
        ) : projectsQuery.isError ? (
          <p className="support-copy">
            Unable to load projects right now. Try refreshing the page.
          </p>
        ) : projects.length === 0 ? (
          <p className="support-copy">
            No projects match the current query yet.
          </p>
        ) : (
          <>
            <ProjectList projects={projects} />

            <PaginationControls
              page={search.page}
              totalPages={totalPages}
              onPageChange={handlePageChange}
              hasPreviousPage={hasPreviousPage}
              hasNextPage={hasNextPage}
            />
          </>
        )}
      </PageSection>
    </>
  )
}
