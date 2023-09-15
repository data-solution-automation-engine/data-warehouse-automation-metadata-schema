namespace DataWarehouseAutomation;

/// <summary>
/// The individual column-to-column mapping.
/// </summary>
public class DataItemMapping
{
    /// <summary>
    /// Optional identifier as a string value to allow various identifier approaches.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Id { get; set; }

    [JsonPropertyName("sourceDataItems")]
    public List<IDataItem> SourceDataItems { get; set; } = new();

    [JsonPropertyName("targetDataItem")]
    public DataItem TargetDataItem { get; set; } = new() { Name = "NewTargetDataItem" };

    /// <summary>
    /// The collection of extension Key/Value pairs.
    /// </summary>
    [JsonPropertyName("extensions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<Extension>? Extensions { get; set; }

    #region Methods
    /// <summary>
    /// Use this method to assert if two DataItemMappings are the same, based on their Ids.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>True if the Data Item Mappings are the same, based on their Ids</returns>
    public override bool Equals(object? obj)
    {
        var other = obj as DataItemMapping;
        return other?.Id == Id;
    }

    /// <summary>
    /// Override to get a hash value that represents the identifier.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code</returns>
    public override int GetHashCode() => (Id?.GetHashCode()) ?? 0;

    /// <summary>
    /// String override so that the object returns its value ('name of the target data item').
    /// When an instance of this class is passed to a method that expects a string, the ToString() method will be called implicitly to convert the object to a string, and the value of the Data Item "Name" property will be returned.
    /// </summary>
    /// <returns>The Target Data Item's Name</returns>
    public override string ToString()
    {
        return TargetDataItem.Name;
    }
    #endregion
}
