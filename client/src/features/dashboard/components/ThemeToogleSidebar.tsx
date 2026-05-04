import {
  DropdownMenuItem,
  DropdownMenuPortal,
  DropdownMenuSub,
  DropdownMenuSubContent,
  DropdownMenuSubTrigger,
  useTheme,
} from "@/features/shared";
import { CheckIcon, Monitor, Moon, PaletteIcon, Sun } from "lucide-react";

const themeItems = [
  {
    label: "Light",
    value: "light",
    icon: Sun,
  },
  {
    label: "Dark",
    value: "dark",
    icon: Moon,
  },
  {
    label: "System",
    value: "system",
    icon: Monitor,
  },
] as const;

export function ThemeToggleSidebar() {
  const { theme, setTheme } = useTheme();

  return (
    <DropdownMenuSub>
      <DropdownMenuSubTrigger>
        <PaletteIcon />
        Appearance
      </DropdownMenuSubTrigger>

      <DropdownMenuPortal>
        <DropdownMenuSubContent
          sideOffset={8}
          alignOffset={-60}
          className="w-[160px]"
        >
          {themeItems.map((item) => {
            const Icon = item.icon;
            const isActive = theme === item.value;

            return (
              <DropdownMenuItem
                key={item.value}
                onClick={() => setTheme(item.value)}
              >
                <Icon />
                {item.label}

                {isActive && <CheckIcon className="ml-auto size-4" />}
              </DropdownMenuItem>
            );
          })}
        </DropdownMenuSubContent>
      </DropdownMenuPortal>
    </DropdownMenuSub>
  );
}
