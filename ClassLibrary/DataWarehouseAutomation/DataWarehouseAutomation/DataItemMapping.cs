using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    /// <summary>
    /// The individual column-to-column mapping.
    /// </summary>
    public class DataItemMapping
    {
        [JsonProperty]
        public List<dynamic> sourceDataItems { get; set; }

        [JsonProperty]
        public DataItem targetDataItem { get; set; }

        /// <summary>
        /// The collection of extension Key/Value pairs.
        /// </summary>
        [JsonProperty("extensions", NullValueHandling = NullValueHandling.Ignore)]
        public List<Extension> extensions { get; set; }

    }
}
