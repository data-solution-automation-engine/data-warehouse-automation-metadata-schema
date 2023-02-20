using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    /// <summary>
    /// The individual column-to-column mapping.
    /// </summary>
    public class DataItemMapping
    {
        #nullable enable

        [JsonProperty("sourceDataItems")]
        public List<dynamic> SourceDataItems { get; set; } = new();

        [JsonProperty("targetDataItem")] 
        public DataItem TargetDataItem { get; set; } = new() {Name = "NewTargetDataItem"};

        /// <summary>
        /// The collection of extension Key/Value pairs.
        /// </summary>
        [JsonProperty("extensions", NullValueHandling = NullValueHandling.Ignore)]
        public List<Extension>? Extensions { get; set; }

    }
}
