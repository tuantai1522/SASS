import { createFileRoute, Link, Outlet } from "@tanstack/react-router";
import { CreateConversationButton } from "@/features/dashboard/conversations/create-conversation";
import LogoIcon from "@/assets/logo.png";

export const Route = createFileRoute("/_dashboard/conversations")({
  component: ConversationsPage,
});

function ConversationsPage() {
  return (
    <div className="flex h-full min-h-0">
      <div className="flex h-full w-80 flex-col bg-secondary border-r border-border">
        {/*Header*/}
        <div className="flex items-center px-4 h-12 border-b border-border">
          <Link to="/" aria-label="go home" className="mx-auto block w-fit">
            <img width={32} height={32} src={LogoIcon} alt="Logo" />
          </Link>
        </div>

        {/*Body*/}
        <div className="px-4 py-2">
          <CreateConversationButton />
        </div>
      </div>
      <div className="min-w-0 flex-1">
        <Outlet />
      </div>
    </div>
  );
}
