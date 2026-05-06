import { useSuspenseQuery } from "@tanstack/react-query";
import { getConversationByIdOptions } from "../get-conversation-by-id-options";

interface ConversationHeaderProps {
  conversationId: string;
}
export function ConversationHeader({
  conversationId,
}: ConversationHeaderProps) {
  const { data: conversation } = useSuspenseQuery(
    getConversationByIdOptions({ conversationId: conversationId }),
  );

  return (
    <div className="flex items-center justify-between h-14 px-4 border-b">
      <h1 className="text-lg font-semibold"># {conversation.name}</h1>
    </div>
  );
}
