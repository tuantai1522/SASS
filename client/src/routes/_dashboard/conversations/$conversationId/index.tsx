import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute(
  "/_dashboard/conversations/$conversationId/",
)({
  component: ConversationsPage,
});

function ConversationsPage() {
  const { conversationId } = Route.useParams();

  return <p>Conversation by id feature for {conversationId}</p>;
}
