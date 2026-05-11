import { type ReactNode, useEffect } from "react";
import { useAuthStore } from "@/features/auths/manage-token";
import { useRealtimeStore } from "@/features/realtime";

type RealtimeProviderProps = {
  children: ReactNode;
};

export function RealtimeProvider({ children }: RealtimeProviderProps) {
  const status = useAuthStore((state) => state.status);
  const accessToken = useAuthStore((state) => state.accessToken);
  const connect = useRealtimeStore((state) => state.connect);
  const disconnect = useRealtimeStore((state) => state.disconnect);

  useEffect(() => {
    if (status === "authenticated" && accessToken) {
      void connect();
      return;
    }

    void disconnect();
  }, [accessToken, connect, disconnect, status]);

  return children;
}
