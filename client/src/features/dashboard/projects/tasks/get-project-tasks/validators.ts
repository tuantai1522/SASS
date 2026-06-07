import { PAGINATION } from "@/lib";
import { z } from "zod";
const getProjectTasksOrderByValues = ["dueDate", "title", "priority"] as const;

export const getProjectTasksOrderBySchema = z.enum(
  getProjectTasksOrderByValues,
);

export type GetProjectTasksOrderBy = z.infer<
  typeof getProjectTasksOrderBySchema
>;

export const getProjectTasksRequestSchema = z.object({
  page: z.coerce.number().int().min(1).default(1),
  pageSize: z.coerce.number().int().min(1).max(100).default(10),

  projectId: z.uuid("Invalid project id"),

  statusIds: z.array(z.uuid()).default([]),
  assigneeIds: z.array(z.uuid()).default([]),
  priorityIds: z.array(z.uuid()).default([]),
  typeIds: z.array(z.uuid()).default([]),

  search: z.string().optional(),

  orderBy: getProjectTasksOrderBySchema.default("dueDate"),
  order: z.enum(["Asc", "Desc"]).default("Desc"),
});

export const getProjectTasksDefaultParamsSchema =
  getProjectTasksRequestSchema.omit({
    projectId: true,
  });

export const defaultProjectTasksParams =
  getProjectTasksDefaultParamsSchema.parse({
    page: PAGINATION.DEFAULT_PAGE,
    pageSize: PAGINATION.DEFAULT_PAGE_SIZE,
    order: "Desc",
    orderBy: "dueDate",
    statusIds: [],
    assigneeIds: [],
    priorityIds: [],
    typeIds: [],
  });
