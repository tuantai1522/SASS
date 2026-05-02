import { createFileRoute } from "@tanstack/react-router";
import SignInPage from "@/routes/_auth/_components/-sign-in-page.tsx";

export const Route = createFileRoute("/_auth/sign-in")({
  component: SignIn,
});

function SignIn() {
  return <SignInPage />;
}
