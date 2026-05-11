import { create } from "zustand";
import { subscribeWithSelector } from "zustand/middleware";
import { applicationHubClient } from "./application-hub-client";
import { realtimeEventHandlers } from "./event-handlers";
import type {
  RealtimeConnectionStatus,
  RealtimeEventMap,
  RealtimeEventName,
} from "./types";

type RealtimeState = {
  status: RealtimeConnectionStatus;
};

type RealtimeActions = {
  connect: () => Promise<void>;
  disconnect: () => Promise<void>;
  dispatchEvent: <TEventName extends RealtimeEventName>(
    eventName: TEventName,
    payload: RealtimeEventMap[TEventName],
  ) => void;
  setStatus: (status: RealtimeConnectionStatus) => void;
};

export type RealtimeStore = RealtimeState & RealtimeActions;

export const useRealtimeStore = create<RealtimeStore>()(
  subscribeWithSelector((set, get) => ({
    status: "idle",

    async connect() {
      const { status, dispatchEvent, setStatus } = get();

      if (
        status === "connected" ||
        status === "connecting" ||
        status === "reconnecting"
      ) {
        return;
      }

      setStatus("connecting");

      try {
        await applicationHubClient.start({
          dispatchEvent,
          onReconnecting: () => setStatus("reconnecting"),
          onReconnected: () => setStatus("connected"),
          onDisconnected: () => setStatus("disconnected"),
        });

        setStatus("connected");
      } catch {
        setStatus("disconnected");
      }
    },

    async disconnect() {
      await applicationHubClient.stop();
      set({ status: "idle" });
    },

    dispatchEvent(eventName, payload) {
      realtimeEventHandlers[eventName](payload);
    },

    setStatus(status) {
      set({ status });
    },
  })),
);
