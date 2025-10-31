using System;
using Knot.Core.Configuration;
using Knot.Core.Mapping;

namespace Knot.Core
{
    /// <summary>
    /// Defines the contract for the mapping engine that executes the mapping operations.
    /// </summary>
    public interface IMappingEngine
    {
/// <summary>
        /// Executes a mapping operation using the provided mapping context.
        /// </summary>
    /// <param name="context">The mapping context containing source and destination information.</param>
      /// <returns>The mapped destination object.</returns>
        object Execute(MappingContext context);

        /// <summary>
 /// Gets the mapping registry containing all registered type mappings.
        /// </summary>
      MappingRegistry Registry { get; }
    }
}
