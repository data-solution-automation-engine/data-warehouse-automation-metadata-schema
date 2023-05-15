using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataWarehouseAutomation;

/// <summary>
/// A Business Key, which consists of one or more components (column mappings) and has its own surrogate key.
/// A Business Key is a special column, or combination of columns, that is defined separately outside of regular data item mappings.
/// </summary>
public class BusinessKeyDefinition
{
    #nullable enable

    /// <summary>
    /// Optional identifier as a string value to allow various identifier approaches.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Items that define the Business Key e.g. the collection of columns for a Business Key.
    /// </summary>
    [JsonPropertyName("businessKeyComponentMapping")]
    public List<DataItemMapping> BusinessKeyComponentMapping { get; set; } = new();

    /// <summary>
    /// An optional label for the end result e.g. the target business key attribute.
    /// </summary>
    [JsonPropertyName("surrogateKey")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? SurrogateKey { get; set; }

    /// <summary>
    /// Free-form and optional classification for the Business Key for use in generation logic (evaluation).
    /// </summary>
    [JsonPropertyName("businessKeyClassification")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<DataClassification>? BusinessKeyClassification { get; set; }

    /// <summary>
    /// The collection of extension Key/Value pairs.
    /// </summary>
    [JsonPropertyName("extensions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<Extension>? Extensions { get; set; }
}