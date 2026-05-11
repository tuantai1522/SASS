import {
  HubConnection,
  HubConnectionBuilder,
  HubConnectionState,
  LogLevel,
} from "@microsoft/signalr";
import { env } from "@/lib/env";
import { useAuthStore } from "@/features/auths/manage-token";
import type {
  MessageCreatedRealtimeEvent,
  RealtimeEventMap,
  RealtimeEventName,
} from "./types";
import { ApplicationEventNames } from "./constants";

type ApplicationHubHandlers = {
  dispatchEvent: <TEventName extends RealtimeEventName>(
    eventName: TEventName,
    payload: RealtimeEventMap[TEventName],
  ) => void;
  onReconnecting: () => void;
  onReconnected: () => void;
  onDisconnected: () => void;
};

const APPLICATION_HUB_PATH = "/hubs/application";

function getApplicationHubUrl() {
  return new URL(APPLICATION_HUB_PATH, env.baseApiUrl).toString();
}

function createConnection() {
  return new HubConnectionBuilder()
    .withUrl(getApplicationHubUrl(), {
      accessTokenFactory: async () => useAuthStore.getState().accessToken ?? "",
    })
    .withAutomaticReconnect()
    .configureLogging(LogLevel.Warning)
    .build();
}

export class ApplicationHubClient {
  private connection: HubConnection | null = null;

  async start(handlers: ApplicationHubHandlers) {
    if (
      this.connection &&
      (this.connection.state === HubConnectionState.Connected ||
        this.connection.state === HubConnectionState.Connecting ||
        this.connection.state === HubConnectionState.Reconnecting)
    ) {
      return;
    }

    const connection = createConnection();

    connection.on(
      ApplicationEventNames.MessageCreated,
      (payload: MessageCreatedRealtimeEvent) => {
        handlers.dispatchEvent(ApplicationEventNames.MessageCreated, payload);
      },
    );
    connection.onreconnecting(() => {
      handlers.onReconnecting();
      return Promise.resolve();
    });
    connection.onreconnected(() => {
      handlers.onReconnected();
      return Promise.resolve();
    });
    connection.onclose(() => {
      handlers.onDisconnected();
      this.connection = null;
      return Promise.resolve();
    });

    this.connection = connection;
    await connection.start();
  }

  async stop() {
    if (!this.connection) {
      return;
    }

    const connection = this.connection;
    this.connection = null;
    await connection.stop();
  }
}

export const applicationHubClient = new ApplicationHubClient();
