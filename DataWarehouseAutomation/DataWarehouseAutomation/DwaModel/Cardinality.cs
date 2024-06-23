﻿using System.Buffers.Text;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DataWarehouseAutomation.DwaModel;

/// <summary>
/// This object captures the cardinality and ordinality of a relationship.
/// Cardinality refers to the uniqueness of data values contained in a column (attribute) of a database table.
/// It defines the number of occurrences of one entity that are associated with the number of occurrences of another entity through a relationship.
/// </summary>
public class Cardinality : IMetadata
{
    /// <summary>
    /// Optional identifier as a string value to allow various identifier approaches.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Id { get; set; }

    /// <summary>
    /// Optional information to name a certain cardinality construct. For example
    /// One-to-One (1:1): {"from": {"min": 1, "max": 1}, "to": {"min": 1, "max": 1}}
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// The 'from' component in the cardinality, e.g. the '1' in 1 to many.
    /// </summary>
    public CardinalityRange FromRange { get; set; } = new CardinalityRange { Min = 1, Max = "1" };

    /// <summary>
    /// The 'to' component in the cardinality, e.g. the 'many' in 1 to many.
    /// </summary>
    public CardinalityRange ToRange { get; set; } = new CardinalityRange { Min = 1, Max = "N" };

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

/// <summary>
/// The possible range for a from/to component for the cardinality.
/// For eaxmple "min": 1, "max": "N"
/// This way, you can define at least 1 to many. Or 0 or 1 to 1.
/// </summary>
public class CardinalityRange
{
    public int Min { get; set; }
    public string Max { get; set; }
}