import KnotIcon from "@/components/KnotIcon";

export default function Home() {
    return (
        <div className="min-h-screen bg-linear-to-br from-slate-50 via-white to-slate-100 dark:from-slate-900 dark:via-slate-800 dark:to-slate-900">
            {/* Hero Section */}
            <section className="relative overflow-hidden">
                <div className="absolute inset-0 bg-linear-to-r from-blue-600/10 via-purple-600/5 to-pink-600/10"></div>
                <div className="relative max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-24 sm:py-32">
                    <div className="text-center">
                        <div className="flex justify-center mb-8">
                            <div className="relative">
                                <div className="absolute inset-0 bg-linear-to-r from-blue-500 to-purple-600 rounded-full blur-xl opacity-30"></div>
                                <KnotIcon size={80} className="relative z-10" />
                            </div>
                        </div>

                        <h1 className="text-6xl sm:text-7xl lg:text-8xl font-black bg-linear-to-r from-slate-900 via-slate-800 to-slate-900 dark:from-white dark:via-slate-200 dark:to-white bg-clip-text text-transparent mb-6">
                            Knot
                        </h1>

                        <p className="text-xl sm:text-2xl text-slate-600 dark:text-slate-300 mb-8 max-w-3xl mx-auto leading-relaxed">
                            Effortless, type-safe object mapping for .NET
                            developers.
                            <span className="block mt-2 text-lg sm:text-xl font-medium text-slate-700 dark:text-slate-200">
                                Lightweight • Fast • Zero Dependencies
                            </span>
                        </p>

                        <div className="flex flex-wrap justify-center gap-3 mb-12">
                            <span className="px-4 py-2 bg-white/80 dark:bg-slate-800/80 backdrop-blur-sm border border-slate-200 dark:border-slate-700 rounded-full text-sm font-medium text-slate-700 dark:text-slate-300 shadow-sm">
                                Convention-based Mapping
                            </span>
                            <span className="px-4 py-2 bg-white/80 dark:bg-slate-800/80 backdrop-blur-sm border border-slate-200 dark:border-slate-700 rounded-full text-sm font-medium text-slate-700 dark:text-slate-300 shadow-sm">
                                Custom Profiles
                            </span>
                            <span className="px-4 py-2 bg-white/80 dark:bg-slate-800/80 backdrop-blur-sm border border-slate-200 dark:border-slate-700 rounded-full text-sm font-medium text-slate-700 dark:text-slate-300 shadow-sm">
                                High Performance
                            </span>
                            <span className="px-4 py-2 bg-white/80 dark:bg-slate-800/80 backdrop-blur-sm border border-slate-200 dark:border-slate-700 rounded-full text-sm font-medium text-slate-700 dark:text-slate-300 shadow-sm">
                                .NET Standard 2.0+
                            </span>
                        </div>

                        <div className="flex flex-col sm:flex-row gap-4 justify-center mb-16">
                            <a
                                href="/documentation"
                                className="group relative px-8 py-4 bg-linear-to-r from-slate-900 to-slate-800 dark:from-slate-100 dark:to-slate-200 text-white dark:text-slate-900 font-semibold rounded-full shadow-lg hover:shadow-xl transform hover:scale-105 transition-all duration-200"
                            >
                                <span className="relative z-10">
                                    Read Documentation
                                </span>
                                <div className="absolute inset-0 bg-linear-to-r from-blue-600 to-purple-600 rounded-full opacity-0 group-hover:opacity-100 transition-opacity duration-200"></div>
                            </a>
                            <a
                                href="https://github.com/dipjyotisikder/Knot"
                                target="_blank"
                                rel="noopener noreferrer"
                                className="px-8 py-4 border-2 border-slate-300 dark:border-slate-600 text-slate-700 dark:text-slate-300 font-semibold rounded-full hover:bg-slate-50 dark:hover:bg-slate-800 hover:border-slate-400 dark:hover:border-slate-500 transition-all duration-200"
                            >
                                View on GitHub
                            </a>
                        </div>
                    </div>
                </div>
            </section>

            {/* Quick Start Article */}
            <section className="py-16 px-4 sm:px-6 lg:px-8">
                <div className="max-w-4xl mx-auto">
                    <article className="bg-white dark:bg-slate-800 rounded-2xl shadow-xl border border-slate-200 dark:border-slate-700 overflow-hidden">
                        <div className="p-8 sm:p-12">
                            <div className="flex items-center gap-3 mb-6">
                                <div className="w-12 h-12 bg-linear-to-r from-blue-500 to-purple-600 rounded-full flex items-center justify-center">
                                    <div className="w-2 h-2 bg-white rounded-full"></div>
                                </div>
                                <div>
                                    <h2 className="text-2xl sm:text-3xl font-bold text-slate-900 dark:text-white">
                                        Quick Start Guide
                                    </h2>
                                    <p className="text-slate-600 dark:text-slate-400">
                                        Get up and running in minutes
                                    </p>
                                </div>
                            </div>

                            <div className="space-y-6">
                                <div>
                                    <h3 className="text-lg font-semibold text-slate-900 dark:text-white mb-3">
                                        1. Install via NuGet
                                    </h3>
                                    <div className="bg-slate-50 dark:bg-slate-900 rounded-lg p-4 border border-slate-200 dark:border-slate-700">
                                        <code className="text-slate-800 dark:text-slate-200 font-mono text-sm">
                                            dotnet add package Knot
                                        </code>
                                    </div>
                                </div>

                                <div>
                                    <h3 className="text-lg font-semibold text-slate-900 dark:text-white mb-3">
                                        2. Basic Usage
                                    </h3>
                                    <div className="bg-slate-50 dark:bg-slate-900 rounded-lg p-4 border border-slate-200 dark:border-slate-700 overflow-x-auto">
                                        <pre className="text-slate-800 dark:text-slate-200 font-mono text-sm">
                                            {`using Knot;
using Knot.Configuration;

var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Person, PersonDto>();
});

var mapper = config.CreateMapper();
var personDto = mapper.Map<PersonDto>(person);`}
                                        </pre>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </article>
                </div>
            </section>

            {/* Features Grid */}
            <section className="py-16 px-4 sm:px-6 lg:px-8 bg-white/50 dark:bg-slate-800/50 backdrop-blur-sm">
                <div className="max-w-7xl mx-auto">
                    <div className="text-center mb-16">
                        <h2 className="text-3xl sm:text-4xl font-bold text-slate-900 dark:text-white mb-4">
                            Why Choose Knot?
                        </h2>
                        <p className="text-xl text-slate-600 dark:text-slate-300 max-w-2xl mx-auto">
                            Discover the features that make Knot the perfect
                            choice for your .NET mapping needs
                        </p>
                    </div>

                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
                        <article className="group bg-white dark:bg-slate-800 rounded-xl shadow-lg hover:shadow-2xl border border-slate-200 dark:border-slate-700 p-8 transform hover:-translate-y-2 transition-all duration-300">
                            <div className="w-16 h-16 bg-linear-to-r from-blue-500 to-blue-600 rounded-xl flex items-center justify-center mb-6 group-hover:scale-110 transition-transform duration-300">
                                <div className="w-3 h-3 bg-white rounded-full"></div>
                            </div>
                            <h3 className="text-xl font-bold text-slate-900 dark:text-white mb-4">
                                Automatic Mapping
                            </h3>
                            <p className="text-slate-600 dark:text-slate-300 leading-relaxed">
                                Convention-based mapping eliminates boilerplate
                                code. Properties with matching names are mapped
                                automatically, saving you time and reducing
                                errors.
                            </p>
                        </article>

                        <article className="group bg-white dark:bg-slate-800 rounded-xl shadow-lg hover:shadow-2xl border border-slate-200 dark:border-slate-700 p-8 transform hover:-translate-y-2 transition-all duration-300">
                            <div className="w-16 h-16 bg-linear-to-r from-purple-500 to-purple-600 rounded-xl flex items-center justify-center mb-6 group-hover:scale-110 transition-transform duration-300">
                                <div className="w-3 h-3 bg-white rounded-full"></div>
                            </div>
                            <h3 className="text-xl font-bold text-slate-900 dark:text-white mb-4">
                                High Performance
                            </h3>
                            <p className="text-slate-600 dark:text-slate-300 leading-relaxed">
                                Optimized with expression compilation and
                                minimal memory allocations. Benchmark results
                                show Knot outperforming other mapping libraries.
                            </p>
                        </article>

                        <article className="group bg-white dark:bg-slate-800 rounded-xl shadow-lg hover:shadow-2xl border border-slate-200 dark:border-slate-700 p-8 transform hover:-translate-y-2 transition-all duration-300">
                            <div className="w-16 h-16 bg-linear-to-r from-green-500 to-green-600 rounded-xl flex items-center justify-center mb-6 group-hover:scale-110 transition-transform duration-300">
                                <div className="w-3 h-3 bg-white rounded-full"></div>
                            </div>
                            <h3 className="text-xl font-bold text-slate-900 dark:text-white mb-4">
                                Custom Profiles
                            </h3>
                            <p className="text-slate-600 dark:text-slate-300 leading-relaxed">
                                Organize complex mapping logic with profiles and
                                type converters. Perfect for large applications
                                with multiple mapping scenarios.
                            </p>
                        </article>

                        <article className="group bg-white dark:bg-slate-800 rounded-xl shadow-lg hover:shadow-2xl border border-slate-200 dark:border-slate-700 p-8 transform hover:-translate-y-2 transition-all duration-300">
                            <div className="w-16 h-16 bg-linear-to-r from-orange-500 to-orange-600 rounded-xl flex items-center justify-center mb-6 group-hover:scale-110 transition-transform duration-300">
                                <div className="w-3 h-3 bg-white rounded-full"></div>
                            </div>
                            <h3 className="text-xl font-bold text-slate-900 dark:text-white mb-4">
                                Zero Dependencies
                            </h3>
                            <p className="text-slate-600 dark:text-slate-300 leading-relaxed">
                                Lightweight and self-contained. No external
                                dependencies means smaller package size and
                                fewer potential conflicts.
                            </p>
                        </article>

                        <article className="group bg-white dark:bg-slate-800 rounded-xl shadow-lg hover:shadow-2xl border border-slate-200 dark:border-slate-700 p-8 transform hover:-translate-y-2 transition-all duration-300">
                            <div className="w-16 h-16 bg-linear-to-r from-pink-500 to-pink-600 rounded-xl flex items-center justify-center mb-6 group-hover:scale-110 transition-transform duration-300">
                                <div className="w-3 h-3 bg-white rounded-full"></div>
                            </div>
                            <h3 className="text-xl font-bold text-slate-900 dark:text-white mb-4">
                                Type Safe
                            </h3>
                            <p className="text-slate-600 dark:text-slate-300 leading-relaxed">
                                Compile-time type checking ensures mapping
                                correctness. Catch errors at build time, not
                                runtime.
                            </p>
                        </article>

                        <article className="group bg-white dark:bg-slate-800 rounded-xl shadow-lg hover:shadow-2xl border border-slate-200 dark:border-slate-700 p-8 transform hover:-translate-y-2 transition-all duration-300">
                            <div className="w-16 h-16 bg-linear-to-r from-indigo-500 to-indigo-600 rounded-xl flex items-center justify-center mb-6 group-hover:scale-110 transition-transform duration-300">
                                <div className="w-3 h-3 bg-white rounded-full"></div>
                            </div>
                            <h3 className="text-xl font-bold text-slate-900 dark:text-white mb-4">
                                Cross Platform
                            </h3>
                            <p className="text-slate-600 dark:text-slate-300 leading-relaxed">
                                Supports .NET Standard 2.0+ and runs on Windows,
                                Linux, and macOS. Use anywhere .NET runs.
                            </p>
                        </article>
                    </div>
                </div>
            </section>

            {/* Call to Action */}
            <section className="py-16 px-4 sm:px-6 lg:px-8">
                <div className="max-w-4xl mx-auto text-center">
                    <div className="bg-linear-to-r from-blue-600 via-purple-600 to-pink-600 rounded-2xl p-8 sm:p-12 text-white">
                        <h2 className="text-3xl sm:text-4xl font-bold mb-4">
                            Ready to Get Started?
                        </h2>
                        <p className="text-xl mb-8 opacity-90">
                            Join thousands of developers using Knot for their
                            mapping needs
                        </p>
                        <div className="flex flex-col sm:flex-row gap-4 justify-center">
                            <a
                                href="/documentation"
                                className="px-8 py-4 bg-white text-slate-900 font-semibold rounded-full hover:bg-slate-100 transition-colors shadow-lg"
                            >
                                Explore Documentation
                            </a>
                            <a
                                href="https://github.com/dipjyotisikder/Knot"
                                target="_blank"
                                rel="noopener noreferrer"
                                className="px-8 py-4 border-2 border-white/30 text-white font-semibold rounded-full hover:bg-white/10 transition-colors"
                            >
                                Star on GitHub
                            </a>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    );
}
