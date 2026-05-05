import { useEffect, type RefObject } from "react";

type UseInfiniteScrollOptions = {
  targetRef: RefObject<Element | null>;
  hasNextPage: boolean;
  isFetchingNextPage: boolean;
  fetchNextPage: () => void | Promise<unknown>;
  enabled?: boolean;
  rootMargin?: string;
  threshold?: number;
};

export function useInfiniteSidebarScroll({
  targetRef,
  hasNextPage,
  isFetchingNextPage,
  fetchNextPage,
  enabled = true,
  rootMargin = "120px",
  threshold = 0,
}: UseInfiniteScrollOptions) {
  useEffect(() => {
    const target = targetRef.current;

    if (!target) return;
    if (!enabled) return;

    const observer = new IntersectionObserver(
      (entries) => {
        const entry = entries[0];

        if (!entry?.isIntersecting) return;
        if (!hasNextPage) return;
        if (isFetchingNextPage) return;

        void fetchNextPage();
      },
      {
        rootMargin,
        threshold,
      },
    );

    observer.observe(target);

    return () => {
      observer.unobserve(target);
    };
  }, [
    targetRef,
    hasNextPage,
    isFetchingNextPage,
    fetchNextPage,
    enabled,
    rootMargin,
    threshold,
  ]);
}
