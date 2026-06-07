import type { ColumnDef } from "@tanstack/react-table";
import { Ellipsis, User } from "lucide-react";
import { DataTableColumnHeader } from "@/features/shared";
import { Badge } from "@/features/shared";
import { Button } from "@/features/shared";
import { Checkbox } from "@/features/shared";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/features/shared";
import type { GetProjectTasksResponse } from "../types";
import { formatUnixTimestampToDate } from "@/lib";
import { getPriorityMeta, getStatusMeta, getTypeMeta } from "../../../utils";

export function getTasksTableColumns(): ColumnDef<GetProjectTasksResponse>[] {
  return [
    {
      id: "select",
      header: ({ table }) => (
        <Checkbox
          aria-label="Select all"
          className="translate-y-0.5"
          checked={
            table.getIsAllPageRowsSelected() ||
            (table.getIsSomePageRowsSelected() && "indeterminate")
          }
          onCheckedChange={(value) => table.toggleAllPageRowsSelected(!!value)}
        />
      ),
      cell: ({ row }) => (
        <Checkbox
          aria-label="Select row"
          className="translate-y-0.5"
          checked={row.getIsSelected()}
          onCheckedChange={(value) => row.toggleSelected(!!value)}
        />
      ),
      enableHiding: false,
      enableSorting: false,
      size: 100,
    },
    {
      id: "code",
      accessorKey: "code",
      header: ({ column }) => (
        <DataTableColumnHeader column={column} label="Task" />
      ),
      cell: ({ row }) => <div className="w-20">{row.getValue("code")}</div>,
      enableSorting: false,
      enableHiding: false,
      size: 20,
    },
    {
      id: "title",
      accessorKey: "title",
      header: ({ column }) => (
        <DataTableColumnHeader column={column} label="Title" />
      ),
      cell: ({ row }) => {
        const typeMeta = getTypeMeta(row.original.typeKey);

        const TypeIcon = typeMeta?.icon;

        return (
          <div className="grid min-w-0 grid-cols-[auto_1fr] items-center gap-2">
            <Badge variant="outline" className="shrink-0">
              {TypeIcon ? <TypeIcon className={typeMeta.className} /> : null}
              {row.original.typeName}
            </Badge>

            <span className="min-w-0 truncate font-medium">
              {row.getValue("title")}
            </span>
          </div>
        );
      },
      enableColumnFilter: true,
      size: 1200,
    },
    {
      id: "statusName",
      accessorKey: "statusName",
      header: ({ column }) => (
        <DataTableColumnHeader column={column} label="Status" />
      ),
      cell: ({ row }) => {
        const statusMeta = getStatusMeta(row.original.statusKey);

        if (!statusMeta) return null;

        const StatusIcon = statusMeta.icon;

        return (
          <div className="grid min-w-0 grid-cols-[auto_1fr] items-center gap-2">
            <Badge variant="outline">
              <StatusIcon className={statusMeta.className} />
              <span>{row.original.statusName}</span>
            </Badge>
          </div>
        );
      },
      enableColumnFilter: true,
      size: 150,
    },
    {
      id: "priorityName",
      accessorKey: "priorityName",
      header: ({ column }) => (
        <DataTableColumnHeader column={column} label="Priority" />
      ),
      cell: ({ row }) => {
        const priorityMeta = getPriorityMeta(row.original.priorityKey);

        if (!priorityMeta) return null;

        const PriorityIcon = priorityMeta.icon;

        return (
          <div className="grid min-w-0 grid-cols-[auto_1fr] items-center gap-2">
            <Badge variant="outline" className="py-1 [&>svg]:size-3.5">
              <PriorityIcon className={priorityMeta.className} />
              <span>{row.original.priorityName}</span>
            </Badge>
          </div>
        );
      },
      enableColumnFilter: true,
      size: 20,
    },
    {
      id: "assigneeName",
      accessorKey: "assigneeName",
      header: ({ column }) => (
        <DataTableColumnHeader column={column} label="Assignee" />
      ),
      cell: ({ cell }) => {
        return (
          <Badge
            variant="outline"
            className="w-35 justify-start py-1 [&>svg]:size-3.5"
          >
            <User className="shrink-0" />

            <span className="min-w-0 truncate">{cell.getValue<string>()}</span>
          </Badge>
        );
      },
      enableColumnFilter: true,
      size: 150,
    },
    {
      id: "startDate",
      accessorKey: "startDate",
      header: ({ column }) => (
        <DataTableColumnHeader column={column} label="Start Date" />
      ),
      cell: ({ cell }) => cell.getValue<string>(),
      enableColumnFilter: true,
      size: 20,
    },
    {
      id: "dueDate",
      accessorKey: "dueDate",
      header: ({ column }) => (
        <DataTableColumnHeader column={column} label="Due Date" />
      ),
      cell: ({ cell }) => cell.getValue<string>(),
      enableColumnFilter: true,
      size: 20,
    },
    {
      id: "createdAt",
      accessorKey: "createdAt",
      header: ({ column }) => (
        <DataTableColumnHeader column={column} label="Created At" />
      ),
      cell: ({ cell }) => formatUnixTimestampToDate(cell.getValue<number>()),
      enableColumnFilter: true,
      size: 20,
    },
    {
      id: "actions",
      cell: function Cell() {
        return (
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button
                aria-label="Open menu"
                variant="ghost"
                className="flex size-8 p-0 data-[state=open]:bg-muted"
              >
                <Ellipsis className="size-4" aria-hidden="true" />
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end" className="w-40">
              <DropdownMenuItem>Copy</DropdownMenuItem>
              <DropdownMenuItem>Edit</DropdownMenuItem>
              <DropdownMenuSeparator />
              <DropdownMenuItem>Delete</DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        );
      },
      size: 20,
    },
  ];
}
