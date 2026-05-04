import { Link } from "@tanstack/react-router";
import { FolderKanban, MessageCircle } from "lucide-react";
import {
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger,
} from "@/features/shared";

const menuItems = [
  {
    label: "Chats",
    to: "/chats",
    icon: MessageCircle,
  },
  {
    label: "Projects",
    to: "/projects",
    icon: FolderKanban,
  },
];

export function Sidebar() {
  return (
    <aside className="flex h-screen w-16 flex-col items-center border-r bg-background p-2">
      <TooltipProvider delayDuration={100}>
        <nav className="flex flex-col gap-2 w-full items-center">
          {menuItems.map((item) => {
            const Icon = item.icon;

            return (
              <Tooltip key={item.to}>
                <TooltipTrigger asChild>
                  <Link
                    to={item.to}
                    aria-label={item.label}
                    className="flex h-10 w-full items-center justify-center rounded-lg text-muted-foreground transition hover:bg-accent hover:text-accent-foreground"
                    activeProps={{
                      className: "bg-accent text-accent-foreground",
                    }}
                  >
                    <Icon size={18} />
                  </Link>
                </TooltipTrigger>

                <TooltipContent side="right">{item.label}</TooltipContent>
              </Tooltip>
            );
          })}
        </nav>
      </TooltipProvider>
    </aside>
  );
}
