using System.Text.Json.Serialization.Metadata;

namespace DataWarehouseAutomation.Utils;

public static class DefaultJsonOptions
{
    /// <summary>
    /// Use as default when serializing objects to json strings/files
    /// </summary>
    public static JsonSerializerOptions SerializerOptions => new()
    {
        TypeInfoResolver = new DefaultJsonTypeInfoResolver
        {
            Modifiers = { UtilityFunctions.DefaultValueModifier }
        },
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    /// <summary>
    /// Use as default when deserializing json strings/files to objects
    /// </summary>
    public static JsonSerializerOptions DeserializerOptions => new()
    {
        AllowTrailingCommas = true,
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
    };
}
