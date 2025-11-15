using System;
using Knot.Configuration;
using Knot.Exceptions;
using Knot.Mapping;

namespace Knot
{
    /// <summary>
    /// Core mapping engine that executes object transformations.
    /// </summary>
    public class MapperEngine : IMapper, IMappingEngine
    {
        private readonly MappingRegistry _registry;

        /// <summary>
        /// Creates a new mapping engine with the given registry.
        /// </summary>
        internal MapperEngine(MappingRegistry registry)
        {
            _registry = registry ?? throw new ArgumentNullException(nameof(registry));
        }

        /// <summary>
        /// Gets the mapping registry with all type mappings.
        /// </summary>
        MappingRegistry IMappingEngine.Registry => _registry;

        /// <summary>
        /// Maps source to a new destination instance.
        /// </summary>
        public TDestination Map<TDestination>(object source)
        {
            if (source == null)
            {
                return default!;
            }

            var sourceType = source.GetType();
            var destinationType = typeof(TDestination);

            var context = new MappingContext(source, sourceType, destinationType);
            return (TDestination)((IMappingEngine)this).Execute(context);
        }

        /// <summary>
        /// Maps source to a new destination with explicit types.
        /// </summary>
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            if (source == null)
            {
                return default!;
            }

            var context = new MappingContext(source, typeof(TSource), typeof(TDestination));
            return (TDestination)((IMappingEngine)this).Execute(context);
        }

        /// <summary>
        /// Maps source to an existing destination instance.
        /// </summary>
        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            if (source == null)
            {
                return destination;
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination), "Destination cannot be null when mapping to an existing instance.");
            }

            var context = new MappingContext(source, typeof(TSource), typeof(TDestination), destination);
            return (TDestination)((IMappingEngine)this).Execute(context);
        }

        /// <summary>
        /// Executes the mapping operation with the given context.
        /// </summary>
        object IMappingEngine.Execute(MappingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var typeMap = _registry.GetTypeMap(context.SourceType, context.DestinationType);
            if (typeMap == null)
            {
                throw new MappingException($"No mapping found from {context.SourceType.Name} to {context.DestinationType.Name}. " +
                    $"Ensure that CreateMap<{context.SourceType.Name}, {context.DestinationType.Name}>() has been called during configuration.");
            }

            return typeMap.Execute(context, (IMappingEngine)this);
        }
    }
}
