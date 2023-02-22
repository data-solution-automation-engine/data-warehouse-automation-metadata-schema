using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataWarehouseAutomation;

public class DataItem
{
#nullable enable

    [JsonProperty("name")]
    public string Name { get; set; } = "NewDataItemName"; // Mandatory

    /// <summary>
    /// The data object to which the data item belongs. This can be used to construct fully qualified names.
    /// </summary>
    [JsonProperty("dataObject", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public DataObject? DataObject { get; set; }

    [JsonProperty("dataType", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? DataType { get; set; }

    [JsonProperty("characterLength", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? CharacterLength { get; set; }

    [JsonProperty("numericPrecision", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? NumericPrecision { get; set; }

    [JsonProperty("numericScale", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? NumericScale { get; set; }

    [JsonProperty("ordinalPosition", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? OrdinalPosition { get; set; }

    [JsonProperty("isPrimaryKey", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? IsPrimaryKey { get; set; }

    [JsonProperty("isHardCodedValue", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? IsHardCodedValue { get; set; }

    /// <summary>
    /// Free-form and optional classification for the Data Item for use in generation logic (evaluation).
    /// </summary>
    [JsonProperty("dataItemClassification", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public List<DataClassification>? DataItemClassification { get; set; }

    /// <summary>
    /// The collection of extension Key/Value pairs.
    /// </summary>
    [JsonProperty("extensions", NullValueHandling = NullValueHandling.Ignore)]
    public List<Extension>? Extensions { get; set; }
}
