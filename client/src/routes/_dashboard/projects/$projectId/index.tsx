import {
  ProjectDetailsHeader,
  ProjectDetailsOverview,
  getProjectByIdOptions,
} from "@/features/dashboard/projects/get-project-by-id";
import { TasksTable } from "@/features/dashboard/projects/tasks/get-project-tasks";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/features/shared";
import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/_dashboard/projects/$projectId/")({
  component: ProjectPage,
  loader: async ({ context: { queryClient }, params }) => {
    await queryClient.prefetchQuery(
      getProjectByIdOptions({ projectId: params.projectId }),
    );
  },
});

function ProjectPage() {
  const { projectId } = Route.useParams();

  return (
    <div className="flex h-full min-h-0 w-full flex-1 flex-col gap-6 p-4">
      <ProjectDetailsHeader projectId={projectId} />

      <Tabs defaultValue="details" className="min-h-0 flex-1">
        <TabsList>
          <TabsTrigger value="details">Details</TabsTrigger>
          <TabsTrigger value="table">Table</TabsTrigger>

          {/* Todo: To add in the future */}
          {/* <TabsTrigger value="kanban">Kanban</TabsTrigger> */}
        </TabsList>

        <TabsContent value="details" className="pt-2">
          <ProjectDetailsOverview projectId={projectId} />
        </TabsContent>

        <TabsContent value="table" className="min-h-0 flex-1 pt-2">
          <div className="min-h-0">
            <TasksTable projectId={projectId} key={projectId} />
          </div>
        </TabsContent>
      </Tabs>
    </div>
  );
}
