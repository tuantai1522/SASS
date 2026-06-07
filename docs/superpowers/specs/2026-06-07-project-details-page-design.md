# Project Details Page Design

## Summary

Build out the `/projects/$projectId` page so it becomes a project workspace shell instead of a tasks-only screen.

The page will:

- Render a compact project header with key summary fields.
- Provide two tabs:
  - `Project details`
  - `Project tasks`
- Reuse the existing project tasks table without changing its behavior.

The backend already exposes `GetProjectById`, so the main implementation work is wiring that response into the client and shaping the page layout around it.

## Goals

- Show essential project information immediately when a project page opens.
- Keep the existing tasks experience intact.
- Create a clear page structure that can later absorb more project-specific sections such as files.

## Non-Goals

- No changes to project task fetching, filtering, sorting, or table behavior.
- No new project files feature in this scope.
- No test work in this scope.

## Existing Context

- The current route at `client/src/routes/_dashboard/projects/$projectId/index.tsx` renders only `TasksTable`.
- The client already has a pattern for project list data and project tasks data using TanStack Query.
- The backend already has `GetProjectByIdEndpoint`, `GetProjectByIdQuery`, `GetProjectByIdQueryHandler`, and `GetProjectByIdResponse`.

## Proposed UX

### Header

The top of the page should show the project identity and core status at a glance:

- Project code and title
- Description if present
- Current user role in the project
- Progress percentage
- Total tasks
- Completed tasks
- Created date

This header should stay simple and informational, not action-heavy.

### Tabs

The page body should use two tabs:

#### Project details

This tab is the project overview tab and will show:

- Full description or an empty-state fallback
- Summary cards or compact stats for:
  - progress
  - total tasks
  - completed tasks
  - current user role
  - created date

The layout should intentionally leave room for future expansion, especially a files section the user plans to add later.

#### Project tasks

This tab will render the existing `TasksTable` using the current `projectId` route param.

No task API, task table, or task metadata behavior should be changed as part of this feature.

## API Design

Use the existing `GET /projects/{projectId}` endpoint as the page-level details query.

### Response shape

The current response is already close to what the page needs. It is acceptable to rename fields for clearer client semantics if that improves readability, especially:

- `Role` -> `CurrentUserRole`

Recommended logical response shape on the client:

```ts
type GetProjectByIdResponse = {
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
```

If backend naming remains unchanged, the client can adapt to the existing field names instead of forcing an API rename. The implementation should prefer the smallest safe change.

## Frontend Structure

Add a dedicated client feature for project details fetching, following existing conventions:

- `api.ts`
- `types.ts`
- `get-project-by-id-options.ts` or equivalent
- `index.ts`

Add a new query key:

- `queryKeys.projects.detail(projectId)`

Update the project route so it:

1. Reads `projectId` from params
2. Fetches project details
3. Renders the project header
4. Renders the two-tab layout
5. Mounts `TasksTable` inside the `Project tasks` tab

## Error Handling

- Reuse existing query loading states for a clean initial render.
- If details are loading, show a lightweight page-level loading state.
- If the details query fails, rely on the app's existing query/error behavior rather than inventing a new error framework for this page.

## Testing Strategy

No testing work is included in this scope by explicit user direction.

## Implementation Notes

- Preserve the current project tasks feature as-is.
- Prefer adapting the UI to the existing backend response over expanding backend scope unless a small naming cleanup materially improves maintainability.
- Keep the tab and header UI straightforward so future additions such as files can slot into the details tab without restructuring the page.
