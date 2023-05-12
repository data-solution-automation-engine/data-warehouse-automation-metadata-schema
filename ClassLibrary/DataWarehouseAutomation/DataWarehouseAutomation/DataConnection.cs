using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataWarehouseAutomation;

/// <summary>
/// Optional connectivity information, that can be used for either a DataObject or DataQuery. Can be anything (e.g. key, token, string)
/// </summary>
public class DataConnection
{
#nullable enable

    /// <summary>
    /// The connection information expressed in a key, token or (connection)string.
    /// </summary>
    [JsonPropertyName("dataConnectionString")]
    public string DataConnectionString { get; set; } = "NewConnection";

    /// <summary>
    /// The collection of extension Key/Value pairs.
    /// </summary>
    [JsonPropertyName("extensions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<Extension>? Extensions { get; set; }
}
