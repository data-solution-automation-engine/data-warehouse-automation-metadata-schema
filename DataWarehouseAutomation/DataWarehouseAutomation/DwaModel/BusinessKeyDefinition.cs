namespace DataWarehouseAutomation.DwaModel;

/// <summary>
/// A business key definition is a special object that is defined as an optional property of a data object mapping.
/// In other words, the business key definition can be a part of describing the relationship between a source data object and a target data object.
/// </summary>
public class BusinessKeyDefinition
{
    /// <summary>
    /// Optional identifier as a string value to allow various identifier approaches.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Id { get; set; }

    /// <summary>
    /// The optional name of the business key definition.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Items that define the Business Key e.g. the collection of columns for a business key.
    /// The integer value defines the order of the components in the business key definition.
    /// The order is significant, and can be recorded as part of the tuple definition.
    /// </summary>
    [JsonPropertyName("businessKeyComponents")]
    public List<Tuple<int, IDataItem>> BusinessKeyComponents { get; set; } = new();

    /// <summary>
    /// An optional label for the end result e.g. the target business key attribute.
    /// </summary>
    [JsonPropertyName("surrogateKey")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? SurrogateKey { get; set; }

    /// <summary>
    /// Free-form and optional classification for the business key for use in generation logic (evaluation).
    /// </summary>
    [JsonPropertyName("classifications")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<DataClassification>? Classifications { get; set; }

    /// <summary>
    /// The collection of extension key/value pairs.
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
}
