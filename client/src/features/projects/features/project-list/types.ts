import { z } from 'zod'

export const defaultProjectsSearch = {
  page: 1,
  pageSize: 6,
  order: 'Desc',
  orderBy: 'CreatedAt',
  search: '',
} as const

export const GetProjectsRequestSchema = z.object({
  page: z.coerce.number().int().min(1).catch(defaultProjectsSearch.page),
  pageSize: z.coerce
    .number()
    .int()
    .min(1)
    .max(100)
    .catch(defaultProjectsSearch.pageSize),
  order: z.enum(['Asc', 'Desc']).catch(defaultProjectsSearch.order),
  orderBy: z.enum(['CreatedAt', 'Title']).catch(defaultProjectsSearch.orderBy),
  search: z.string().trim().catch(defaultProjectsSearch.search),
})

export type GetProjectsRequest = z.infer<typeof GetProjectsRequestSchema>

export type GetProjectsItemResponse = {
  id: string
  code: string
  title: string
  description?: string
  createdAt: number
  role: 'leader' | 'owner'
  progress: number
}
