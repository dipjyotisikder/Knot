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
            Console.WriteLine("1. Simple mapping benchmarks");
            Console.WriteLine("2. Collection mapping benchmarks");
            Console.WriteLine("3. Complex mapping benchmarks");
            Console.WriteLine("4. Configuration benchmarks");
            Console.WriteLine("5. Memory allocation benchmarks");
            Console.WriteLine("6. Run all benchmarks");
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
                        BenchmarkRunner.Run<SimpleMappingBenchmarks>();
                        break;
                    case "2":
                        BenchmarkRunner.Run<CollectionMappingBenchmarks>();
                        break;
                    case "3":
                        BenchmarkRunner.Run<ComplexMappingBenchmarks>();
                        break;
                    case "4":
                        BenchmarkRunner.Run<ConfigurationBenchmarks>();
                        break;
                    case "5":
                        BenchmarkRunner.Run<MemoryAllocationBenchmarks>();
                        break;
                    case "6":
                        RunAllBenchmarks();
                        break;
                    case "9":
                        return;
                    default:
                        Console.WriteLine("Invalid choice; running quick test.");
                        QuickPerformanceTest.RunQuickTest();
                        break;
                }
            }
            else
            {
                BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
            }
        }

        static void RunAllBenchmarks()
        {
            Console.WriteLine("Running all benchmarks...");

            BenchmarkRunner.Run<SimpleMappingBenchmarks>();
            BenchmarkRunner.Run<CollectionMappingBenchmarks>();
            BenchmarkRunner.Run<ComplexMappingBenchmarks>();
            BenchmarkRunner.Run<ConfigurationBenchmarks>();
            BenchmarkRunner.Run<MemoryAllocationBenchmarks>();
        }
    }
}
