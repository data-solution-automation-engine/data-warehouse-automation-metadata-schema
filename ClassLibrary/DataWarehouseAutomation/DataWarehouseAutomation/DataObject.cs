using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataWarehouseAutomation;

public class DataObject
{
#nullable enable

    /// <summary>
    /// An optional identifier for the Data Object.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int? Id { get; set; }

    /// <summary>
    /// The mandatory name of the Data Object.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = "NewDataObject";

    /// <summary>
    /// The collection of Data Items associated with this Data Object.
    /// </summary>
    [JsonPropertyName("dataItems")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<dynamic>? DataItems { get; set; }

    /// <summary>
    /// The connection information associated to the Data Object.
    /// </summary>
    [JsonPropertyName("dataObjectConnection")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DataConnection? DataObjectConnection { get; set; }

    /// <summary>
    /// Free-form and optional classification for the Data Object for use in ETL generation logic (evaluation).
    /// </summary>
    [JsonPropertyName("dataObjectClassifications")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<DataClassification>? DataObjectClassifications { get; set; }

    /// <summary>
    /// The collection of extension Key/Value pairs.
    /// </summary>
    [JsonPropertyName("extensions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<Extension>? Extensions { get; set; }
}