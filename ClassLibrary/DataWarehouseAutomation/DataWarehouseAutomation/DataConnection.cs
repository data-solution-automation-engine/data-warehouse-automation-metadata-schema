using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataWarehouseAutomation;

/// <summary>
/// Optional connectivity information, that can be used for either a DataObject or DataQuery. Can be anything (e.g. key, token, string)
/// </summary>
public class DataConnection
{
    #nullable enable

    /// <summary>
    /// Optional identifier as a string value to allow various identifier approaches.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The connection information expressed in a key, token or (connection)string.
    /// </summary>
    [JsonPropertyName("dataConnectionString")]
    public string DataConnectionString { get; set; } = "NewConnection";

    /// <summary>
    /// The collection of extension Key/Value pairs.
    /// </summary>
    [JsonPropertyName("extensions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<Extension>? Extensions { get; set; }

    #region Methods
    /// <summary>
    /// Use this method to assert an object as a DataConnection (or not).
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public override bool Equals(object? o)
    {
        var other = o as DataConnection;
        return other?.Id == Id;
    }

    /// <summary>
    /// Override to get a hash value that represents the identifier. 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => Id.GetHashCode();

    /// <summary>
    /// String override so that the object returns its value ('connection string').
    /// When an instance of this class is passed to a method that expects a string, the ToString() method will be called implicitly to convert the object to a string, and the value of the "Connection String" property will be returned.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return DataConnectionString;
    }
    #endregion
}
