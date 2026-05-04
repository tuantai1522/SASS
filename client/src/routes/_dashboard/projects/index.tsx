import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/_dashboard/projects/")({
  component: ChatPage,
});

function ChatPage() {
  return <p>Project feature page placeholder for `/project`.</p>;
}
