using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace DataWarehouseAutomation;

/// <summary>
/// The mapping between a source and target data set / table / file.
/// </summary>
public class DataObjectMapping
{
    #nullable enable

    /// <summary>
    /// An optional identifier for the Data Object.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The name of the source-to-target mapping. An optional unique name that identifies the individual mapping.
    /// </summary>
    [JsonPropertyName("mappingName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? MappingName { get; set; }

    /// <summary>
    /// Free-form and optional classification for the mapping for use in ETL generation logic (evaluation).
    /// </summary>
    [JsonPropertyName("mappingClassifications")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<DataClassification>? MappingClassifications { get; set; }

    /// <summary>
    /// The source object of the mapping.
    /// </summary>
    [JsonPropertyName("sourceDataObjects")]
    public List<dynamic> SourceDataObjects { get; set; } = new();

    /// <summary>
    /// The target object of the mapping.
    /// </summary>
    [JsonPropertyName("targetDataObject")]
    public DataObject TargetDataObject { get; set; } = new() { Name = "NewTargetDataObject" };

    /// <summary>
    /// Optional associated data object (collection) for purposes other than source- and target relationship.
    /// For example Lookups, merge joins etc.
    /// </summary>
    [JsonPropertyName("relatedDataObjects")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<DataObject>? RelatedDataObjects { get; set; }

    /// <summary>
    /// The collection of individual attribute (column) mappings.
    /// </summary>
    [JsonPropertyName("dataItemMappings")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<DataItemMapping>? DataItemMappings { get; set; }

    /// <summary>
    /// The definition of the Business Key for the source-to-target mapping.
    /// </summary>
    [JsonPropertyName("businessKeys")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<BusinessKeyDefinition>? BusinessKeys { get; set; }

    /// <summary>
    /// Any filtering that needs to be applied to the source-to-target mapping.
    /// </summary>
    [JsonPropertyName("filterCriterion")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]

    #nullable enable
    public string? FilterCriterion { get; set; }

    /// <summary>
    /// An indicator (boolean) which can capture enabling / disabling of (the usage of) an individual source-to-target mapping.
    /// </summary>
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? Enabled { get; set; }

    /// <summary>
    /// The collection of extension Key/Value pairs.
    /// </summary>
    [JsonPropertyName("extensions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<Extension>? Extensions { get; set; }

    #region Methods
    /// <summary>
    /// Use this method to assert an object as a Data Object Mapping (or not).
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public override bool Equals(object? o)
    {
        var other = o as DataObjectMapping;
        return other?.Id == Id;
    }

    /// <summary>
    /// Override to get a hash value that represents the identifier. 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => Id.GetHashCode();

    /// <summary>
    /// String override so that the object returns its value ('MappingName').
    /// When an instance of this class is passed to a method that expects a string, the ToString() method will be called implicitly to convert the object to a string, and the value of the "MappingName" property will be returned.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return MappingName;
    }
    #endregion
}