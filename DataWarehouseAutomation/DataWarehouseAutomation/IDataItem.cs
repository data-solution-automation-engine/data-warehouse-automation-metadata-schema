namespace DataWarehouseAutomation;

/// <summary>
/// IDataItem can be either a <see cref="DataItem"/> or <see cref="DataItemQuery"/>.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "dataItemType")]
[JsonDerivedType(typeof(DataItem), typeDiscriminator: "dataItem")]
[JsonDerivedType(typeof(DataItemQuery), typeDiscriminator: "dataItemQuery")]
public interface IDataItem
{
    /// <summary>
    /// Optional identifier as a string value to allow various identifier approaches.
    /// </summary>
    string Id { get; set; }
    string Name { get; set; }

    string? DataType { get; set; }
    int? CharacterLength { get; set; }

    int? NumericPrecision { get; set; }
    int? NumericScale { get; set; }

    bool? IsPrimaryKey { get; set; }
    int? OrdinalPosition { get; set; }

    IDataObject DataObject { get; set; }

    List<DataClassification> Classifications { get; set; }

    /// <summary>
    /// The collection of extension Key/Value pairs.
    /// </summary>
    List<Extension>? Extensions { get; set; }

    /// <summary>
    /// Free-format notes.
    /// </summary>
    string? Notes { get; set; }
}