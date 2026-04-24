import { Link } from '@tanstack/react-router'

const navItemClassName =
  'grid gap-1 rounded-[22px] bg-transparent p-4 text-(--color-text-muted) no-underline transition duration-150 ease-in-out hover:-translate-y-px focus-visible:outline-3 focus-visible:outline-(--color-accent) focus-visible:outline-offset-2'

const activeNavItemClassName = `${navItemClassName} bg-(--color-surface-strong) text-(--color-heading) shadow-(--shadow-sm)`

const navigationItems = [
  {
    to: '/projects',
    label: 'Projects',
    description: 'Roadmaps and owners',
  },
  { to: '/chat', label: 'Chat', description: 'Team conversations' },
] as const

export function SidebarNav() {
  return (
    <nav className="grid gap-3" aria-label="Primary navigation">
      {navigationItems.map((item) => (
        <Link
          key={item.to}
          to={item.to}
          activeProps={{ className: activeNavItemClassName }}
          inactiveProps={{ className: navItemClassName }}
        >
          <span className="font-bold">{item.label}</span>
          <span className="text-[0.92rem]">{item.description}</span>
        </Link>
      ))}
    </nav>
  )
}
