import { getTasksTableColumns } from "./TasksTableColumn";
import { DataTable, Spinner, useDataTable } from "@/features/shared";
import { useMemo } from "react";
import { getProjectTasksOptions } from "../get-project-tasks-options";
import { useQuery } from "@tanstack/react-query";
import { defaultProjectTasksParams } from "../validators";

interface TasksTableProps {
  projectId: string;
}

export function TasksTable({ projectId }: TasksTableProps) {
  const { data, isLoading } = useQuery(
    getProjectTasksOptions({
      ...defaultProjectTasksParams,
      projectId,
    }),
  );

  const columns = useMemo(() => getTasksTableColumns({}), []);

  const { table } = useDataTable({
    data: data?.items ?? [],
    columns,
    pageCount: data?.totalPages ?? 0,
    initialState: {
      sorting: [{ id: "dueDate", desc: true }],
    },
  });

  if (isLoading) {
    return (
      <div className="flex h-64 items-center justify-center">
        <Spinner className="size-6 animate-spin text-muted-foreground" />
      </div>
    );
  }

  return <DataTable table={table} />;
}
