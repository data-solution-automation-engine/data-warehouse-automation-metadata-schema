using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataWarehouseAutomation;

public class DataItem
{
#nullable enable

    [JsonPropertyName("name")]
    public string Name { get; set; } = "NewDataItemName"; // Mandatory

    /// <summary>
    /// The data object to which the data item belongs. This can be used to construct fully qualified names.
    /// </summary>
    [JsonPropertyName("dataObject")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DataObject? DataObject { get; set; }

    [JsonPropertyName("dataType")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? DataType { get; set; }

    [JsonPropertyName("characterLength")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int? CharacterLength { get; set; }

    [JsonPropertyName("numericPrecision")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int? NumericPrecision { get; set; }

    [JsonPropertyName("numericScale")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int? NumericScale { get; set; }

    [JsonPropertyName("ordinalPosition")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int? OrdinalPosition { get; set; }

    [JsonPropertyName("isPrimaryKey")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? IsPrimaryKey { get; set; }

    [JsonPropertyName("isHardCodedValue")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? IsHardCodedValue { get; set; }

    /// <summary>
    /// Free-form and optional classification for the Data Item for use in generation logic (evaluation).
    /// </summary>
    [JsonPropertyName("dataItemClassification")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<DataClassification>? DataItemClassification { get; set; }

    /// <summary>
    /// The collection of extension Key/Value pairs.
    /// </summary>
    [JsonPropertyName("extensions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<Extension>? Extensions { get; set; }
}
