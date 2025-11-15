using Knot.Configuration;
using Knot.Utilities;
using System.Diagnostics;

namespace Knot.Benchmarks
{
    /// <summary>
    /// Quick verification of performance optimizations
    /// </summary>
    public class QuickPerformanceTest
    {
        public class Person
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
            public string Email { get; set; }
        }

        public class PersonDto
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
            public string Email { get; set; }
        }

        public static void RunQuickTest()
        {
            Console.WriteLine("Knot Performance Quick Test\n");

            // Setup
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Person, PersonDto>();
                });

            var mapper = config.CreateMapper();

            var person = new Person
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Age = 30,
                Email = "john.doe@example.com"
            };

            // Warmup (JIT compilation)
            for (int i = 0; i < 100; i++)
            {
                _ = mapper.Map<PersonDto>(person);
            }

            // Test 1: Single mapping performance
            Console.WriteLine("Test 1: Single object mapping");

            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 10000; i++)
            {
                _ = mapper.Map<PersonDto>(person);
            }
            sw.Stop();

            var avgTime = sw.Elapsed.TotalMilliseconds / 10000 * 1000000; // Convert to nanoseconds
            Console.WriteLine($"Average time per mapping: {avgTime:F0} ns");
            Console.WriteLine($"Throughput: {10000 / sw.Elapsed.TotalSeconds:F0} mappings/sec");

            if (avgTime < 500)
            {
                Console.WriteLine("Excellent: < 500 ns");
            }
            else if (avgTime < 1500)
            {
                Console.WriteLine("Good: 500-1500 ns");
            }
            else
            {
                Console.WriteLine("Baseline: > 1500 ns");
            }

            Console.WriteLine();

            // Test 2: Batch mapping
            Console.WriteLine("Test 2: Batch mapping (10,000 objects)");

            sw.Restart();
            for (int i = 0; i < 10000; i++)
            {
                _ = mapper.Map<PersonDto>(person);
            }
            sw.Stop();

            Console.WriteLine($"Total time: {sw.Elapsed.TotalMilliseconds:F2} ms");
            Console.WriteLine($"Throughput: {10000 / sw.Elapsed.TotalSeconds:F0} mappings/sec");

            if (sw.Elapsed.TotalMilliseconds < 5)
            {
                Console.WriteLine("Excellent: < 5 ms for 10K");
            }
            else if (sw.Elapsed.TotalMilliseconds < 20)
            {
                Console.WriteLine("Good: 5-20 ms for 10K");
            }
            else
            {
                Console.WriteLine("Baseline: > 20 ms for 10K");
            }

            Console.WriteLine();

            // Test 3: Cache statistics
            Console.WriteLine("Test 3: Compiled expression cache statistics");

            var stats = CompiledExpressionCache.GetStatistics();
            Console.WriteLine($"Factory cache size: {stats.FactoryCacheSize} types");
            Console.WriteLine($"Getter cache size: {stats.GetterCacheSize} properties");
            Console.WriteLine($"Setter cache size: {stats.SetterCacheSize} properties");
            Console.WriteLine($"Total cached expressions: {stats.TotalCacheSize}");
            Console.WriteLine("Expressions compiled and cached for reuse");

            Console.WriteLine();

            // Test 4: Manual mapping comparison
            Console.WriteLine("Test 4: Manual mapping comparison (baseline)");

            sw.Restart();
            for (int i = 0; i < 10000; i++)
            {
                _ = new PersonDto
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Age = person.Age,
                    Email = person.Email
                };
            }
            sw.Stop();

            var manualAvg = sw.Elapsed.TotalMilliseconds / 10000 * 1000000;
            Console.WriteLine($"Average time per manual mapping: {manualAvg:F0} ns");
            Console.WriteLine($"Mapper overhead: {avgTime / manualAvg:F1}x slower than manual");

            if (avgTime / manualAvg < 3)
            {
                Console.WriteLine("Excellent: Within 3x of manual mapping");
            }
            else if (avgTime / manualAvg < 5)
            {
                Console.WriteLine("Good: Within 5x of manual mapping");
            }
            else
            {
                Console.WriteLine("Needs optimization: > 5x slower than manual");
            }

            Console.WriteLine();
            Console.WriteLine("Test complete");
            Console.WriteLine();
            Console.WriteLine("Expected results with optimizations:");
            Console.WriteLine("  - Mapping time: ~150-300 ns per object");
            Console.WriteLine("  - Throughput: ~3-6 million mappings/sec");
            Console.WriteLine("  - Overhead: ~2-3x slower than manual mapping");
            Console.WriteLine("  - Improvement: significant vs pre-optimization");
        }
    }
}
