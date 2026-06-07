import { TasksTable } from "@/features/dashboard/projects/tasks/get-project-tasks";
import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/_dashboard/projects/$projectId/")({
  component: ProjectPage,
});

function ProjectPage() {
  const { projectId } = Route.useParams();

  return (
    <div className="flex h-full min-h-0 w-full flex-1 flex-col p-4">
      {/*Task table*/}
      <TasksTable projectId={projectId} key={projectId} />
    </div>
  );
}
