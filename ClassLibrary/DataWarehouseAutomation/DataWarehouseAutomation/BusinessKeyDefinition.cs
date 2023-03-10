using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataWarehouseAutomation;

/// <summary>
/// A Business Key, which consists of one or more components (column mappings) and has its own surrogate key.
/// A Business Key is a special column, or combination of columns, that is defined separately outside of regular data item mappings.
/// </summary>
public class BusinessKeyDefinition
{
    /// <summary>
    /// Items that define the Business Key e.g. the collection of columns for a Business Key.
    /// </summary>
    [JsonProperty("businessKeyComponentMapping")]
    public List<DataItemMapping> BusinessKeyComponentMapping { get; set; } = new();

#nullable enable

    /// <summary>
    /// An optional label for the end result e.g. the target business key attribute.
    /// </summary>
    [JsonProperty("surrogateKey", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? SurrogateKey { get; set; }

    /// <summary>
    /// Free-form and optional classification for the Business Key for use in generation logic (evaluation).
    /// </summary>
    [JsonProperty("businessKeyClassification", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public List<DataClassification>? BusinessKeyClassification { get; set; }

    /// <summary>
    /// The collection of extension Key/Value pairs.
    /// </summary>
    [JsonProperty("extensions", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public List<Extension>? Extensions { get; set; }
}