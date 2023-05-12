using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataWarehouseAutomation;

/// <summary>
/// The individual column-to-column mapping.
/// </summary>
public class DataItemMapping
{
#nullable enable

    [JsonPropertyName("sourceDataItems")]
    public List<dynamic> SourceDataItems { get; set; } = new();

    [JsonPropertyName("targetDataItem")]
    public DataItem TargetDataItem { get; set; } = new() { Name = "NewTargetDataItem" };

    /// <summary>
    /// The collection of extension Key/Value pairs.
    /// </summary>
    [JsonPropertyName("extensions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<Extension>? Extensions { get; set; }

}
