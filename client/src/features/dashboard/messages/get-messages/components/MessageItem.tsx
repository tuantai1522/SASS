import { cn, formatDate } from "@/lib";
import {
  Avatar,
  AvatarFallback,
  AvatarImage,
  SafeContent,
} from "@/features/shared";
import { BadgeCheck } from "lucide-react";
import type { GetMessagesResponse } from "../types";

interface MessageItemProps {
  message: GetMessagesResponse;
}
export function MessageItem({ message }: MessageItemProps) {
  const { content, createdAt, isMe, displayName, avatarUrl } = message;

  return (
    <div
      className={cn(
        "group relative flex space-x-3 rounded-lg p-1",
        isMe ? "bg-muted/30" : "hover:bg-muted/50",
      )}
    >
      <Avatar>
        <AvatarImage
          src={avatarUrl}
          className="object-cover"
          alt={"Avatar of " + displayName}
        />
        <AvatarFallback>
          {displayName?.slice(0, 2).toUpperCase()}
        </AvatarFallback>
      </Avatar>

      <div className="flex-1 space-y-1 min-w-0">
        <div className="flex items-center gap-x-2">
          <p className="font-medium leading-none">{displayName}</p>

          {isMe && <BadgeCheck className="size-3.5 text-primary" />}
          <p className="text-xs text-muted-foreground leading-none">
            {formatDate(createdAt)}
          </p>
        </div>

        <SafeContent
          className="max-w-none break-words text-sm leading-6 [&_ol]:list-decimal [&_ol]:pl-5 [&_p]:min-h-[1.25rem] [&_ul]:list-disc [&_ul]:pl-5"
          content={JSON.parse(content)}
        />
      </div>
    </div>
  );
}
