// Cleaned: removed corrupted lines above
import Sidebar from "@/components/Sidebar";
import KnotIcon from "@/components/KnotIcon";

export default function Documentation() {
    return (
        <div className="min-h-screen bg-linear-to-br from-slate-50 via-white to-slate-100 dark:from-slate-900 dark:via-slate-800 dark:to-slate-900">
            <Sidebar />
            <main className="flex flex-col items-center justify-start p-3 sm:p-4 md:p-6 lg:p-8 xl:pl-72">
                <div className="w-full max-w-xs sm:max-w-sm md:max-w-2xl lg:max-w-4xl xl:max-w-5xl 2xl:max-w-6xl">
                    {/* ===== HERO HEADER CARD ===== */}
                    <div className="relative overflow-hidden rounded-2xl sm:rounded-3xl bg-white dark:bg-slate-800 shadow-xl border border-slate-200 dark:border-slate-700 mb-8 sm:mb-12">
                        <div className="absolute inset-0 bg-linear-to-r from-blue-600/5 via-purple-600/3 to-pink-600/5"></div>
                        <div className="relative p-4 sm:p-6 md:p-8 lg:p-12">
                            <div className="flex flex-col sm:flex-row items-center sm:items-start gap-4 sm:gap-6 mb-4 sm:mb-6">
                                <div className="relative shrink-0">
                                    <div className="absolute inset-0 bg-linear-to-r from-blue-500 to-purple-600 rounded-xl sm:rounded-2xl blur-xl opacity-30"></div>
                                    <KnotIcon
                                        size={48}
                                        className="relative z-10 sm:w-16 sm:h-16"
                                    />
                                </div>
                                <div className="text-center sm:text-left flex-1">
                                    <h1 className="text-3xl sm:text-4xl md:text-5xl lg:text-6xl font-black bg-linear-to-r from-slate-900 via-slate-800 to-slate-900 dark:from-white dark:via-slate-200 dark:to-white bg-clip-text text-transparent mb-2">
                                        Documentation
                                    </h1>
                                    <p className="text-base sm:text-lg md:text-xl text-slate-600 dark:text-slate-300">
                                        Everything you need to master Knot
                                    </p>
                                </div>
                            </div>

                            <div className="grid grid-cols-1 xs:grid-cols-3 gap-3 sm:gap-4">
                                <div className="bg-linear-to-r from-blue-50 to-blue-100 dark:from-blue-900/20 dark:to-blue-800/20 rounded-lg sm:rounded-xl p-3 sm:p-4 border border-blue-200 dark:border-blue-800">
                                    <div className="text-xl sm:text-2xl font-bold text-blue-600 dark:text-blue-400">
                                        5
                                    </div>
                                    <div className="text-xs sm:text-sm text-blue-700 dark:text-blue-300">
                                        Sample Projects
                                    </div>
                                </div>
                                <div className="bg-linear-to-r from-purple-50 to-purple-100 dark:from-purple-900/20 dark:to-purple-800/20 rounded-lg sm:rounded-xl p-3 sm:p-4 border border-purple-200 dark:border-purple-800">
                                    <div className="text-xl sm:text-2xl font-bold text-purple-600 dark:text-purple-400">
                                        100%
                                    </div>
                                    <div className="text-xs sm:text-sm text-purple-700 dark:text-purple-300">
                                        Type Safe
                                    </div>
                                </div>
                                <div className="bg-linear-to-r from-green-50 to-green-100 dark:from-green-900/20 dark:to-green-800/20 rounded-lg sm:rounded-xl p-3 sm:p-4 border border-green-200 dark:border-green-800">
                                    <div className="text-xl sm:text-2xl font-bold text-green-600 dark:text-green-400">
                                        0
                                    </div>
                                    <div className="text-xs sm:text-sm text-green-700 dark:text-green-300">
                                        Dependencies
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    {/* ===== OVERVIEW CARD ===== */}
                    <section id="overview" className="mb-4 sm:mb-6">
                        <article className="bg-white dark:bg-slate-800 rounded-2xl shadow-xl border border-slate-200 dark:border-slate-700 overflow-hidden">
                            <div className="bg-linear-to-r from-blue-600 to-purple-600 p-4 sm:p-6">
                                <h2 className="text-xl sm:text-2xl md:text-3xl font-bold text-white flex items-center gap-2 sm:gap-3">
                                    <div className="w-8 h-8 bg-white/20 rounded-lg flex items-center justify-center">
                                        <div className="w-2 h-2 bg-white rounded-full"></div>
                                    </div>
                                    Overview
                                </h2>
                            </div>
                            <div className="p-4 sm:p-6 md:p-8">
                                <p className="text-base sm:text-lg text-slate-700 dark:text-slate-300 mb-4 sm:mb-6 leading-relaxed">
                                    <strong className="text-slate-900 dark:text-white">
                                        Knot
                                    </strong>{" "}
                                    is a lightweight, zero-dependency .NET
                                    object-to-object mapping library designed
                                    for modern development. It transforms
                                    complex mapping tasks into simple,
                                    maintainable code with exceptional
                                    performance.
                                </p>

                                <div className="grid grid-cols-1 sm:grid-cols-2 gap-4 sm:gap-6">
                                    <div className="space-y-3 sm:space-y-4">
                                        <div className="flex items-start gap-2 sm:gap-3">
                                            <div className="w-6 h-6 sm:w-8 sm:h-8 bg-linear-to-r from-blue-500 to-blue-600 rounded-lg flex items-center justify-center shrink-0 mt-0.5">
                                                <div className="w-1.5 h-1.5 bg-white rounded-full"></div>
                                            </div>
                                            <div>
                                                <h3 className="font-semibold text-slate-900 dark:text-white mb-1 text-sm sm:text-base">
                                                    Automatic Mapping
                                                </h3>
                                                <p className="text-xs sm:text-sm text-slate-600 dark:text-slate-400">
                                                    Convention-based property
                                                    mapping eliminates
                                                    boilerplate
                                                </p>
                                            </div>
                                        </div>

                                        <div className="flex items-start gap-2 sm:gap-3">
                                            <div className="w-6 h-6 sm:w-8 sm:h-8 bg-linear-to-r from-purple-500 to-purple-600 rounded-lg flex items-center justify-center shrink-0 mt-0.5">
                                                <div className="w-1.5 h-1.5 bg-white rounded-full"></div>
                                            </div>
                                            <div>
                                                <h3 className="font-semibold text-slate-900 dark:text-white mb-1 text-sm sm:text-base">
                                                    Custom Profiles
                                                </h3>
                                                <p className="text-xs sm:text-sm text-slate-600 dark:text-slate-400">
                                                    Organize complex mappings
                                                    with reusable profiles
                                                </p>
                                            </div>
                                        </div>
                                    </div>

                                    <div className="space-y-3 sm:space-y-4">
                                        <div className="flex items-start gap-2 sm:gap-3">
                                            <div className="w-6 h-6 sm:w-8 sm:h-8 bg-linear-to-r from-green-500 to-green-600 rounded-lg flex items-center justify-center shrink-0 mt-0.5">
                                                <div className="w-1.5 h-1.5 bg-white rounded-full"></div>
                                            </div>
                                            <div>
                                                <h3 className="font-semibold text-slate-900 dark:text-white mb-1 text-sm sm:text-base">
                                                    Collection Support
                                                </h3>
                                                <p className="text-xs sm:text-sm text-slate-600 dark:text-slate-400">
                                                    Seamless mapping of lists,
                                                    arrays, and nested objects
                                                </p>
                                            </div>
                                        </div>

                                        <div className="flex items-start gap-2 sm:gap-3">
                                            <div className="w-6 h-6 sm:w-8 sm:h-8 bg-linear-to-r from-orange-500 to-orange-600 rounded-lg flex items-center justify-center shrink-0 mt-0.5">
                                                <div className="w-1.5 h-1.5 bg-white rounded-full"></div>
                                            </div>
                                            <div>
                                                <h3 className="font-semibold text-slate-900 dark:text-white mb-1 text-sm sm:text-base">
                                                    High Performance
                                                </h3>
                                                <p className="text-xs sm:text-sm text-slate-600 dark:text-slate-400">
                                                    Optimized with expression
                                                    compilation and minimal
                                                    allocations
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </article>
                    </section>

                    {/* ===== INSTALLATION CARD ===== */}
                    <section id="getting-started" className="mb-4 sm:mb-6">
                        <article className="bg-white dark:bg-slate-800 rounded-2xl shadow-xl border border-slate-200 dark:border-slate-700 overflow-hidden">
                            <div className="bg-linear-to-r from-green-600 to-emerald-600 p-4 sm:p-6">
                                <h2 className="text-xl sm:text-2xl md:text-3xl font-bold text-white flex items-center gap-2 sm:gap-3">
                                    <div className="w-8 h-8 bg-white/20 rounded-lg flex items-center justify-center">
                                        <div className="w-2 h-2 bg-white rounded-full"></div>
                                    </div>
                                    Installation
                                </h2>
                            </div>
                            <div className="p-4 sm:p-6 md:p-8">
                                <p className="text-sm sm:text-base text-slate-700 dark:text-slate-300 mb-4 sm:mb-6">
                                    Get started with Knot in seconds using NuGet
                                    Package Manager.
                                </p>

                                <div className="space-y-3 sm:space-y-4">
                                    <div>
                                        <h3 className="text-base sm:text-lg font-semibold text-slate-900 dark:text-white mb-2 sm:mb-3 flex items-center gap-2">
                                            <div className="w-5 h-5 bg-blue-500 rounded flex items-center justify-center">
                                                <div className="w-1.5 h-1.5 bg-white rounded-full"></div>
                                            </div>
                                            Package Manager
                                        </h3>
                                        <div className="bg-slate-50 dark:bg-slate-900 rounded-lg p-3 sm:p-4 border border-slate-200 dark:border-slate-700">
                                            <code className="text-slate-800 dark:text-slate-200 font-mono text-xs sm:text-sm">
                                                Install-Package Knot
                                            </code>
                                        </div>
                                    </div>

                                    <div>
                                        <h3 className="text-base sm:text-lg font-semibold text-slate-900 dark:text-white mb-2 sm:mb-3 flex items-center gap-2">
                                            <div className="w-5 h-5 bg-purple-500 rounded flex items-center justify-center">
                                                <div className="w-1.5 h-1.5 bg-white rounded-full"></div>
                                            </div>
                                            .NET CLI
                                        </h3>
                                        <div className="bg-slate-50 dark:bg-slate-900 rounded-lg p-3 sm:p-4 border border-slate-200 dark:border-slate-700">
                                            <code className="text-slate-800 dark:text-slate-200 font-mono text-xs sm:text-sm">
                                                dotnet add package Knot
                                            </code>
                                        </div>
                                    </div>
                                </div>

                                <div className="mt-4 sm:mt-6 p-3 sm:p-4 bg-blue-50 dark:bg-blue-900/20 rounded-lg border border-blue-200 dark:border-blue-800">
                                    <div className="flex items-start gap-2 sm:gap-3">
                                        <span className="text-blue-500 text-lg sm:text-xl">
                                            <div className="w-5 h-5 bg-blue-500 rounded flex items-center justify-center">
                                                <div className="w-1.5 h-1.5 bg-white rounded-full"></div>
                                            </div>
                                        </span>
                                        <div>
                                            <h4 className="font-semibold text-blue-900 dark:text-blue-100 mb-1 text-sm sm:text-base">
                                                Pro Tip
                                            </h4>
                                            <p className="text-xs sm:text-sm text-blue-700 dark:text-blue-300">
                                                Knot supports .NET Standard 2.0+
                                                and runs on Windows, Linux, and
                                                macOS.
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </article>
                    </section>

                    {/* ===== QUICK START CARD ===== */}
                    <section id="quick-start" className="mb-4 sm:mb-6">
                        <article className="bg-white dark:bg-slate-800 rounded-2xl shadow-xl border border-slate-200 dark:border-slate-700 overflow-hidden">
                            <div className="bg-linear-to-r from-orange-600 to-red-600 p-4 sm:p-6">
                                <h2 className="text-xl sm:text-2xl md:text-3xl font-bold text-white flex items-center gap-2 sm:gap-3">
                                    <div className="w-8 h-8 bg-white/20 rounded-lg flex items-center justify-center">
                                        <div className="w-2 h-2 bg-white rounded-full"></div>
                                    </div>
                                    Quick Start
                                </h2>
                            </div>
                            <div className="p-4 sm:p-6 md:p-8">
                                <div className="grid grid-cols-1 md:grid-cols-2 gap-6 sm:gap-8">
                                    <div>
                                        <h3 className="text-lg sm:text-xl font-semibold text-slate-900 dark:text-white mb-3 sm:mb-4">
                                            Get Mapping in 4 Steps
                                        </h3>
                                        <ol className="space-y-2 sm:space-y-3">
                                            <li className="flex items-start gap-2 sm:gap-3">
                                                <div className="w-5 h-5 sm:w-6 sm:h-6 bg-blue-500 text-white rounded-full flex items-center justify-center text-xs sm:text-sm font-bold shrink-0 mt-0.5">
                                                    1
                                                </div>
                                                <span className="text-sm sm:text-base text-slate-700 dark:text-slate-300">
                                                    Define your source and
                                                    destination classes
                                                </span>
                                            </li>
                                            <li className="flex items-start gap-2 sm:gap-3">
                                                <div className="w-5 h-5 sm:w-6 sm:h-6 bg-blue-500 text-white rounded-full flex items-center justify-center text-xs sm:text-sm font-bold shrink-0 mt-0.5">
                                                    2
                                                </div>
                                                <span className="text-sm sm:text-base text-slate-700 dark:text-slate-300">
                                                    Configure mappings once at
                                                    startup
                                                </span>
                                            </li>
                                            <li className="flex items-start gap-2 sm:gap-3">
                                                <div className="w-5 h-5 sm:w-6 sm:h-6 bg-blue-500 text-white rounded-full flex items-center justify-center text-xs sm:text-sm font-bold shrink-0 mt-0.5">
                                                    3
                                                </div>
                                                <span className="text-sm sm:text-base text-slate-700 dark:text-slate-300">
                                                    Create a mapper instance
                                                </span>
                                            </li>
                                            <li className="flex items-start gap-2 sm:gap-3">
                                                <div className="w-5 h-5 sm:w-6 sm:h-6 bg-blue-500 text-white rounded-full flex items-center justify-center text-xs sm:text-sm font-bold shrink-0 mt-0.5">
                                                    4
                                                </div>
                                                <span className="text-sm sm:text-base text-slate-700 dark:text-slate-300">
                                                    Map your objects!
                                                </span>
                                            </li>
                                        </ol>
                                    </div>

                                    <div>
                                        <h3 className="text-lg sm:text-xl font-semibold text-slate-900 dark:text-white mb-3 sm:mb-4">
                                            Basic Example
                                        </h3>
                                        <div className="bg-slate-50 dark:bg-slate-900 rounded-lg p-3 sm:p-4 border border-slate-200 dark:border-slate-700 overflow-x-auto">
                                            <pre className="text-slate-800 dark:text-slate-200 font-mono text-xs sm:text-sm">
                                                {`using Knot;
using Knot.Configuration;

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}

public class PersonDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}

var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Person, PersonDto>();
});

var mapper = config.CreateMapper();
var person = new Person
{
    FirstName = "John",
    LastName = "Doe",
    Age = 30
};

var dto = mapper.Map<PersonDto>(person);`}
                                            </pre>
                                        </div>
                                    </div>
                                </div>

                                <div className="mt-4 sm:mt-6 p-3 sm:p-4 bg-green-50 dark:bg-green-900/20 rounded-lg border border-green-200 dark:border-green-800">
                                    <div className="flex items-start gap-2 sm:gap-3">
                                        <span className="text-green-500 text-lg sm:text-xl">
                                            <div className="w-5 h-5 bg-green-500 rounded flex items-center justify-center">
                                                <div className="w-1.5 h-1.5 bg-white rounded-full"></div>
                                            </div>
                                        </span>
                                        <div>
                                            <p className="text-sm sm:text-base text-green-700 dark:text-green-300">
                                                <strong>That's it!</strong> Knot
                                                automatically maps properties
                                                with matching names. No
                                                configuration needed for simple
                                                cases.
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </article>
                    </section>

                    {/* ===== SAMPLE PROJECTS CARD ===== */}
                    <section id="sample-projects" className="mb-4 sm:mb-6">
                        <article className="bg-white dark:bg-slate-800 rounded-2xl shadow-xl border border-slate-200 dark:border-slate-700 overflow-hidden">
                            <div className="bg-linear-to-r from-purple-600 to-pink-600 p-4 sm:p-6">
                                <h2 className="text-xl sm:text-2xl md:text-3xl font-bold text-white flex items-center gap-2 sm:gap-3">
                                    <div className="w-8 h-8 bg-white/20 rounded-lg flex items-center justify-center">
                                        <div className="w-2 h-2 bg-white rounded-full"></div>
                                    </div>
                                    Sample Projects
                                </h2>
                                <p className="text-purple-100 mt-1 sm:mt-2 text-sm sm:text-base">
                                    Explore real-world examples in the{" "}
                                    <code className="bg-purple-800/50 px-1 sm:px-2 py-0.5 sm:py-1 rounded text-xs sm:text-sm">
                                        samples/
                                    </code>{" "}
                                    directory
                                </p>
                            </div>
                            <div className="p-4 sm:p-6 md:p-8">
                                <div className="grid grid-cols-1 sm:grid-cols-2 xl:grid-cols-2 gap-4 sm:gap-6">
                                    {/* BasicMapping */}
                                    <div className="group bg-linear-to-br from-blue-50 to-blue-100 dark:from-blue-900/20 dark:to-blue-800/20 rounded-lg sm:rounded-xl p-4 sm:p-6 border border-blue-200 dark:border-blue-800 hover:shadow-lg transition-all duration-300">
                                        <div className="flex items-start gap-3 sm:gap-4">
                                            <div className="w-10 h-10 sm:w-12 sm:h-12 bg-linear-to-r from-blue-500 to-blue-600 rounded-lg sm:rounded-xl flex items-center justify-center group-hover:scale-110 transition-transform duration-300 shrink-0">
                                                <div className="w-2 h-2 bg-white rounded-full"></div>
                                            </div>
                                            <div className="flex-1 min-w-0">
                                                <div className="flex flex-col sm:flex-row sm:items-center gap-1 sm:gap-2 mb-2">
                                                    <h3 className="text-base sm:text-lg font-bold text-slate-900 dark:text-white truncate">
                                                        BasicMapping
                                                    </h3>
                                                    <span className="px-1.5 sm:px-2 py-0.5 sm:py-1 bg-blue-100 dark:bg-blue-800 text-blue-700 dark:text-blue-300 text-xs rounded-full font-medium shrink-0">
                                                        Basic
                                                    </span>
                                                </div>
                                                <p className="text-xs sm:text-sm text-slate-600 dark:text-slate-400 mb-3 sm:mb-4">
                                                    Simple flat object mapping
                                                    with matching property
                                                    names. Perfect for most DTO
                                                    scenarios.
                                                </p>
                                                <div className="bg-slate-50 dark:bg-slate-900 rounded-lg p-2 sm:p-3 border border-slate-200 dark:border-slate-700 overflow-x-auto">
                                                    <pre className="text-slate-800 dark:text-slate-200 font-mono text-xs">
                                                        {`var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Person, PersonDto>();
});

var mapper = config.CreateMapper();
var dto = mapper.Map<PersonDto>(person);`}
                                                    </pre>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    {/* CustomPropertyMapping */}
                                    <div className="group bg-linear-to-br from-purple-50 to-purple-100 dark:from-purple-900/20 dark:to-purple-800/20 rounded-lg sm:rounded-xl p-4 sm:p-6 border border-purple-200 dark:border-purple-800 hover:shadow-lg transition-all duration-300">
                                        <div className="flex items-start gap-3 sm:gap-4">
                                            <div className="w-10 h-10 sm:w-12 sm:h-12 bg-linear-to-r from-purple-500 to-purple-600 rounded-lg sm:rounded-xl flex items-center justify-center group-hover:scale-110 transition-transform duration-300 shrink-0">
                                                <div className="w-2 h-2 bg-white rounded-full"></div>
                                            </div>
                                            <div className="flex-1 min-w-0">
                                                <div className="flex flex-col sm:flex-row sm:items-center gap-1 sm:gap-2 mb-2">
                                                    <h3 className="text-base sm:text-lg font-bold text-slate-900 dark:text-white truncate">
                                                        CustomPropertyMapping
                                                    </h3>
                                                    <span className="px-1.5 sm:px-2 py-0.5 sm:py-1 bg-purple-100 dark:bg-purple-800 text-purple-700 dark:text-purple-300 text-xs rounded-full font-medium shrink-0">
                                                        Advanced
                                                    </span>
                                                </div>
                                                <p className="text-xs sm:text-sm text-slate-600 dark:text-slate-400 mb-3 sm:mb-4">
                                                    Custom property mappings,
                                                    calculated values, and
                                                    property ignoring.
                                                </p>
                                                <div className="bg-slate-50 dark:bg-slate-900 rounded-lg p-2 sm:p-3 border border-slate-200 dark:border-slate-700 overflow-x-auto">
                                                    <pre className="text-slate-800 dark:text-slate-200 font-mono text-xs">
                                                        {`cfg.CreateMap<Employee, EmployeeDto>(map =>
{
    map.ForMember(dest => dest.FullName,
        src => src.FirstName + " " + src.LastName);
    map.ForMember(dest => dest.YearsOfService,
        src => DateTime.Now.Year - src.HireDate.Year);
});`}
                                                    </pre>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    {/* CollectionMapping */}
                                    <div className="group bg-linear-to-br from-green-50 to-green-100 dark:from-green-900/20 dark:to-green-800/20 rounded-lg sm:rounded-xl p-4 sm:p-6 border border-green-200 dark:border-green-800 hover:shadow-lg transition-all duration-300">
                                        <div className="flex items-start gap-3 sm:gap-4">
                                            <div className="w-10 h-10 sm:w-12 sm:h-12 bg-linear-to-r from-green-500 to-green-600 rounded-lg sm:rounded-xl flex items-center justify-center group-hover:scale-110 transition-transform duration-300 shrink-0">
                                                <div className="w-2 h-2 bg-white rounded-full"></div>
                                            </div>
                                            <div className="flex-1 min-w-0">
                                                <div className="flex flex-col sm:flex-row sm:items-center gap-1 sm:gap-2 mb-2">
                                                    <h3 className="text-base sm:text-lg font-bold text-slate-900 dark:text-white truncate">
                                                        CollectionMapping
                                                    </h3>
                                                    <span className="px-1.5 sm:px-2 py-0.5 sm:py-1 bg-green-100 dark:bg-green-800 text-green-700 dark:text-green-300 text-xs rounded-full font-medium shrink-0">
                                                        Collection
                                                    </span>
                                                </div>
                                                <p className="text-xs sm:text-sm text-slate-600 dark:text-slate-400 mb-3 sm:mb-4">
                                                    Mapping lists, arrays, and
                                                    collections of objects with
                                                    extension methods.
                                                </p>
                                                <div className="bg-slate-50 dark:bg-slate-900 rounded-lg p-2 sm:p-3 border border-slate-200 dark:border-slate-700 overflow-x-auto">
                                                    <pre className="text-slate-800 dark:text-slate-200 font-mono text-xs">
                                                        {`var customers = new List<Customer> { ... };
var customerDtos = customers.MapToList<Customer, CustomerDto>(mapper);`}
                                                    </pre>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    {/* MappingProfiles */}
                                    <div className="group bg-linear-to-br from-orange-50 to-orange-100 dark:from-orange-900/20 dark:to-orange-800/20 rounded-lg sm:rounded-xl p-4 sm:p-6 border border-orange-200 dark:border-orange-800 hover:shadow-lg transition-all duration-300">
                                        <div className="flex items-start gap-3 sm:gap-4">
                                            <div className="w-10 h-10 sm:w-12 sm:h-12 bg-linear-to-r from-orange-500 to-orange-600 rounded-lg sm:rounded-xl flex items-center justify-center group-hover:scale-110 transition-transform duration-300 shrink-0">
                                                <div className="w-2 h-2 bg-white rounded-full"></div>
                                            </div>
                                            <div className="flex-1 min-w-0">
                                                <div className="flex flex-col sm:flex-row sm:items-center gap-1 sm:gap-2 mb-2">
                                                    <h3 className="text-base sm:text-lg font-bold text-slate-900 dark:text-white truncate">
                                                        MappingProfiles
                                                    </h3>
                                                    <span className="px-1.5 sm:px-2 py-0.5 sm:py-1 bg-orange-100 dark:bg-orange-800 text-orange-700 dark:text-orange-300 text-xs rounded-full font-medium shrink-0">
                                                        Organization
                                                    </span>
                                                </div>
                                                <p className="text-xs sm:text-sm text-slate-600 dark:text-slate-400 mb-3 sm:mb-4">
                                                    Organizing mappings with
                                                    profiles for large
                                                    applications.
                                                </p>
                                                <div className="bg-slate-50 dark:bg-slate-900 rounded-lg p-2 sm:p-3 border border-slate-200 dark:border-slate-700 overflow-x-auto">
                                                    <pre className="text-slate-800 dark:text-slate-200 font-mono text-xs">
                                                        {`public class UserMappingProfile : Profile
{
    protected override void Configure()
    {
        CreateMap<User, UserDto>();
    }
}`}
                                                    </pre>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    {/* NestedObjects - Full Width */}
                                    <div className="sm:col-span-2 group bg-linear-to-br from-pink-50 to-pink-100 dark:from-pink-900/20 dark:to-pink-800/20 rounded-lg sm:rounded-xl p-4 sm:p-6 border border-pink-200 dark:border-pink-800 hover:shadow-lg transition-all duration-300">
                                        <div className="flex items-start gap-3 sm:gap-4">
                                            <div className="w-10 h-10 sm:w-12 sm:h-12 bg-linear-to-r from-pink-500 to-pink-600 rounded-lg sm:rounded-xl flex items-center justify-center group-hover:scale-110 transition-transform duration-300 shrink-0">
                                                <div className="w-2 h-2 bg-white rounded-full"></div>
                                            </div>
                                            <div className="flex-1 min-w-0">
                                                <div className="flex flex-col sm:flex-row sm:items-center gap-1 sm:gap-2 mb-2">
                                                    <h3 className="text-base sm:text-lg font-bold text-slate-900 dark:text-white truncate">
                                                        NestedObjects
                                                    </h3>
                                                    <span className="px-1.5 sm:px-2 py-0.5 sm:py-1 bg-pink-100 dark:bg-pink-800 text-pink-700 dark:text-pink-300 text-xs rounded-full font-medium shrink-0">
                                                        Complex
                                                    </span>
                                                </div>
                                                <p className="text-xs sm:text-sm text-slate-600 dark:text-slate-400 mb-3 sm:mb-4">
                                                    Deep object graphs with
                                                    nested relationships and
                                                    complex hierarchies.
                                                </p>
                                                <div className="bg-slate-50 dark:bg-slate-900 rounded-lg p-2 sm:p-3 border border-slate-200 dark:border-slate-700 overflow-x-auto">
                                                    <pre className="text-slate-800 dark:text-slate-200 font-mono text-xs">
                                                        {`public class Company
{
    public Address Headquarters { get; set; }
}

var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Company, CompanyDto>();
    cfg.CreateMap<Address, AddressDto>();
});`}
                                                    </pre>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </article>
                    </section>

                    {/* ===== API REFERENCE CARD ===== */}
                    <section id="api-reference" className="mb-4 sm:mb-6">
                        <article className="bg-white dark:bg-slate-800 rounded-2xl shadow-xl border border-slate-200 dark:border-slate-700 overflow-hidden">
                            <div className="bg-linear-to-r from-indigo-600 to-blue-600 p-4 sm:p-6">
                                <h2 className="text-xl sm:text-2xl md:text-3xl font-bold text-white flex items-center gap-2 sm:gap-3">
                                    <div className="w-8 h-8 bg-white/20 rounded-lg flex items-center justify-center">
                                        <div className="w-2 h-2 bg-white rounded-full"></div>
                                    </div>
                                    API Reference
                                </h2>
                            </div>
                            <div className="p-4 sm:p-6 md:p-8">
                                <div className="text-center py-8 sm:py-12">
                                    <div className="w-12 h-12 sm:w-16 sm:h-16 bg-linear-to-r from-indigo-500 to-blue-600 rounded-full flex items-center justify-center mx-auto mb-3 sm:mb-4">
                                        <div className="w-3 h-3 bg-white rounded-full"></div>
                                    </div>
                                    <h3 className="text-lg sm:text-xl font-semibold text-slate-900 dark:text-white mb-2">
                                        Coming Soon
                                    </h3>
                                    <p className="text-sm sm:text-base text-slate-600 dark:text-slate-400 max-w-sm sm:max-w-md mx-auto">
                                        Detailed API documentation with examples
                                        and usage guides is currently being
                                        prepared. Check the README for now.
                                    </p>
                                </div>
                            </div>
                        </article>
                    </section>

                    {/* ===== BEST PRACTICES CARD ===== */}
                    <section id="best-practices" className="mb-4 sm:mb-6">
                        <article className="bg-white dark:bg-slate-800 rounded-2xl shadow-xl border border-slate-200 dark:border-slate-700 overflow-hidden">
                            <div className="bg-linear-to-r from-emerald-600 to-teal-600 p-4 sm:p-6">
                                <h2 className="text-xl sm:text-2xl md:text-3xl font-bold text-white flex items-center gap-2 sm:gap-3">
                                    <div className="w-8 h-8 bg-white/20 rounded-lg flex items-center justify-center">
                                        <div className="w-2 h-2 bg-white rounded-full"></div>
                                    </div>
                                    Best Practices
                                </h2>
                            </div>
                            <div className="p-4 sm:p-6 md:p-8">
                                <div className="grid grid-cols-1 sm:grid-cols-2 gap-4 sm:gap-6">
                                    <div className="space-y-3 sm:space-y-4">
                                        <div className="flex items-start gap-2 sm:gap-3">
                                            <div className="w-6 h-6 sm:w-8 sm:h-8 bg-emerald-500 rounded-lg flex items-center justify-center shrink-0">
                                                <span className="text-white font-bold text-xs sm:text-sm">
                                                    1
                                                </span>
                                            </div>
                                            <div>
                                                <h4 className="font-semibold text-slate-900 dark:text-white mb-1 text-sm sm:text-base">
                                                    Configure at Startup
                                                </h4>
                                                <p className="text-xs sm:text-sm text-slate-600 dark:text-slate-400">
                                                    Set up all mappings once
                                                    during application startup
                                                    for optimal performance.
                                                </p>
                                            </div>
                                        </div>

                                        <div className="flex items-start gap-2 sm:gap-3">
                                            <div className="w-6 h-6 sm:w-8 sm:h-8 bg-emerald-500 rounded-lg flex items-center justify-center shrink-0">
                                                <span className="text-white font-bold text-xs sm:text-sm">
                                                    2
                                                </span>
                                            </div>
                                            <div>
                                                <h4 className="font-semibold text-slate-900 dark:text-white mb-1 text-sm sm:text-base">
                                                    Use Profiles for
                                                    Organization
                                                </h4>
                                                <p className="text-xs sm:text-sm text-slate-600 dark:text-slate-400">
                                                    Group related mappings in
                                                    profiles for better
                                                    maintainability in large
                                                    projects.
                                                </p>
                                            </div>
                                        </div>
                                    </div>

                                    <div className="space-y-3 sm:space-y-4">
                                        <div className="flex items-start gap-2 sm:gap-3">
                                            <div className="w-6 h-6 sm:w-8 sm:h-8 bg-emerald-500 rounded-lg flex items-center justify-center shrink-0">
                                                <span className="text-white font-bold text-xs sm:text-sm">
                                                    3
                                                </span>
                                            </div>
                                            <div>
                                                <h4 className="font-semibold text-slate-900 dark:text-white mb-1 text-sm sm:text-base">
                                                    Write Unit Tests
                                                </h4>
                                                <p className="text-xs sm:text-sm text-slate-600 dark:text-slate-400">
                                                    Test your custom mapping
                                                    logic to ensure correctness
                                                    and catch regressions.
                                                </p>
                                            </div>
                                        </div>

                                        <div className="flex items-start gap-2 sm:gap-3">
                                            <div className="w-6 h-6 sm:w-8 sm:h-8 bg-emerald-500 rounded-lg flex items-center justify-center shrink-0">
                                                <span className="text-white font-bold text-xs sm:text-sm">
                                                    4
                                                </span>
                                            </div>
                                            <div>
                                                <h4 className="font-semibold text-slate-900 dark:text-white mb-1 text-sm sm:text-base">
                                                    Favor Convention
                                                </h4>
                                                <p className="text-xs sm:text-sm text-slate-600 dark:text-slate-400">
                                                    Use automatic mapping by
                                                    convention for simple cases,
                                                    custom mapping only when
                                                    needed.
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </article>
                    </section>

                    {/* ===== MIGRATION GUIDE CARD ===== */}
                    <section id="migration-guide" className="mb-4 sm:mb-6">
                        <article className="bg-white dark:bg-slate-800 rounded-2xl shadow-xl border border-slate-200 dark:border-slate-700 overflow-hidden">
                            <div className="bg-linear-to-r from-amber-600 to-orange-600 p-4 sm:p-6">
                                <h2 className="text-xl sm:text-2xl md:text-3xl font-bold text-white flex items-center gap-2 sm:gap-3">
                                    <div className="w-8 h-8 bg-white/20 rounded-lg flex items-center justify-center">
                                        <div className="w-2 h-2 bg-white rounded-full"></div>
                                    </div>
                                    Migration Guide
                                </h2>
                            </div>
                            <div className="p-4 sm:p-6 md:p-8">
                                <div className="bg-amber-50 dark:bg-amber-900/20 rounded-lg p-4 sm:p-6 border border-amber-200 dark:border-amber-800">
                                    <div className="flex flex-col sm:flex-row items-start gap-3 sm:gap-4">
                                        <div className="w-10 h-10 sm:w-12 sm:h-12 bg-amber-500 rounded-lg sm:rounded-xl flex items-center justify-center shrink-0">
                                            <div className="w-2 h-2 bg-white rounded-full"></div>
                                        </div>
                                        <div className="flex-1">
                                            <h3 className="text-base sm:text-lg font-semibold text-amber-900 dark:text-amber-100 mb-2">
                                                Coming from AutoMapper?
                                            </h3>
                                            <p className="text-sm sm:text-base text-amber-700 dark:text-amber-300 mb-3 sm:mb-4">
                                                Knot provides a simpler, more
                                                performant alternative to
                                                AutoMapper with zero
                                                dependencies.
                                            </p>
                                            <div className="grid grid-cols-1 xs:grid-cols-2 gap-3 sm:gap-4 text-xs sm:text-sm">
                                                <div className="bg-white dark:bg-slate-800 rounded p-2 sm:p-3 border border-amber-300 dark:border-amber-700">
                                                    <div className="font-medium text-amber-900 dark:text-amber-100 mb-1">
                                                        AutoMapper
                                                    </div>
                                                    <code className="text-amber-700 dark:text-amber-300 text-xs">
                                                        CreateMap&lt;Source,
                                                        Dest&gt;()
                                                    </code>
                                                </div>
                                                <div className="bg-white dark:bg-slate-800 rounded p-2 sm:p-3 border border-amber-300 dark:border-amber-700">
                                                    <div className="font-medium text-amber-900 dark:text-amber-100 mb-1">
                                                        Knot
                                                    </div>
                                                    <code className="text-amber-700 dark:text-amber-300 text-xs">
                                                        cfg.CreateMap&lt;Source,
                                                        Dest&gt;()
                                                    </code>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </article>
                    </section>

                    {/* ===== COMMUNITY & CONTRIBUTION CARD ===== */}
                    <section id="community" className="mb-4 sm:mb-6">
                        <article className="bg-white dark:bg-slate-800 rounded-2xl shadow-xl border border-slate-200 dark:border-slate-700 overflow-hidden">
                            <div className="bg-linear-to-r from-rose-600 to-pink-600 p-4 sm:p-6">
                                <h2 className="text-xl sm:text-2xl md:text-3xl font-bold text-white flex items-center gap-2 sm:gap-3">
                                    <div className="w-8 h-8 bg-white/20 rounded-lg flex items-center justify-center">
                                        <div className="w-2 h-2 bg-white rounded-full"></div>
                                    </div>
                                    Community & Contribution
                                </h2>
                            </div>
                            <div className="p-4 sm:p-6 md:p-8">
                                <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4 sm:gap-6">
                                    <a
                                        href="https://github.com/dipjyotisikder/Knot"
                                        target="_blank"
                                        rel="noopener noreferrer"
                                        className="group bg-linear-to-br from-slate-50 to-slate-100 dark:from-slate-700 dark:to-slate-600 rounded-lg sm:rounded-xl p-4 sm:p-6 border border-slate-200 dark:border-slate-600 hover:shadow-lg hover:-translate-y-1 transition-all duration-300"
                                    >
                                        <div className="w-10 h-10 sm:w-12 sm:h-12 bg-linear-to-r from-slate-600 to-slate-800 rounded-lg sm:rounded-xl flex items-center justify-center mb-3 sm:mb-4 group-hover:scale-110 transition-transform duration-300">
                                            <div className="w-2 h-2 bg-white rounded-full"></div>
                                        </div>
                                        <h3 className="text-base sm:text-lg font-semibold text-slate-900 dark:text-white mb-2">
                                            Star on GitHub
                                        </h3>
                                        <p className="text-xs sm:text-sm text-slate-600 dark:text-slate-400">
                                            Show your support and stay updated
                                            with releases.
                                        </p>
                                    </a>

                                    <a
                                        href="https://github.com/dipjyotisikder/Knot#readme"
                                        target="_blank"
                                        rel="noopener noreferrer"
                                        className="group bg-linear-to-br from-slate-50 to-slate-100 dark:from-slate-700 dark:to-slate-600 rounded-lg sm:rounded-xl p-4 sm:p-6 border border-slate-200 dark:border-slate-600 hover:shadow-lg hover:-translate-y-1 transition-all duration-300"
                                    >
                                        <div className="w-10 h-10 sm:w-12 sm:h-12 bg-linear-to-r from-slate-600 to-slate-800 rounded-lg sm:rounded-xl flex items-center justify-center mb-3 sm:mb-4 group-hover:scale-110 transition-transform duration-300">
                                            <div className="w-2 h-2 bg-white rounded-full"></div>
                                        </div>
                                        <h3 className="text-base sm:text-lg font-semibold text-slate-900 dark:text-white mb-2">
                                            Read the README
                                        </h3>
                                        <p className="text-xs sm:text-sm text-slate-600 dark:text-slate-400">
                                            Comprehensive documentation and
                                            contribution guidelines.
                                        </p>
                                    </a>

                                    <div className="group bg-linear-to-br from-slate-50 to-slate-100 dark:from-slate-700 dark:to-slate-600 rounded-lg sm:rounded-xl p-4 sm:p-6 border border-slate-200 dark:border-slate-600 hover:shadow-lg hover:-translate-y-1 transition-all duration-300 sm:col-span-2 lg:col-span-1">
                                        <div className="w-10 h-10 sm:w-12 sm:h-12 bg-linear-to-r from-slate-600 to-slate-800 rounded-lg sm:rounded-xl flex items-center justify-center mb-3 sm:mb-4 group-hover:scale-110 transition-transform duration-300">
                                            <div className="w-2 h-2 bg-white rounded-full"></div>
                                        </div>
                                        <h3 className="text-base sm:text-lg font-semibold text-slate-900 dark:text-white mb-2">
                                            Report Issues
                                        </h3>
                                        <p className="text-xs sm:text-sm text-slate-600 dark:text-slate-400">
                                            Found a bug? Open an issue on GitHub
                                            to help improve Knot.
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </article>
                    </section>
                </div>
            </main>
        </div>
    );
}
