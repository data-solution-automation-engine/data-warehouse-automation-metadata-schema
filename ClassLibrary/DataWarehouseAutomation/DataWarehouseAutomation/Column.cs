using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    /// <summary>
    /// And individual column
    /// </summary>
    public class Column
    {
        [JsonProperty] // Can be used to change the names of properties when they are serialized to JSON by for example [JsonProperty("column_name")]
        public string columnName { get; set; }
        public string columnType { get; set; }
    }
}
