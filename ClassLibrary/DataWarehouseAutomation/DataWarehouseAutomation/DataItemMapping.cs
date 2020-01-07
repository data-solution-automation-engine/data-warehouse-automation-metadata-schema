using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    /// <summary>
    /// The individual column-to-column mapping
    /// </summary>
    public class DataItemMapping
    {
        [JsonProperty]
        public Column sourceDataItem { get; set; }
        public Column targetDataItem { get; set; }
    }
}
