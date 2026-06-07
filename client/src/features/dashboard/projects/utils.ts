import {
  ArrowDownIcon,
  ArrowRightIcon,
  ArrowUpIcon,
  Bug,
  CheckCircle2,
  CircleHelp,
  FileText,
  ListTodo,
  MessageSquareText,
  Sparkles,
  Timer,
  TrendingUp,
  type LucideIcon,
} from "lucide-react";

export function getProjectSlug(title: string, code: string) {
  return `[${code}] - ${title}`;
}

type TaskMetaPresentation = {
  icon: LucideIcon;
  className: string;
};

export function getStatusMeta(
  key: string,
): TaskMetaPresentation | undefined {
  const statusMetaMap: Record<string, TaskMetaPresentation> = {
    Todo: {
      icon: CircleHelp,
      className: "text-muted-foreground",
    },
    InProgress: {
      icon: Timer,
      className: "text-blue-500",
    },
    Review: {
      icon: MessageSquareText,
      className: "text-yellow-500",
    },
    Done: {
      icon: CheckCircle2,
      className: "text-green-500",
    },
  };

  return statusMetaMap[key];
}

export function getPriorityMeta(
  key: string,
): TaskMetaPresentation | undefined {
  const priorityMetaMap: Record<string, TaskMetaPresentation> = {
    Low: {
      icon: ArrowDownIcon,
      className: "text-green-500",
    },
    Medium: {
      icon: ArrowRightIcon,
      className: "text-yellow-500",
    },
    High: {
      icon: ArrowUpIcon,
      className: "text-red-500",
    },
  };

  return priorityMetaMap[key];
}

export function getTypeMeta(key: string): TaskMetaPresentation | undefined {
  const taskTypeMetaMap: Record<string, TaskMetaPresentation> = {
    Feature: {
      icon: Sparkles,
      className: "text-blue-500",
    },
    Bug: {
      icon: Bug,
      className: "text-red-500",
    },
    Improvement: {
      icon: TrendingUp,
      className: "text-green-500",
    },
    Documentation: {
      icon: FileText,
      className: "text-slate-500",
    },
    Task: {
      icon: ListTodo,
      className: "text-yellow-500",
    },
  };

  return taskTypeMetaMap[key];
}
