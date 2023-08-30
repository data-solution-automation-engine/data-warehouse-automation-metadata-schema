namespace DataWarehouseAutomation;

/// <summary>
/// IDataObject can be either a <see cref="DataObject"/> or <see cref="DataObjectQuery"/>.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "dataObjectType")]
[JsonDerivedType(typeof(DataObject), typeDiscriminator: "dataObject")]
[JsonDerivedType(typeof(DataObjectQuery), typeDiscriminator: "dataObjectQuery")]
public interface IDataObject
{
    /// <summary>
    /// Optional identifier as a string value to allow various identifier approaches.
    /// </summary>
    string? Id { get; set; }

    /// <summary>
    /// The name of the Data Object or Data Object Query.
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// The connection that is used for the Data Object or Data Object Query.
    /// </summary>
    DataConnection? DataConnection { get; set; }
}
