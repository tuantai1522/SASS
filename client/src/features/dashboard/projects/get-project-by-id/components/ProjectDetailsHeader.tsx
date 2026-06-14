import { useSuspenseQuery } from "@tanstack/react-query";
import { getProjectSlug } from "../../utils";
import { getProjectByIdOptions } from "../get-project-by-id-options";

interface ProjectDetailsHeaderProps {
  projectId: string;
}

export function ProjectDetailsHeader({
  projectId,
}: ProjectDetailsHeaderProps) {
  const { data: project } = useSuspenseQuery(getProjectByIdOptions({ projectId }));

  return (
    <header className="border-b border-border/60 pb-4">
      <div className="space-y-1">
        <p className="text-xs font-medium uppercase tracking-[0.24em] text-muted-foreground">
          Project
        </p>
        <h1 className="cn-font-heading text-2xl leading-tight font-semibold text-foreground">
          {getProjectSlug(project.title, project.code)}
        </h1>
      </div>
    </header>
  );
}
