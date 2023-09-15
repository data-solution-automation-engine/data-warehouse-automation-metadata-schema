namespace DataWarehouseAutomation;

/// <summary>
/// Used to define a list of classifications (labels) and notes to add to various components of the schema definition.
/// </summary>
public class DataClassification
{
    /// <summary>
    /// Optional identifier as a string value to allow various identifier approaches.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Id { get; set; }

    /// <summary>
    /// The mandatory classification description, usually a keyword used for automation purposes.
    /// </summary>
    [JsonPropertyName("classification")]
    public string Classification { get; set; } = "NewClassification";

    /// <summary>
    /// Free-format notes on the Data Classification.
    /// </summary>
    [JsonPropertyName("notes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Notes { get; set; }

    #region Methods
    /// <summary>
    /// Use this method to assert if two Data Classifications are the same, based on their Ids.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>True if the Data Classifications are the same, based on their Ids</returns>
    public override bool Equals(object? obj)
    {
        var other = obj as DataClassification;
        return other?.Id == Id;
    }

    /// <summary>
    /// Override to get a hash value that represents the identifier.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code</returns>
    public override int GetHashCode() => (Id?.GetHashCode()) ?? 0;

    /// <summary>
    /// String override so that the object returns the classification value ('classification').
    /// When an instance of this class is passed to a method that expects a string, the ToString() method will be called implicitly to convert the object to a string, and the value of the "Classification" property will be returned.
    /// </summary>
    /// <returns>The Classification value</returns>
    public override string ToString()
    {
        return Classification;
    }
    #endregion
}
