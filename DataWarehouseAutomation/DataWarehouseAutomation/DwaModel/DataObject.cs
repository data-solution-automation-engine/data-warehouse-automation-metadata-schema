namespace DataWarehouseAutomation.DwaModel;

/// <summary>
/// The definition of a data set, file, or table.
/// The Data Object can be the 'source' or 'target' in a <see cref="DataObjectMapping"/>.
/// A Data Object which acts as target in one mapping, can be a source in another mapping,
/// building up the data logistics lineage.
/// </summary>
public class DataObject : IMetadata, IDataObject
{
    /// <summary>
    /// An optional identifier for the Data Object.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Id { get; set; }

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
    public List<IDataItem>? DataItems { get; set; }

    /// <summary>
    /// The connection information associated to the Data Object.
    /// </summary>
    [JsonPropertyName("dataConnection")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DataConnection? DataConnection { get; set; }

    /// <summary>
    /// The definition of the Business Key(s) for the Data Object.
    /// Being able to record the business key definition 
    /// This serves multiple purposes, but one of them is to support defining a series of business key definitions against the source data object, and reuse these across different data object mappings.
    /// The order is stored as well, because in some cases the order of keys is meaningful.
    /// </summary>
    [JsonPropertyName("businessKeyDefinitions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<BusinessKeyDefinitions>? BusinessKeyDefinitions { get; set; }

    /// <summary>
    /// Any relationship to other data objects.
    /// </summary>
    [JsonPropertyName("relationships")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<Relationships>? Relationships { get; set; }

    /// <summary>
    /// Free-form and optional classification for the Data Object.
    /// </summary>
    [JsonPropertyName("classifications")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<DataClassification>? Classifications { get; set; }

    /// <summary>
    /// The collection of extension Key/Value pairs.
    /// </summary>
    [JsonPropertyName("extensions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<Extension>? Extensions { get; set; }

    /// <summary>
    /// Free-format notes.
    /// </summary>
    [JsonPropertyName("notes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Notes { get; set; }

    #region Methods
    /// <summary>
    /// Use this method to assert if two Data Objects are the same, based on their Ids.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>True if the Data Objects are the same, based on their Ids</returns>
    public override bool Equals(object? obj)
    {
        var other = obj as DataObject;
        return other?.Id == Id;
    }
    /// <summary>
    /// Override to get a hash value that represents the identifier.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code</returns>
    public override int GetHashCode() => (Id?.GetHashCode()) ?? 0;

    /// <summary>
    /// String override so that the object returns its value ('name').
    /// When an instance of this class is passed to a method that expects a string, the ToString() method will be called implicitly to convert the object to a string, and the value of the "Name" property will be returned.
    /// </summary>
    /// <returns>The Name</returns>
    public override string ToString()
    {
        return Name;
    }
    #endregion
}
