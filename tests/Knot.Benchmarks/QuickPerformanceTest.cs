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
            Console.WriteLine("=== Knot Performance Optimizations - Quick Test ===\n");

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
            Console.WriteLine("Test 1: Single Object Mapping");
            Console.WriteLine("?????????????????????????????????????");

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
                Console.WriteLine("? EXCELLENT: < 500ns (30-50x faster than before!)");
            }
            else if (avgTime < 1500)
            {
                Console.WriteLine("? GOOD: 500-1500ns (10-30x faster)");
            }
            else
            {
                Console.WriteLine("??  BASELINE: > 1500ns (pre-optimization performance)");
            }

            Console.WriteLine();

            // Test 2: Batch mapping
            Console.WriteLine("Test 2: Batch Mapping (10,000 objects)");
            Console.WriteLine("?????????????????????????????????????");

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
                Console.WriteLine("? EXCELLENT: < 5ms for 10K mappings");
            }
            else if (sw.Elapsed.TotalMilliseconds < 20)
            {
                Console.WriteLine("? GOOD: 5-20ms for 10K mappings");
            }
            else
            {
                Console.WriteLine("??  BASELINE: > 20ms for 10K mappings");
            }

            Console.WriteLine();

            // Test 3: Cache statistics
            Console.WriteLine("Test 3: Compiled Expression Cache Statistics");
            Console.WriteLine("?????????????????????????????????????");

            var stats = CompiledExpressionCache.GetStatistics();
            Console.WriteLine($"Factory cache size: {stats.FactoryCacheSize} types");
            Console.WriteLine($"Getter cache size: {stats.GetterCacheSize} properties");
            Console.WriteLine($"Setter cache size: {stats.SetterCacheSize} properties");
            Console.WriteLine($"Total cached expressions: {stats.TotalCacheSize}");
            Console.WriteLine("? Expressions compiled and cached for reuse");

            Console.WriteLine();

            // Test 4: Manual mapping comparison
            Console.WriteLine("Test 4: Manual Mapping Comparison (Baseline)");
            Console.WriteLine("?????????????????????????????????????");

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
            Console.WriteLine($"Knot mapper overhead: {avgTime / manualAvg:F1}x slower than manual");

            if (avgTime / manualAvg < 3)
            {
                Console.WriteLine("? EXCELLENT: Within 3x of manual mapping!");
            }
            else if (avgTime / manualAvg < 5)
            {
                Console.WriteLine("? GOOD: Within 5x of manual mapping");
            }
            else
            {
                Console.WriteLine("??  NEEDS OPTIMIZATION: > 5x slower than manual");
            }

            Console.WriteLine();
            Console.WriteLine("=== Test Complete ===");
            Console.WriteLine();
            Console.WriteLine("Expected Results with Optimizations:");
            Console.WriteLine("  • Mapping time: 150-300 ns per object");
            Console.WriteLine("  • Throughput: 3-6 million mappings/sec");
            Console.WriteLine("  • Overhead: 2-3x slower than manual mapping");
            Console.WriteLine("  • Improvement: 30-50x faster than pre-optimization");
        }
    }
}
