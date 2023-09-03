namespace DataWarehouseAutomation;

/// <summary>
/// The definition of a data set, file, or table.
/// The Data Object can be the 'source' or 'target' in a Data Object Mapping.
/// A Data Object which acts as target in one mapping, can be a source in another mapping, building up the data logistics lineage.
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
    /// Free-form and optional classification for the Data Object for use in ETL generation logic (evaluation).
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
    /// Use this method to assert if two DataObjects are the same, based on their Id.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>bool</returns>
    public override bool Equals(object? obj)
    {
        var other = obj as DataObject;
        return other?.Id == Id;
    }
    /// <summary>
    /// Override to get a hash value that represents the identifier.
    /// </summary>
    /// <returns>int</returns>
    public override int GetHashCode() => (Id?.GetHashCode()) ?? 0;

    /// <summary>
    /// String override so that the object returns its value ('name').
    /// When an instance of this class is passed to a method that expects a string, the ToString() method will be called implicitly to convert the object to a string, and the value of the "Name" property will be returned.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Name;
    }
    #endregion
}
