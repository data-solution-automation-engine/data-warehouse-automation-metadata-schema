using Newtonsoft.Json;

namespace DataWarehouseAutomation;

/// <summary>
/// A free form key/value pair addition that can contain additional context.
/// </summary>
public class Extension
{
#nullable enable

    /// <summary>
    /// The Key in a Key/Value pair.
    /// </summary>
    [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Key { get; set; } = "NewExtension";

    /// <summary>
    /// The Value in a Key/Value pair.
    /// </summary>
    [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Value { get; set; }

    /// <summary>
    /// Any additional, optional, information to explain the intent of extension key/value pair.
    /// </summary>
    [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Description { get; set; }
}
