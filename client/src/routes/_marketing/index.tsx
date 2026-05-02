import { createFileRoute } from "@tanstack/react-router";
import HeroSection from "@/routes/_marketing/_components/-hero-section.tsx";

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
