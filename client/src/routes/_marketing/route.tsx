import { createFileRoute, Outlet } from "@tanstack/react-router";
import { HeroHeader } from "@/routes/_marketing/_components/-hero-header.tsx";

export const Route = createFileRoute("/_marketing")({
  component: MarketingLayout,
});

function MarketingLayout() {
  return (
    <>
      <HeroHeader />
      <Outlet />
    </>
  );
}
