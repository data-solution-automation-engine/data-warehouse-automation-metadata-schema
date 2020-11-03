using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    /// <summary>
    /// The individual column-to-column mapping.
    /// </summary>
    public class DataItemMapping
    {
        [JsonProperty]
        public DataItem sourceDataItem { get; set; }

        [JsonProperty("sourceDataQuery", NullValueHandling = NullValueHandling.Ignore)]
        public DataQuery sourceDataQuery { get; set; }

        [JsonProperty]
        public DataItem targetDataItem { get; set; }

    }
}
