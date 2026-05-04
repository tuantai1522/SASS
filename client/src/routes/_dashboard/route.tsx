import { createFileRoute, Outlet, redirect } from "@tanstack/react-router";
import { Sidebar } from "@/features/shared";

export const Route = createFileRoute("/_dashboard")({
  beforeLoad: ({ context }) => {
    if (context.auth.status !== "authenticated") {
      throw redirect({ to: "/" });
    }
  },
  component: RouteComponent,
});

function RouteComponent() {
  return (
    <div className="flex h-screen">
      <Sidebar />

      <main className="p-6">
        <Outlet />
      </main>
    </div>
  );
}
