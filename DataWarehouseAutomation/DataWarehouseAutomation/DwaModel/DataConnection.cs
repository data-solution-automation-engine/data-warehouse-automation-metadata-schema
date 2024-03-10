namespace DataWarehouseAutomation.DwaModel;

/// <summary>
/// Connectivity information, that can be used for either a DataObject or DataObjectQuery.
/// This is be a key, token, reference, connection string and similar.
/// </summary>
public class DataConnection : IMetadata
{
    /// <summary>
    /// Optional identifier as a string value to allow various identifier approaches.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Id { get; set; }

    /// <summary>
    /// The connection information expressed in a key, token or (connection)string.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = "NewConnection";

    /// <summary>
    /// Free-form and optional classification for the Data Item for use in generation logic (evaluation).
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
    /// Use this method to assert if two Data Connections are the same, based on their Ids.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>True if the Data Connections are the same, based on their Ids</returns>
    public override bool Equals(object? obj)
    {
        var other = obj as DataConnection;
        return other?.Id == Id;
    }

    /// <summary>
    /// Override to get a hash value that represents the identifier.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code</returns>
    public override int GetHashCode() => (Id?.GetHashCode()) ?? 0;

    /// <summary>
    /// String override so that the object returns its value ('connection string').
    /// When an instance of this class is passed to a method that expects a string, the ToString() method will be called implicitly to convert the object to a string, and the value of the "Connection String" property will be returned.
    /// </summary>
    /// <returns>The Name</returns>
    public override string ToString()
    {
        return Name;
    }
    #endregion
}
