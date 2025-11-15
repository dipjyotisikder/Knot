using Knot.Configuration;
using Knot.Mapping;

namespace Knot
{
    /// <summary>
    /// Internal contract for the mapping engine.
    /// </summary>
    internal interface IMappingEngine
    {
        /// <summary>
        /// Executes a mapping operation.
        /// </summary>
        object Execute(MappingContext context);

        /// <summary>
        /// Gets the registry with all type mappings.
        /// </summary>
        MappingRegistry Registry { get; }
    }
}
