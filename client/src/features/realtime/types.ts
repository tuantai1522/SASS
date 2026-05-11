import type { ApplicationEventNames } from "./constants";

export type RealtimeConnectionStatus =
  | "idle"
  | "connecting"
  | "connected"
  | "reconnecting"
  | "disconnected";

export type MessageCreatedRealtimeEvent = {
  conversationId: string;
  id: string;
  content: string;
  createdAt: number;
  senderId?: string;
  displayName?: string;
  avatarUrl?: string;
};

export type RealtimeEventMap = {
  [ApplicationEventNames.MessageCreated]: MessageCreatedRealtimeEvent;
};

export type RealtimeEventName =
  (typeof ApplicationEventNames)[keyof typeof ApplicationEventNames];
