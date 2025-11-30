using BenchmarkDotNet.Running;

namespace Knot.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Knot Performance Benchmarks");

            Console.WriteLine("Select benchmark to run:");
            Console.WriteLine("0. Quick Performance Test (fast check)");
            Console.WriteLine("1. Knot vs AutoMapper: Simple mapping");
            Console.WriteLine("2. Knot vs AutoMapper: Collection mapping");
            Console.WriteLine("3. Knot vs AutoMapper: Complex mapping");
            Console.WriteLine("4. Knot vs AutoMapper: Configuration");
            Console.WriteLine("5. Knot vs AutoMapper: Memory allocation");
            Console.WriteLine("6. Run all Knot vs AutoMapper benchmarks");
            Console.WriteLine("9. Exit");

            if (args.Length == 0)
            {
                Console.Write("Enter your choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        QuickPerformanceTest.RunQuickTest();
                        break;
                    case "1":
                        BenchmarkRunner.Run<KnotVsAutoMapperSimpleBenchmarks>();
                        break;
                    case "2":
                        BenchmarkRunner.Run<KnotVsAutoMapperCollectionBenchmarks>();
                        break;
                    case "3":
                        BenchmarkRunner.Run<KnotVsAutoMapperComplexBenchmarks>();
                        break;
                    case "4":
                        BenchmarkRunner.Run<KnotVsAutoMapperConfigurationBenchmarks>();
                        break;
                    case "5":
                        BenchmarkRunner.Run<KnotVsAutoMapperMemoryBenchmarks>();
                        break;
                    case "6":
                        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
                        break;
                    case "9":
                        return;
                    default:
                        Console.WriteLine("Invalid choice; running all Knot vs AutoMapper benchmarks.");
                        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
                        break;
                }
            }
            else
            {
                BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
            }
        }
    }
}
