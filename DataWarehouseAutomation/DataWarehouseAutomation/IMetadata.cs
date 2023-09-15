namespace DataWarehouseAutomation;

/// <summary>
/// IMetadata can be either a <see cref="DataConnection"/>, a <see cref="DataObject"/>, or a <see cref="DataObjectMapping"/>.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "metadataType")]
[JsonDerivedType(typeof(DataConnection), typeDiscriminator: "dataConnection")]
[JsonDerivedType(typeof(DataObject), typeDiscriminator: "dataObject")]
[JsonDerivedType(typeof(DataObjectMapping), typeDiscriminator: "dataObjectMapping")]
public interface IMetadata
{
    /// <summary>
    /// Optional unique identifier in string format, to allow various types of identifiers to be added.
    /// </summary>
    string? Id { get; set; }

    /// <summary>
    /// Mandatory name for the metadata object.
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Optional free-format notes that can be added to the metadata object.
    /// </summary>
    string? Notes { get; set; }

    /// <summary>
    /// A list of user-configurable extensions as key/value pairs.
    /// </summary>
    List<Extension>? Extensions { get; set; }
}
