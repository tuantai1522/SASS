import { createFileRoute } from "@tanstack/react-router";
import { HeroSection } from "@/features/marketings/components";

export const Route = createFileRoute("/_marketing/")({
  component: MarketingIndex,
});

function MarketingIndex() {
  return (
    <>
      <HeroSection />
    </>
  );
}
