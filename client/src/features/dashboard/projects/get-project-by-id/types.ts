export type GetProjectByIdRequest = {
  projectId: string;
};

export type GetProjectByIdApiResponse = {
  id: string;
  code: string;
  title: string;
  description?: string | null;
  createdAt: number;
  role: string;
  progress: number;
  totalTasks: number;
  totalCompletedTasks: number;
};

export type GetProjectByIdResponse = {
  id: string;
  code: string;
  title: string;
  description?: string;
  createdAt: number;
  currentUserRole: string;
  progress: number;
  totalTasks: number;
  totalCompletedTasks: number;
};
