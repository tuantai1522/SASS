import {
  Avatar,
  AvatarFallback,
  AvatarImage,
  Button,
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/features/shared";
import { useSuspenseQuery } from "@tanstack/react-query";
import { getMeOptions } from "@/features/auths/get-me";
import { SignOutButton } from "@/features/auths/sign-out";
import { ThemeToggleSidebar } from "@/features/dashboard/components/ThemeToogleSidebar.tsx";

export function UserNav() {
  const { data: me } = useSuspenseQuery(getMeOptions());

  return (
    <DropdownMenu>
      <DropdownMenuTrigger asChild>
        <Button
          aria-label={me.displayName}
          variant="outline"
          size="icon"
          className="size-12 rounded-xl hover:rounded-lg transition-all duration-200 bg-bacgkround/50 border-border/50 hover:bg-accent hover:text-accent-foreground"
        >
          <Avatar>
            <AvatarImage
              src={me.avatarUrl}
              className="object-cover"
              alt={"Avatar of " + me.displayName}
            />
            <AvatarFallback>
              {me.displayName.slice(0, 2).toUpperCase()}
            </AvatarFallback>
          </Avatar>
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent side="right" className="w-[200px]">
        <DropdownMenuLabel className="flex gap-2">
          <Avatar>
            <AvatarImage
              src={me.avatarUrl}
              className="object-cover"
              alt={"Avatar of " + me.displayName}
            />
            <AvatarFallback>
              {me.displayName.slice(0, 2).toUpperCase()}
            </AvatarFallback>
          </Avatar>
          <div className="grid flex-1 text-left text-sm leading-tight gap-1">
            <p className="truncate font-medium">{me.displayName}</p>
            <p className="text-muted-foreground truncate text-xs">{me.email}</p>
          </div>
        </DropdownMenuLabel>
        <ThemeToggleSidebar />
        <DropdownMenuSeparator />
        <DropdownMenuItem>
          <SignOutButton />
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>
  );
}
