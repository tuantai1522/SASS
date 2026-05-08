import { createFileRoute } from "@tanstack/react-router";
import { getConversationByIdParamsSchema } from "@/features/dashboard/conversations/get-conversation-by-id";
import { getConversationByIdOptions } from "@/features/dashboard/conversations/get-conversation-by-id";
import { ConversationHeader } from "@/features/dashboard/conversations/get-conversation-by-id";
import { MessageList } from "@/features/dashboard/messages/get-messages";
import { CreateMessageInputForm } from "@/features/dashboard/messages/create-message";

export const Route = createFileRoute(
  "/_dashboard/conversations/$conversationId/",
)({
  component: ConversationPage,
  params: {
    parse: (params) => getConversationByIdParamsSchema.parse(params),
  },
  loader: async ({ context: { queryClient }, params }) => {
    await queryClient.prefetchQuery(
      getConversationByIdOptions({
        conversationId: params.conversationId,
      }),
    );
  },
});

function ConversationPage() {
  const { conversationId } = Route.useParams();

  return (
    <div className="flex flex-col flex-1">
      {/*Conversation Header*/}
      <ConversationHeader conversationId={conversationId} />

      {/* Scrollable messages area */}

      <div className="flex-1 overflow-hidden mb-4">
        <MessageList conversationId={conversationId} />
      </div>

      {/*  Input message form*/}
      <div className="border-t bg-background p-4">
        <CreateMessageInputForm conversationId={conversationId} />
      </div>
    </div>
  );
}
