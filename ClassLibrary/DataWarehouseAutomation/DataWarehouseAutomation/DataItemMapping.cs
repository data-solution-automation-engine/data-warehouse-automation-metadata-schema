using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace DataWarehouseAutomation;

/// <summary>
/// The individual column-to-column mapping.
/// </summary>
public class DataItemMapping
{
#nullable enable

    /// <summary>
    /// Optional identifier as a string value to allow various identifier approaches.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("sourceDataItems")]
    public List<dynamic> SourceDataItems { get; set; } = new();

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
    /// Use this method to assert an object as a DataItemMapping (or not).
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public override bool Equals(object? o)
    {
        var other = o as DataItemMapping;
        return other?.Id == Id;
    }

    /// <summary>
    /// Override to get a hash value that represents the identifier. 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => Id.GetHashCode();

    /// <summary>
    /// String override so that the object returns its value ('name of the target data item').
    /// When an instance of this class is passed to a method that expects a string, the ToString() method will be called implicitly to convert the object to a string, and the value of the Data Item "Name" property will be returned.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return TargetDataItem.Name;
    }
    #endregion
}
