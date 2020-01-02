using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    /// <summary>
    /// And individual column
    /// </summary>
    public class DataItem
    {
        [JsonProperty] // Can be used to change the names of properties when they are serialized to JSON by for example [JsonProperty("column_name")]
        public string name { get; set; }
        public string dataType { get; set; }
    }
}
