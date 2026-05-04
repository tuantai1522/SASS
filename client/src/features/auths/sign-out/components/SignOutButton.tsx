import { useNavigate } from "@tanstack/react-router";
import { useMutation } from "@tanstack/react-query";
import { useAuthStore } from "@/features/auths/manage-token";
import { normalizeApiError } from "@/lib/normalize-api-error.ts";
import { toast } from "sonner";
import { useEffect } from "react";
import { LogOutIcon } from "lucide-react";
import { signOutOptions } from "../sign-out-options.ts";
import { Spinner } from "@/features/shared";

export function SignOutButton() {
  const { clearAuth, status } = useAuthStore();

  const navigate = useNavigate();

  // This effect will listen to the auth status to change, if the user is already logged out, it will redirect to the "/" page
  useEffect(() => {
    if (status === "unauthenticated") {
      void navigate({ to: "/" });
    }
  }, [status, navigate]);

  const signOutMutation = useMutation({
    ...signOutOptions(),
    onSuccess: async () => {
      clearAuth();
    },
    onError: (error) => {
      const normalizedError = normalizeApiError(error);
      toast.error(normalizedError.detail, { position: "bottom-right" });
    },
  });

  function handleSignOut() {
    signOutMutation.mutate();
  }

  return (
    <div
      className={`flex flex-row flex-1 gap-2 items-center ${
        signOutMutation.isPending ? "justify-center" : ""
      }`}
      onClick={handleSignOut}
    >
      {signOutMutation.isPending ? (
        <Spinner />
      ) : (
        <>
          <LogOutIcon />
          Sign Out
        </>
      )}
    </div>
  );
}
