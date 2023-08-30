namespace DataWarehouseAutomation;

/// <summary>
/// Used to define a list of classifications (labels) and notes to add to a data object mapping.
/// A classification can be used to identify a certain type of data object mapping.
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
    /// Free-format notes on the data classification.
    /// </summary>
    [JsonPropertyName("notes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Notes { get; set; }

    #region Methods
    /// <summary>
    /// Use this method to assert an object as a DataClassification (or not).
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public override bool Equals(object? o)
    {
        var other = o as DataClassification;
        return other?.Id == Id;
    }

    /// <summary>
    /// Override to get a hash value that represents the identifier. 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => Id.GetHashCode();

    /// <summary>
    /// String override so that the object returns the classification value ('classification').
    /// When an instance of this class is passed to a method that expects a string, the ToString() method will be called implicitly to convert the object to a string, and the value of the "Classification" property will be returned.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Classification;
    }
    #endregion
}
