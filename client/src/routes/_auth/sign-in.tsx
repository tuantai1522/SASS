import { createFileRoute } from "@tanstack/react-router";
import { SignInPage } from "@/features/auths/sign-in";

export const Route = createFileRoute("/_auth/sign-in")({
  component: SignIn,
});

function SignIn() {
  return <SignInPage />;
}
