namespace DataWarehouseAutomation.DwaModel;

/// <summary>
/// A relationship from one data object to another.
/// This can apply at conceptual, logical, and physical level.
/// The intent of this class is to support lineage relationships (e.g. parent/child) as well as foreign keys and sub- and supertypes.
/// </summary>
public class Relationship
{
    #region Properties
    /// <summary>
    /// An optional identifier for the relationship.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The mandatory name of the relationship.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; } = string.Empty;

    /// <summary>
    /// The related data object, <see cref="IDataObject"/>
    /// This is nullable to allow creating the object before selecting the target / 'to' object, the relatedDataObject.
    /// Conceptually, a relationship is expected to have a data object target.
    /// </summary>
    [JsonPropertyName("relatedDataObject")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IDataObject? RelatedDataObject { get; set; }

    /// <summary>
    /// The collection of individual attribute (column or query) mappings, containing the data items that apply to the relationship.
    /// </summary>
    [JsonPropertyName("dataItemMappings")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<DataItemMapping>? DataItemMappings { get; set; }

    /// <summary>
    /// The type of relationship. This is a free-format field that acts as a label.
    /// For example, parent, child, grandfather, or lookups.
    /// </summary>
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Type { get; set; }

    /// <summary>
    /// Cardinality of the relationship, e.g. 0 or 1 to many, 1 (and only one) to 1, or zero or many.
    /// </summary>
    [JsonPropertyName("cardinality")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Cardinality? Cardinality { get; set; }

    /// <summary>
    /// Free-form and optional classification for the relationship.
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
    #endregion

    #region Methods
    /// <summary>
    /// Used to assert whether two Entities are the same, based on their Ids.
    /// </summary>
    /// <param name="obj">Comparison Entity</param>
    /// <returns>true if the Entities have the same Id</returns>
    public override bool Equals(object? obj)
    {
        var other = obj as Relationship;
        return other?.Id == Id;
    }

    /// <summary>
    /// Generates a Hash Code derived from the Entity's Id.
    /// </summary>
    /// <returns>Hash Code</returns>
    public override int GetHashCode() => Id?.GetHashCode() ?? 0;

    /// <summary>
    /// String representation override for the Entity
    /// </summary>
    /// <returns>The Name Attribute</returns>
    public override string ToString()
    {
        return Name ?? string.Empty;
    }
    #endregion
}
