import {
  getCoreRowModel,
  useReactTable,
  type PaginationState,
  type RowSelectionState,
  type TableOptions,
  type Updater,
  type VisibilityState,
} from "@tanstack/react-table";
import { useCallback, useMemo, useState } from "react";

import { parseAsInteger, useQueryState, type UseQueryStateOptions } from "nuqs";

interface UseDataTableProps<TData> extends Omit<
  TableOptions<TData>,
  "getCoreRowModel"
> {}

const PAGE_KEY = "page";
const PAGE_SIZE_KEY = "pageSize";

export function useDataTable<TData>(props: UseDataTableProps<TData>) {
  const {
    columns,
    pageCount = -1,
    initialState,

    ...tableProps
  } = props;

  // Get all fields ignoing "parse" from options
  const queryStateOptions = useMemo<
    Omit<UseQueryStateOptions<string>, "parse">
  >(
    () => ({
      history: "replace", // Don't create new history entry
      scroll: false, // Don't scroll to top on query change
      shallow: true, // Just update URL, don't trigger data fetching
      throttleMs: 50, // Throttle time to update url, to avoid too many updates when user is typing or changing filters,
      debounceMs: 300, // After x seconds of inactivity, update the url
      clearOnDefault: false, // To keep default values in url
    }),
    [],
  );

  const [rowSelection, setRowSelection] = useState<RowSelectionState>(
    initialState?.rowSelection ?? {},
  );
  const [columnVisibility, setColumnVisibility] = useState<VisibilityState>(
    initialState?.columnVisibility ?? {},
  );

  const [page, setPage] = useQueryState(
    PAGE_KEY,
    parseAsInteger.withOptions(queryStateOptions).withDefault(1),
  );
  const [perPage, setPerPage] = useQueryState(
    PAGE_SIZE_KEY,
    parseAsInteger
      .withOptions(queryStateOptions)
      .withDefault(initialState?.pagination?.pageSize ?? 10),
  );

  const pagination: PaginationState = useMemo(() => {
    return {
      pageIndex: page - 1, // zero-based index -> one-based index
      pageSize: perPage,
    };
  }, [page, perPage]);

  const onPaginationChange = useCallback(
    (updaterOrValue: Updater<PaginationState>) => {
      if (typeof updaterOrValue === "function") {
        const newPagination = updaterOrValue(pagination);
        void setPage(newPagination.pageIndex + 1);
        void setPerPage(newPagination.pageSize);
      } else {
        void setPage(updaterOrValue.pageIndex + 1);
        void setPerPage(updaterOrValue.pageSize);
      }
    },
    [pagination, setPage, setPerPage],
  );

  const table = useReactTable({
    ...tableProps,
    columns,
    initialState,
    pageCount,
    state: {
      pagination,
      columnVisibility,
      rowSelection,
    },
    defaultColumn: {
      ...tableProps.defaultColumn,
      enableColumnFilter: false,
    },
    enableRowSelection: true,
    onRowSelectionChange: setRowSelection,
    onPaginationChange,
    onColumnVisibilityChange: setColumnVisibility,
    getCoreRowModel: getCoreRowModel(),
    manualPagination: true,
    manualSorting: true,
    manualFiltering: true,
    meta: {
      ...tableProps.meta,
      queryKeys: {
        page: PAGE_KEY,
        perPage: PAGE_SIZE_KEY,
      },
    },
  });

  return useMemo(() => ({ table }), [table]);
}
