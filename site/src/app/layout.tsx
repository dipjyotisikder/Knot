import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "../styles/globals.css";
import KnotIcon from "@/components/KnotIcon";
import { type Viewport } from "next";
import Script from "next/script";

const geistSans = Geist({
    variable: "--font-geist-sans",
    subsets: ["latin"],
});

const geistMono = Geist_Mono({
    variable: "--font-geist-mono",
    subsets: ["latin"],
});

export const metadata: Metadata = {
    metadataBase: new URL("https://knot-docs.vercel.app/"),
    title: "Knot – Effortless .NET Object Mapping",
    description:
        "Effortless, type-safe object mapping for .NET. Lightweight, fast, and zero dependencies.",
    openGraph: {
        title: "Knot – Effortless .NET Object Mapping",
        description:
            "Effortless, type-safe object mapping for .NET. Lightweight, fast, and zero dependencies.",
        url: "https://knot.yourdomain.com/",
        siteName: "Knot",
        images: [
            {
                url: "/icon.ico",
                width: 64,
                height: 64,
                alt: "Knot Icon",
            },
        ],
        locale: "en_US",
        type: "website",
    },
    twitter: {
        card: "summary",
        title: "Knot – Effortless .NET Object Mapping",
        description:
            "Effortless, type-safe object mapping for .NET. Lightweight, fast, and zero dependencies.",
        images: ["/icon.ico"],
    },
    robots: {
        index: true,
        follow: true,
    },
    alternates: {
        canonical: "https://knot-docs.vercel.app/",
    },
};

export const viewport: Viewport = {
    themeColor: "#18181b",
    colorScheme: "light dark",
    width: "device-width",
    initialScale: 1,
    maximumScale: 5,
    viewportFit: "cover",
};

export default function RootLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <html lang="en">
            <head>
                {/* JSON-LD Structured Data for Organization */}
                <Script
                    id="jsonld-organization"
                    type="application/ld+json"
                    strategy="afterInteractive"
                >
                    {JSON.stringify({
                        "@context": "https://schema.org",
                        "@type": "Organization",
                        name: "Knot",
                        url: "https://knot-docs.vercel.app/",
                        logo: "/icon.ico",
                        sameAs: ["https://github.com/your-org/knot"],
                    })}
                </Script>
                {/* JSON-LD Structured Data for Website */}
                <Script
                    id="jsonld-website"
                    type="application/ld+json"
                    strategy="afterInteractive"
                >
                    {JSON.stringify({
                        "@context": "https://schema.org",
                        "@type": "WebSite",
                        name: "Knot Documentation",
                        url: "https://knot-docs.vercel.app/",
                        potentialAction: {
                            "@type": "SearchAction",
                            target: "https://knot-docs.vercel.app/documentation?q={search_term_string}",
                            "query-input": "required name=search_term_string",
                        },
                    })}
                </Script>
            </head>
            <body
                className={`${geistSans.variable} ${geistMono.variable} antialiased`}
            >
                <nav className="w-full bg-linear-to-r from-slate-50 via-white to-slate-50 dark:from-slate-900 dark:via-slate-800 dark:to-slate-900 border-b border-slate-200 dark:border-slate-700 backdrop-blur-sm bg-white/80 dark:bg-slate-900/80 sticky top-0 z-50">
                    <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
                        <div className="flex items-center justify-between h-16">
                            {/* Logo and Brand */}
                            <div className="flex items-center gap-3">
                                <div className="relative">
                                    <div className="absolute inset-0 bg-linear-to-r from-blue-500 to-purple-600 rounded-lg blur-lg opacity-30"></div>
                                    <KnotIcon
                                        size={32}
                                        className="relative z-10"
                                    />
                                </div>
                                <div className="hidden sm:block">
                                    <h1 className="text-xl font-bold bg-linear-to-r from-slate-900 to-slate-700 dark:from-white dark:to-slate-200 bg-clip-text text-transparent">
                                        Knot
                                    </h1>
                                    <p className="text-xs text-slate-500 dark:text-slate-400 -mt-1">
                                        .NET Object Mapping
                                    </p>
                                </div>
                            </div>

                            {/* Navigation Links */}
                            <div className="flex items-center gap-1 sm:gap-2">
                                <a
                                    href="/"
                                    className="group relative px-3 sm:px-4 py-2 rounded-lg text-sm sm:text-base font-medium text-slate-700 dark:text-slate-300 hover:text-slate-900 dark:hover:text-white transition-all duration-200 hover:bg-slate-100 dark:hover:bg-slate-800"
                                >
                                    <span className="relative z-10">Home</span>
                                    <div className="absolute inset-0 bg-linear-to-r from-blue-500/10 to-purple-500/10 rounded-lg opacity-0 group-hover:opacity-100 transition-opacity duration-200"></div>
                                </a>
                                <a
                                    href="/documentation"
                                    className="group relative px-3 sm:px-4 py-2 rounded-lg text-sm sm:text-base font-medium text-slate-700 dark:text-slate-300 hover:text-slate-900 dark:hover:text-white transition-all duration-200 hover:bg-slate-100 dark:hover:bg-slate-800"
                                >
                                    <span className="relative z-10">
                                        Documentation
                                    </span>
                                    <div className="absolute inset-0 bg-linear-to-r from-purple-500/10 to-pink-500/10 rounded-lg opacity-0 group-hover:opacity-100 transition-opacity duration-200"></div>
                                </a>
                            </div>

                            {/* GitHub Link */}
                            <div className="flex items-center gap-2">
                                <a
                                    href="https://github.com/dipjyotisikder/Knot"
                                    target="_blank"
                                    rel="noopener noreferrer"
                                    className="group inline-flex items-center gap-2 px-3 sm:px-4 py-2 rounded-lg bg-slate-900 dark:bg-white text-white dark:text-slate-900 font-medium text-sm hover:bg-slate-800 dark:hover:bg-slate-100 transition-all duration-200 hover:scale-105"
                                >
                                    <svg
                                        className="w-4 h-4"
                                        fill="currentColor"
                                        viewBox="0 0 24 24"
                                    >
                                        <path d="M12 0c-6.626 0-12 5.373-12 12 0 5.302 3.438 9.8 8.207 11.387.599.111.793-.261.793-.577v-2.234c-3.338.726-4.033-1.416-4.033-1.416-.546-1.387-1.333-1.756-1.333-1.756-1.089-.745.083-.729.083-.729 1.205.084 1.839 1.237 1.839 1.237 1.07 1.834 2.807 1.304 3.492.997.107-.775.418-1.305.762-1.604-2.665-.305-5.467-1.334-5.467-5.931 0-1.311.469-2.381 1.236-3.221-.124-.303-.535-1.524.117-3.176 0 0 1.008-.322 3.301 1.23.957-.266 1.983-.399 3.003-.404 1.02.005 2.047.138 3.006.404 2.291-1.552 3.297-1.23 3.297-1.23.653 1.653.242 2.874.118 3.176.77.84 1.235 1.911 1.235 3.221 0 4.609-2.807 5.624-5.479 5.921.43.372.823 1.102.823 2.222v3.293c0 .319.192.694.801.576 4.765-1.589 8.199-6.086 8.199-11.386 0-6.627-5.373-12-12-12z" />
                                    </svg>
                                    <span className="hidden sm:inline">
                                        GitHub
                                    </span>
                                </a>
                            </div>
                        </div>
                    </div>
                </nav>
                {children}
            </body>
        </html>
    );
}
