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
  statusKey: string;
  statusColorToken: string;
  statusIconKey: string;

  priorityId: string;
  priorityName: string;
  priorityKey: string;
  priorityColorToken: string;
  priorityIconKey: string;

  typeId: string;
  typeName: string;
  typeKey: string;
  typeColorToken: string;
  typeIconKey: string;

  assigneeId?: string;
  assigneeName?: string;

  startDate?: Date;
  dueDate?: Date;
  createdAt: number;
};
