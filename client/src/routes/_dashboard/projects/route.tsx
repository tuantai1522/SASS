import { createFileRoute, Link, Outlet } from "@tanstack/react-router";
import LogoIcon from "@/assets/logo.png";
import { CreateProjectButton } from "@/features/dashboard/projects/create-project";

export const Route = createFileRoute("/_dashboard/projects")({
  component: ProjectsPage,
});

function ProjectsPage() {
  return (
    <div className="flex h-full min-h-0 min-w-0">
      <div className="flex h-full w-80 flex-col bg-secondary border-r border-border">
        {/*Header*/}
        <div className="flex items-center px-4 h-12 border-b border-border">
          <Link to="/" aria-label="go home" className="mx-auto block w-fit">
            <img width={32} height={32} src={LogoIcon} alt="Logo" />
          </Link>
        </div>

        {/*Body*/}
        <div className="px-4 py-2">
          <CreateProjectButton />
        </div>
      </div>
      <div className="flex min-w-0 flex-1 flex-col h-full">
        <Outlet />
      </div>
    </div>
  );
}
