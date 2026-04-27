import type { GetProjectsRequest } from '#/features/projects'

export const queryKeys = {
  auth: {
    all: ['auth'] as const,
    me: () => ['auth', 'me'] as const,
  },
  projects: {
    all: ['projects'] as const,
    list: (params: GetProjectsRequest) => ['projects', 'list', params] as const,
    detail: (projectId: string) => ['projects', 'detail', projectId] as const,
  },
  tasks: {
    all: ['tasks'] as const,
    byProject: (projectId: string) => ['tasks', 'project', projectId] as const,
  },
  chat: {
    all: ['chat'] as const,
    conversations: () => ['chat', 'conversations'] as const,
  },
}
