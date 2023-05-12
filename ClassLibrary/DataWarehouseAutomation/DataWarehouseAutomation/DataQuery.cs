using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataWarehouseAutomation;

public class DataQuery
{
    #nullable enable

    /// <summary>
    /// An optional identifier for the Data Object.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The name for the query.
    /// </summary>
    [JsonPropertyName("dataQueryName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string DataQueryName { get; set; } = "NewDataQuery";

    /// <summary>
    /// The actual code that constitutes the query.
    /// </summary>
    [JsonPropertyName("dataQueryCode")]
    public string? DataQueryCode { get; set; }

    /// <summary>
    /// The language that the code was written in (e.g. SQL).
    /// </summary>
    [JsonPropertyName("dataQueryLanguage")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? DataQueryLanguage { get; set; }

    /// <summary>
    /// The connection for the query.
    /// </summary>
    [JsonPropertyName("dataQueryConnection")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DataConnection? DataQueryConnection { get; set; }

    /// <summary>
    /// Free-form and optional classification for the Data Query for use in generation logic (evaluation).
    /// </summary>
    [JsonPropertyName("dataQueryClassification")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<DataClassification>? DataQueryClassification { get; set; }

    /// <summary>
    /// The collection of extension Key/Value pairs.
    /// </summary>
    [JsonPropertyName("extensions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<Extension>? Extensions { get; set; }
}
