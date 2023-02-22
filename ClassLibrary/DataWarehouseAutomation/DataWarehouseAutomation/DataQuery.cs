using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataWarehouseAutomation;

public class DataQuery
{
#nullable enable

    /// <summary>
    /// The name for the query.
    /// </summary>
    [JsonProperty("dataQueryName", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string DataQueryName { get; set; } = "NewDataQuery";

    /// <summary>
    /// The actual code that constitutes the query.
    /// </summary>
    [JsonProperty("dataQueryCode")]
    public string? DataQueryCode { get; set; }

    /// <summary>
    /// The language that the code was written in (e.g. SQL).
    /// </summary>
    [JsonProperty("dataQueryLanguage", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? DataQueryLanguage { get; set; }

    /// <summary>
    /// The connection for the query.
    /// </summary>
    [JsonProperty("dataQueryConnection", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public DataConnection? DataQueryConnection { get; set; }

    /// <summary>
    /// Free-form and optional classification for the Data Query for use in generation logic (evaluation).
    /// </summary>
    [JsonProperty("dataQueryClassification", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public List<DataClassification>? DataQueryClassification { get; set; }

    /// <summary>
    /// The collection of extension Key/Value pairs.
    /// </summary>
    [JsonProperty("extensions", NullValueHandling = NullValueHandling.Ignore)]
    public List<Extension>? Extensions { get; set; }
}
