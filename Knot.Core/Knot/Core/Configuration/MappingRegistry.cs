using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Knot.Core.Mapping;
using Knot.Core.Utilities;

namespace Knot.Core.Configuration
{
    /// <summary>
  /// Registry for managing type mapping configurations.
    /// </summary>
    public class MappingRegistry
    {
        private readonly Dictionary<TypePair, TypeMap> _typeMaps;
      private readonly List<TypeConverter> _typeConverters;

        /// <summary>
    /// Initializes a new instance of the MappingRegistry class.
      /// </summary>
        public MappingRegistry()
        {
         _typeMaps = new Dictionary<TypePair, TypeMap>();
            _typeConverters = new List<TypeConverter>();
   }

        /// <summary>
/// Creates a mapping configuration between two types.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <returns>The created TypeMap.</returns>
        public TypeMap CreateMap<TSource, TDestination>()
        {
     return CreateMap(typeof(TSource), typeof(TDestination));
        }

        /// <summary>
        /// Creates a mapping configuration between two types.
        /// </summary>
        /// <param name="sourceType">The source type.</param>
        /// <param name="destinationType">The destination type.</param>
/// <returns>The created TypeMap.</returns>
 public TypeMap CreateMap(Type sourceType, Type destinationType)
      {
            var typePair = new TypePair(sourceType, destinationType);
            
        if (_typeMaps.ContainsKey(typePair))
      return _typeMaps[typePair];

        var typeMap = new TypeMap(sourceType, destinationType);
          _typeMaps[typePair] = typeMap;

         // Auto-configure property mappings
  AutoConfigurePropertyMaps(typeMap);

return typeMap;
        }

        /// <summary>
        /// Gets a type map for the specified source and destination types.
        /// </summary>
        /// <param name="sourceType">The source type.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <returns>The TypeMap if found; otherwise, null.</returns>
        public TypeMap GetTypeMap(Type sourceType, Type destinationType)
      {
     var typePair = new TypePair(sourceType, destinationType);
    return _typeMaps.ContainsKey(typePair) ? _typeMaps[typePair] : null;
        }

        /// <summary>
        /// Registers a type converter.
        /// </summary>
        /// <param name="converter">The type converter to register.</param>
    public void RegisterConverter(TypeConverter converter)
     {
  if (converter == null)
            throw new ArgumentNullException(nameof(converter));

    _typeConverters.Add(converter);
        }

        private void AutoConfigurePropertyMaps(TypeMap typeMap)
        {
  var sourceProperties = ReflectionHelper.GetPublicProperties(typeMap.SourceType);
            var destinationProperties = ReflectionHelper.GetPublicProperties(typeMap.DestinationType);

          foreach (var destProp in destinationProperties)
        {
        if (!destProp.CanWrite)
          continue;

          var sourceProp = sourceProperties.FirstOrDefault(sp => 
         sp.Name.Equals(destProp.Name, StringComparison.OrdinalIgnoreCase) &&
        sp.CanRead);

           var propertyMap = new PropertyMap(sourceProp, destProp);
        typeMap.PropertyMaps.Add(propertyMap);
            }
        }

    private struct TypePair : IEquatable<TypePair>
{
            public Type SourceType { get; }
            public Type DestinationType { get; }

      public TypePair(Type sourceType, Type destinationType)
            {
  SourceType = sourceType;
                DestinationType = destinationType;
       }

  public bool Equals(TypePair other)
       {
  return SourceType == other.SourceType && DestinationType == other.DestinationType;
            }

  public override bool Equals(object obj)
       {
     return obj is TypePair other && Equals(other);
            }

            public override int GetHashCode()
            {
   unchecked
      {
   return ((SourceType?.GetHashCode() ?? 0) * 397) ^ (DestinationType?.GetHashCode() ?? 0);
 }
            }
        }
 }
}
