namespace DataWarehouseAutomation.DwaModel;

/// <summary>
/// The schema's top-level object is a 'DataObjectMappingList'. It is an array of individual source-to-target mappings called 'DataObjectMappings', commonly referred to as 'mappings'.
/// In code, this means a DataObjectMappingList is defined as a List/<DataObjectMapping/>.
/// 
/// A DataObjectMapping is a unique ETL mapping / transformation that moves, or interprets, data from a given source to a given destination.
/// The decision to start the format with an array / list that contains potentially multiple DataObjectMappings relates to the Data Warehouse virtualization use-case. In these implementations, multiple individual mappings together create a single view object. 
/// As an example, consider the loading of a Core Business Concept / Hub type entity from various different sources.If you would use these different mappings to generate ETL processes you would create one physical ETL object for each mapping. However, if you are seeking to generate a view that represents the target table the result you would use the collection (list) of mappings to generate separate statements that are unioned in the single view object.
/// </summary>
public class DataObjectMappingList
{
    /// <summary>
    /// The top-level object that contains a list of one or more source-to-target mappings.
    /// </summary>
    [JsonPropertyName("dataObjectMappings")]
    public List<DataObjectMapping> DataObjectMappings { get; set; } = new();

    /// <summary>
    /// The name of the Data Object Mapping List. This is typically provided by one of the data object mappings in the list.
    /// It has been added to allow easy identification of the objects in case a list contains many mappings.
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string? Name { get; set; }

    /// <summary>
    /// Free-form and optional classification for the mapping for use in data logistics generation logic.
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
}
