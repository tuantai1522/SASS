import { createFileRoute, Outlet } from "@tanstack/react-router";
import { HeroHeader } from "@/features/marketings/components";

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
