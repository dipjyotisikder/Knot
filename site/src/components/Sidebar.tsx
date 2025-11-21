import Link from "next/link";

const sections = [
    {
        label: "Overview",
        href: "#overview",
    },
    {
        label: "Installation",
        href: "#getting-started",
    },
    {
        label: "Quick Start",
        href: "#quick-start",
    },
    {
        label: "Sample Projects",
        href: "#sample-projects",
    },
    {
        label: "API Reference",
        href: "#api-reference",
    },
    {
        label: "Best Practices",
        href: "#best-practices",
    },
    {
        label: "Migration Guide",
        href: "#migration-guide",
    },
    {
        label: "Community & Contribution",
        href: "#community",
    },
];

export default function Sidebar() {
    return (
        <aside
            className="hidden md:flex flex-col w-64 h-screen bg-linear-to-b from-slate-50 via-white to-slate-100 dark:from-slate-900 dark:via-slate-800 dark:to-slate-900 border-r border-slate-200 dark:border-slate-700 backdrop-blur-sm"
            style={{
                position: "sticky",
                top: 0,
                alignSelf: "flex-start",
                maxHeight: "100vh",
                overflowY: "auto",
            }}
        >
            {/* Header */}
            <div className="p-6 border-b border-slate-200 dark:border-slate-700 bg-white/50 dark:bg-slate-900/50 backdrop-blur-sm">
                <div className="flex items-center gap-3">
                    <div className="w-8 h-8 bg-linear-to-r from-blue-500 to-purple-600 rounded-lg flex items-center justify-center">
                        <span className="text-white text-sm font-bold">K</span>
                    </div>
                    <div>
                        <h2 className="text-sm font-semibold text-slate-900 dark:text-white">
                            Documentation
                        </h2>
                        <p className="text-xs text-slate-500 dark:text-slate-400">
                            v1.0.0
                        </p>
                    </div>
                </div>
            </div>

            {/* Navigation */}
            <nav className="flex-1 p-4">
                <div className="space-y-1">
                    {sections.map((section, index) => (
                        <a
                            key={section.href}
                            href={section.href}
                            className="group relative flex items-center gap-3 px-3 py-2.5 rounded-lg text-sm font-medium text-slate-700 dark:text-slate-300 hover:text-slate-900 dark:hover:text-white transition-all duration-200 hover:bg-slate-100 dark:hover:bg-slate-800 hover:translate-x-1"
                        >
                            {/* Active indicator */}
                            <div className="absolute left-0 top-1/2 -translate-y-1/2 w-1 h-6 bg-linear-to-b from-blue-500 to-purple-600 rounded-r-full opacity-0 group-hover:opacity-100 transition-opacity duration-200"></div>

                            {/* Icon */}
                            <div className="w-8 h-8 bg-slate-200 dark:bg-slate-700 rounded-lg flex items-center justify-center group-hover:bg-blue-500 transition-colors duration-200">
                                <div className="w-2 h-2 bg-slate-400 dark:bg-slate-500 rounded-full group-hover:bg-white transition-colors duration-200"></div>
                            </div>

                            {/* Label */}
                            <span className="flex-1 truncate">
                                {section.label}
                            </span>

                            {/* Hover effect background */}
                            <div className="absolute inset-0 bg-linear-to-r from-blue-500/5 to-purple-500/5 rounded-lg opacity-0 group-hover:opacity-100 transition-opacity duration-200 -z-10"></div>
                        </a>
                    ))}
                </div>
            </nav>

            {/* Footer */}
            <div className="p-4 border-t border-slate-200 dark:border-slate-700 bg-white/50 dark:bg-slate-900/50 backdrop-blur-sm">
                <div className="flex items-center justify-between text-xs text-slate-500 dark:text-slate-400">
                    <span>© 2025 Knot</span>
                    <div className="flex items-center gap-1">
                        <div className="w-2 h-2 bg-green-500 rounded-full animate-pulse"></div>
                        <span>Online</span>
                    </div>
                </div>
            </div>
        </aside>
    );
}
