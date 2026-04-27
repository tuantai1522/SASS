export type GetProjectByIdResponse = {
  id: string
  code: string
  title: string
  description?: string
  createdAt: number
  role: string
  progress: number
  totalTasks: number
  totalCompletedTasks: number
}
