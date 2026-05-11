import type { RealtimeEventMap, RealtimeEventName } from "../types";
import { handleMessageCreated } from "./message-created-handler";

export const realtimeEventHandlers = {
  MessageCreated: handleMessageCreated,
} satisfies {
  [TEventName in RealtimeEventName]: (
    payload: RealtimeEventMap[TEventName],
  ) => void;
};
