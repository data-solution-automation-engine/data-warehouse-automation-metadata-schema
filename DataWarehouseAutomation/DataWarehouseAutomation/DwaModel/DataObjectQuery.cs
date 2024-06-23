using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Emit;

namespace DataWarehouseAutomation.DwaModel;

/// <summary>
/// A data query is a bespoke code element that is scoped for a specific function or purpose, and can act as a ‘source’ in a source-target mapping.
/// This applies to both data object and data item level.
/// When acting the data object object, the data query replaces the source data object in the mapping.
/// This is a data object query, which could be a view, script, or procedure that provides the data in the mapping instead of a table or file.
/// </summary>
public class DataObjectQuery : IDataObject
{
    /// <summary>
    /// Identifier for the Data Query.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Id { get; set; }

    /// <summary>
    /// The name for the query.
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Name { get; set; } = "NewDataQuery";

    /// <summary>
    /// The actual code that constitutes the query.
    /// </summary>
    [JsonPropertyName("queryCode")]
    public string? QueryCode { get; set; }

    /// <summary>
    /// The language that the code was written in (e.g. SQL).
    /// </summary>
    [JsonPropertyName("queryLanguage")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? QueryLanguage { get; set; }

    private List<IDataItem> _dataItems = [];
    /// <summary>
    /// The collection of Data Items <see cref="IDataItem"/> associated with this Data Object Query.
    /// </summary>
    [JsonPropertyName("dataItems")]
    [JsonPropertyOrder(order: 40)]
    public List<IDataItem> DataItems
    {
        get
        {
            return _dataItems;
        }
        set
        {
            _dataItems = value;
        }
    }

    /// <summary>
    /// The connection for the query.
    /// </summary>
    [JsonPropertyName("dataConnection")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DataConnection? DataConnection { get; set; }

    /// <summary>
    /// The definition of the Business Key(s) for the Data Object Query.
    /// Being able to record the business key definition 
    /// This serves multiple purposes, but one of them is to support defining a series of business key definitions against the source data object, and reuse these across different data object mappings.
    /// </summary>
    [JsonPropertyName("businessKeyDefinitions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<BusinessKeyDefinition>? BusinessKeyDefinitions { get; set; }

    /// <summary>
    /// Any relationship to other data objects and/or queries.
    /// </summary>
    [JsonPropertyName("relationships")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<Relationship>? Relationships { get; set; }

    /// <summary>
    /// Free-form and optional classification for the Data Query for use in generation logic (evaluation).
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
    /// Use this method to assert if two DataObjectQueries are the same, based on their Ids.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>true if Data Object Queries are the same, based on their Ids</returns>
    public override bool Equals(object? obj)
    {
        var other = obj as DataObjectQuery;
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
    /// <returns></returns>
    public override string ToString()
    {
        return Name;
    }
    #endregion
}
