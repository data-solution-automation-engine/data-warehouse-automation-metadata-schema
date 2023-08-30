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
    string? Id { get; set; }

    /// <summary>
    /// The name of the IDataItem.
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// The data type of the IDataItem.
    /// E.g. VARCHAR, int, text.
    /// </summary>
    string? DataType { get; set; }

    /// <summary>
    /// The length, number of characters for the IDataItem.
    /// </summary>
    int? CharacterLength { get; set; }

    /// <summary>
    /// The precision for the IDataItem.
    /// </summary>
    int? NumericPrecision { get; set; }

    /// <summary>
    /// The number of decimal points for the IDataItem.
    /// </summary>
    int? NumericScale { get; set; }

    /// <summary>
    /// An indicator to flag whether this IDataItem is part of a Primary Key.
    /// </summary>
    bool? IsPrimaryKey { get; set; }

    /// <summary>
    /// The position of the IDataItem in a Data Object or Data Object Query.
    /// </summary>
    int? OrdinalPosition { get; set; }

    /// <summary>
    /// A reference to the parent Data Object.
    /// </summary>
    IDataObject? DataObject { get; set; }

    /// <summary>
    /// Any custom labels or classifications that require to be applied to the IDataItem.
    /// </summary>
    List<DataClassification>? Classifications { get; set; }

    /// <summary>
    /// The collection of extension Key/Value pairs.
    /// </summary>
    List<Extension>? Extensions { get; set; }

    /// <summary>
    /// Free-format notes.
    /// </summary>
    string? Notes { get; set; }
}
