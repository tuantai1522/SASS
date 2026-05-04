import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/_dashboard/chats/")({
  component: ChatPage,
});

function ChatPage() {
  return <p>Chat feature page placeholder for `/chat`.</p>;
}
