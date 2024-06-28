namespace DataWarehouseAutomation.DwaModel;

/// <summary>
/// The possible range for a from/to component for the cardinality.
/// For example "min": "1", "max": "N"
/// This way, you can define "at least 1 to many". Or "0 or 1 to 1".
/// </summary>
public class CardinalityRange
{
    [JsonPropertyName("min")]
    public string? Min { get; set; }

    [JsonPropertyName("max")]
    public string? Max { get; set; }
}
