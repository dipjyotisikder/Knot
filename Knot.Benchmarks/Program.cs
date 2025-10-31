using BenchmarkDotNet.Running;

namespace Knot.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Knot Performance Benchmarks ===");
            Console.WriteLine();
            Console.WriteLine("Select benchmark to run:");
            Console.WriteLine("0. Quick Performance Test (Fast verification)");
            Console.WriteLine("1. Simple Mapping Benchmarks");
            Console.WriteLine("2. Collection Mapping Benchmarks");
            Console.WriteLine("3. Complex Mapping Benchmarks");
            Console.WriteLine("4. Configuration Benchmarks");
            Console.WriteLine("5. Memory Allocation Benchmarks");
            Console.WriteLine("6. Run ALL Benchmarks");
            Console.WriteLine("9. Exit");
            Console.WriteLine();

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
                        Console.WriteLine("Invalid choice. Running quick test...");
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
            Console.WriteLine("\n=== Running All Benchmarks ===\n");

            BenchmarkRunner.Run<SimpleMappingBenchmarks>();
            BenchmarkRunner.Run<CollectionMappingBenchmarks>();
            BenchmarkRunner.Run<ComplexMappingBenchmarks>();
            BenchmarkRunner.Run<ConfigurationBenchmarks>();
            BenchmarkRunner.Run<MemoryAllocationBenchmarks>();
        }
    }
}
