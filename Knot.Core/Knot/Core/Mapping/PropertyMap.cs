using System;
using System.Reflection;
using Knot.Core.Exceptions;

namespace Knot.Core.Mapping
{
    /// <summary>
    /// Represents a mapping configuration between two properties.
    /// </summary>
    public class PropertyMap
  {
 /// <summary>
        /// Gets the source property information.
        /// </summary>
        public PropertyInfo SourceProperty { get; }

        /// <summary>
        /// Gets the destination property information.
    /// </summary>
        public PropertyInfo DestinationProperty { get; }

        /// <summary>
     /// Gets or sets the custom value resolver function.
  /// </summary>
        public Func<object, object> ValueResolver { get; set; }

 /// <summary>
        /// Initializes a new instance of the PropertyMap class.
  /// </summary>
     /// <param name="sourceProperty">The source property.</param>
        /// <param name="destinationProperty">The destination property.</param>
    public PropertyMap(PropertyInfo sourceProperty, PropertyInfo destinationProperty)
        {
         SourceProperty = sourceProperty;
            DestinationProperty = destinationProperty ?? throw new ArgumentNullException(nameof(destinationProperty));
        }

        /// <summary>
    /// Executes the property mapping from source to destination.
    /// </summary>
    /// <param name="source">The source object.</param>
        /// <param name="destination">The destination object.</param>
        public void Execute(object source, object destination)
        {
  if (source == null)
 throw new ArgumentNullException(nameof(source));

   if (destination == null)
 throw new ArgumentNullException(nameof(destination));

 if (!DestinationProperty.CanWrite)
  return;

            object value;

            if (ValueResolver != null)
            {
       value = ValueResolver(source);
      }
            else if (SourceProperty != null && SourceProperty.CanRead)
        {
  value = SourceProperty.GetValue(source);
      }
      else
            {
 return;
  }

        try
            {
  DestinationProperty.SetValue(destination, value);
  }
            catch (ArgumentException ex)
            {
            throw new MappingException(
         $"Failed to set property '{DestinationProperty.Name}'. " +
   $"Type mismatch or conversion error.", ex);
         }
      }
    }
}
