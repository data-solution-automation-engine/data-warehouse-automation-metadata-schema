namespace DataWarehouseAutomation.Utils;

public static class UtilityFunctions
{
    /// <summary>
    /// Filter out certain defaults and null values from the JSON serialization,
    /// e.g. don't serialize empty collections, empty strings, null values, etc.
    /// </summary>
    /// <param name="type_info"></param>
    public static void DefaultValueModifier(JsonTypeInfo type_info)
    {
        foreach (var property in type_info.Properties)
        {
            // if the property is a collection, only serialize if it contains items
            if (typeof(ICollection).IsAssignableFrom(property.PropertyType))
            {
                property.ShouldSerialize = (_, val) => val is ICollection collection && collection.Count > 0;
            }
            // if it is a string and the value is null or empty, don't serialize
            else if (property.PropertyType == typeof(string))
            {
                property.ShouldSerialize = (_, val) => val is string str && !string.IsNullOrEmpty(str);
            }
            // if it is a nullable string and the value is null or empty, don't serialize
            else if (Nullable.GetUnderlyingType(property.PropertyType) == typeof(string))
            {
                property.ShouldSerialize = (_, val) => val is string str && !string.IsNullOrEmpty(str);
            }
            // don't serialize default bool false
            else if (property.PropertyType == typeof(bool))
            {
                property.ShouldSerialize = (_, val) => val is bool b && b;
            }
            // don't serialize nullable bool false
            else if (Nullable.GetUnderlyingType(property.PropertyType) == typeof(bool))
            {
                property.ShouldSerialize = (_, val) => val is bool b && b;
            }
        }
    }
}
