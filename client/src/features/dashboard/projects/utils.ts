import {
  ArrowDownIcon,
  ArrowRightIcon,
  ArrowUpIcon,
  CheckCircle2,
  CircleHelp,
  Timer,
  type LucideIcon,
  MessageSquareText,
  Sparkles,
  Bug,
  TrendingUp,
  FileText,
} from "lucide-react";

export function getProjectSlug(title: string, code: string) {
  return `[${code}] - ${title}`;
}

export function getStatusMeta(status: string) {
  const statusMeta: Record<string, { icon: LucideIcon; className: string }> = {
    Todo: {
      icon: CircleHelp,
      className: "text-muted-foreground",
    },
    "In-Progress": {
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

  return statusMeta[status];
}

export function getPriorityMeta(priority: string) {
  const priorityMeta: Record<string, { icon: LucideIcon; className: string }> =
    {
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

  return priorityMeta[priority];
}

export function getTypeMeta(type: string) {
  const taskTypeMeta: Record<string, { icon: LucideIcon; className: string }> =
    {
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
    };

  return taskTypeMeta[type];
}
