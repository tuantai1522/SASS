import { useSuspenseQuery } from "@tanstack/react-query";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/features/shared";
import { getProjectByIdOptions } from "../get-project-by-id-options";

interface ProjectDetailsOverviewProps {
  projectId: string;
}

export function ProjectDetailsOverview({
  projectId,
}: ProjectDetailsOverviewProps) {
  const { data: project } = useSuspenseQuery(
    getProjectByIdOptions({ projectId }),
  );

  const summaryItems = [
    {
      label: "Progress",
      value: `${project.progress}%`,
      description: "Overall completion based on finished tasks.",
    },
    {
      label: "Total tasks",
      value: String(project.totalTasks),
      description: "All tasks currently tracked in this project.",
    },
    {
      label: "Completed tasks",
      value: String(project.totalCompletedTasks),
      description: "Tasks that have been marked as done.",
    },
    {
      label: "Current role",
      value: project.currentUserRole,
      description: "Your permission level in this project.",
    },
  ];

  return (
    <div className="space-y-6">
      <Card className="border-none bg-muted/30 ring-1 ring-border/60">
        <CardHeader className="border-b border-border/60">
          <CardTitle>Overview</CardTitle>
        </CardHeader>
        <CardContent>
          <p className="max-w-3xl whitespace-pre-wrap text-sm leading-6 text-muted-foreground">
            {project.description || "No description available."}
          </p>
        </CardContent>
      </Card>

      <section
        aria-label="Project summary"
        className="grid gap-2 md:grid-cols-2 xl:grid-cols-2"
      >
        {summaryItems.map((item) => (
          <Card
            key={item.label}
            className="border-none bg-card/90 ring-1 ring-border/60"
          >
            <CardHeader>
              <CardTitle>{item.label}</CardTitle>
              <CardDescription>{item.description}</CardDescription>
            </CardHeader>
            <CardContent>
              <p className="cn-font-heading text-2xl font-semibold text-foreground">
                {item.value}
              </p>
            </CardContent>
          </Card>
        ))}
      </section>
    </div>
  );
}
