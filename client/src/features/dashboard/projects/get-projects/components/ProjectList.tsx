import { Link } from "@tanstack/react-router";
import {
  buttonVariants,
  Collapsible,
  CollapsibleContent,
  CollapsibleTrigger,
  ScrollArea,
} from "@/features/shared";
import { getProjectsOptions } from "../get-projects-options.ts";
import { useSuspenseQuery } from "@tanstack/react-query";

import { ChevronDown, Hash } from "lucide-react";
import { cn } from "@/lib";
import { getProjectSlug } from "../../utils.ts";

export function ProjectList() {
  const { data: projects } = useSuspenseQuery(getProjectsOptions());

  return (
    <div className="flex h-full min-h-0 flex-col">
      <Collapsible defaultOpen className="flex min-h-0 flex-1 flex-col">
        <CollapsibleTrigger className="shrink-0 flex w-full items-center justify-between px-2 py-1 text-sm font-medium text-muted-foreground hover:text-accent-foreground">
          <span>Projects</span>
          <ChevronDown className="size-4 shrink-0 transition-transform duration-200" />
        </CollapsibleTrigger>

        <CollapsibleContent className="min-h-0 flex-1 overflow-hidden">
          <ScrollArea className="h-full w-full">
            <div className="space-y-0.5 py-1">
              {projects.map((project) => (
                <Link
                  key={project.id}
                  className={buttonVariants({
                    variant: "ghost",
                    className:
                      "w-full min-w-0 overflow-hidden justify-start px-2 py-1 text-muted-foreground hover:text-accent-foreground hover:bg-accent",
                  })}
                  to="/projects/$projectId"
                  params={{ projectId: project.id }}
                >
                  {({ isActive }) => (
                    <>
                      <Hash className="size-4 shrink-0" />
                      <span
                        className={cn(
                          "min-w-0 flex-1 truncate text-left",
                          isActive && "text-accent-foreground",
                        )}
                      >
                        {getProjectSlug(project.title, project.code)}
                      </span>
                    </>
                  )}
                </Link>
              ))}
            </div>
          </ScrollArea>
        </CollapsibleContent>
      </Collapsible>
    </div>
  );
}
