import type z from "zod";
import type { getProjectTasksRequestSchema } from "./validators";

export type GetProjectTasksRequest = z.infer<
  typeof getProjectTasksRequestSchema
>;

export type GetProjectTasksResponse = {
  id: string;
  code: string;
  title: string;

  statusId: string;
  statusName: string;

  priorityId: string;
  priorityName: string;

  assigneeId?: string;
  assigneeName?: string;

  startDate?: Date;
  dueDate?: Date;
  createdAt: number;
};
