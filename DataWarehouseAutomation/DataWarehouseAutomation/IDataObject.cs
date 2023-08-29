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
    /// DataObject Name.
    /// </summary>
    string Name { get; set; }

    DataConnection? DataConnection { get; set; }
}
