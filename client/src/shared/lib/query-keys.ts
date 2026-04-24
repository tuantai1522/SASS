import type { ProjectListSearch } from '#/features/projects/types'

export const queryKeys = {
  auth: {
    all: ['auth'] as const,
    me: () => ['auth', 'me'] as const,
  },
  projects: {
    all: ['projects'] as const,
    list: (search: ProjectListSearch) => ['projects', 'list', search] as const,
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
