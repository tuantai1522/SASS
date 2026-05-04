import { createFileRoute, Outlet, redirect } from "@tanstack/react-router";
import { Sidebar, UserNav } from "@/features/dashboard/components";
import { getMeOptions } from "@/features/auths/get-me";

export const Route = createFileRoute("/_dashboard")({
  beforeLoad: ({ context }) => {
    if (context.auth.status !== "authenticated") {
      throw redirect({ to: "/" });
    }
  },
  loader: async ({ context: { queryClient } }) => {
    await queryClient.prefetchQuery(getMeOptions());
  },
  component: RouteComponent,
});

function RouteComponent() {
  return (
    <>
      <div className="flex h-screen">
        <aside className="flex h-screen w-16 flex-col items-center justify-between border-r bg-background p-2">
          <Sidebar />
          <UserNav />
        </aside>

        <main className="p-6">
          <Outlet />
        </main>
      </div>
    </>
  );
}
