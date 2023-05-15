using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataWarehouseAutomation;

public class DataObjectMappingList
{
    /// <summary>
    /// The top-level object that contains a list of one or more source-to-target mappings.
    /// </summary>
    [JsonPropertyName("dataObjectMappings")]
    public List<DataObjectMapping> DataObjectMappings { get; set; } = new();
}
