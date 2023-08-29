namespace DataWarehouseAutomation;

/// <summary>
/// The mapping between a source and target data set / table / file.
/// 
/// The DataObjectMapping is the element that defines an individual source-to-target mapping / ETL process. It is a mapping between a source and target object - referred to as DataObjects.
/// The DataObject is in fact a reusable definition in the Json schema.
///
/// This definition is used twice in the DataObjectMapping: as the* SourceDataObject* and as the* TargetDataObject* - both instances of the DataObject class / type.
///
/// The other key component of a DataObjectMapping is the* DataItemMapping*, which describes the column-to-column(or transformation-to-column).
/// The SourceDataObject, TargetDataObject and DataItemMapping are the mandatory components of a DataObjectMapping.There are many other attributes that can be set, and there are mandatory items within the DataObjects and DataItems.These are all described in the Json schema.
/// </summary>
public class DataObjectMapping : IMetadata
{
    /// <summary>
    /// An optional identifier for the Data Object.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Id { get; set; }

    private string _name = "NewDataMappingName";

    /// <summary>
    /// The name of the source-to-target mapping. An optional unique name that identifies the individual mapping.
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Name
    {
        get
        {
            return _name; 

        }
        set
        {
            _name = value;
        }
    }

    [Obsolete]
    [JsonPropertyName("mappingName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? MappingName
    {
        get
        {
            return null;
        }
        set
        {
            if (value != null)
            {
                _name = value;
            }
        }
    }

    /// <summary>
    /// Free-form and optional classification for the mapping for use in ETL generation logic (evaluation).
    /// </summary>
    [JsonPropertyName("classifications")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<DataClassification>? Classifications { get; set; }

    /// <summary>
    /// The source object of the mapping.
    /// </summary>
    [JsonPropertyName("sourceDataObjects")]
    public List<IDataObject> SourceDataObjects { get; set; } = new();

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
    [JsonPropertyName("businessKeyDefinitions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<BusinessKeyDefinition>? BusinessKeyDefinitions { get; set; }

    /// <summary>
    /// Any filtering that needs to be applied to the source-to-target mapping.
    /// </summary>
    [JsonPropertyName("filterCriterion")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
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

    /// <summary>
    /// Free-format notes on the classification.
    /// </summary>
    [JsonPropertyName("notes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Notes { get; set; }

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
        return Name;
    }
    #endregion
}
