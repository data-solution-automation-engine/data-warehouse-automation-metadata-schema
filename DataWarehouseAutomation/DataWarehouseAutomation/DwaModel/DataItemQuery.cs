﻿namespace DataWarehouseAutomation.DwaModel;

public class DataItemQuery : IDataItem
{
    /// <summary>
    /// An identifier for the Data Query.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Id { get; set; }

    /// <summary>
    /// The name for the query.
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The <see cref="IDataObject"/> to which the data item belongs. This can be used to construct fully qualified names.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public IDataObject? DataObject { get; set; }

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

    [JsonPropertyName("dataType")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? DataType { get; set; }

    [JsonPropertyName("characterLength")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int? CharacterLength { get; set; }

    [JsonPropertyName("numericPrecision")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int? NumericPrecision { get; set; }

    [JsonPropertyName("numericScale")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int? NumericScale { get; set; }

    [JsonPropertyName("ordinalPosition")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int? OrdinalPosition { get; set; }

    [JsonPropertyName("isPrimaryKey")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? IsPrimaryKey { get; set; }

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
    /// Use this method to assert if two Data Item Queries are the same, based on their Ids.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>True if the Data Item Queries are the same, based on their Ids</returns>
    public override bool Equals(object? obj)
    {
        var other = obj as DataItemQuery;
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
