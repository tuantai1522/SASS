import { createFileRoute } from "@tanstack/react-router";
import { getConversationByIdParamsSchema } from "@/features/dashboard/conversations/get-conversation-by-id";
import { getConversationByIdOptions } from "@/features/dashboard/conversations/get-conversation-by-id";
import { ConversationHeader } from "@/features/dashboard/conversations/get-conversation-by-id";
import {
  MessageList,
  type ConversationMessage,
} from "@/features/dashboard/messages/get-messages";
import { CreateMessageInputForm } from "@/features/dashboard/messages/create-message";

const initialMessages: ConversationMessage[] = [
  {
    id: "1",
    content: "<p>Hello world</p>",
    createdAt: 1777954144,
    isMe: true,
    avatarUrl: "https://github.com/shadcn.png",
    userName: "tuantai1511",
  },
  {
    id: "2",
    content: "<p>I am working</p>",
    createdAt: 1777953979,
    isMe: true,
    avatarUrl: "https://github.com/shadcn.png",
    userName: "tuantai1511",
  },
  {
    id: "3",
    content: "<p>I&#39;m working too, can we meet later</p>",
    createdAt: 1777953978,
    isMe: false,
    avatarUrl: "https://github.com/evilrabbit.png",
    userName: "John Doe",
  },
  {
    id: "4",
    content: "<ul><li>Draft UI</li><li>Hook Tiptap into form</li></ul>",
    createdAt: 1778037443,
    isMe: true,
    avatarUrl: "https://github.com/shadcn.png",
    userName: "tuantai1511",
  },
];

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
        <MessageList messages={initialMessages} />
      </div>

      {/*  Input message form*/}
      <div className="border-t bg-background p-4">
        <CreateMessageInputForm conversationId={conversationId} />
      </div>
    </div>
  );
}
