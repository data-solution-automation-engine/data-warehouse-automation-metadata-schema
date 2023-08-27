namespace DataWarehouseAutomation;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "metadataType")]
[JsonDerivedType(typeof(DataConnection), typeDiscriminator: "dataConnection")]
[JsonDerivedType(typeof(DataObject), typeDiscriminator: "dataObject")]
[JsonDerivedType(typeof(DataObjectMapping), typeDiscriminator: "dataObjectMapping")]
public interface IMetadata
{
    string Id { get; set; }
    string Name { get; set; }
    string? Notes { get; set; }
    List<Extension>? Extensions { get; set; }
}
