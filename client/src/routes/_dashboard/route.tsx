import { createFileRoute, Outlet, redirect } from "@tanstack/react-router";

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
    <div className="grid min-h-screen grid-cols-[240px_minmax(0,1fr)]">
      <aside className="border-r p-6">
        <p className="text-sm font-medium">Shared dashboard sidebar</p>
        <p className="mt-2 text-sm text-muted-foreground">
          Put common left navbar items here.
        </p>
      </aside>

      <main className="p-6">
        <p className="mb-6 text-sm text-muted-foreground">
          Main area changes based on the current feature route.
        </p>
        <Outlet />
      </main>
    </div>
  );
}
