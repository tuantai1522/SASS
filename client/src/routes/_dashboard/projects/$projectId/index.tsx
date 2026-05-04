import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/_dashboard/projects/$projectId/")({
  component: ProjectPage,
});

function ProjectPage() {
  return <p>Project feature page placeholder for `/project/$id`.</p>;
}
