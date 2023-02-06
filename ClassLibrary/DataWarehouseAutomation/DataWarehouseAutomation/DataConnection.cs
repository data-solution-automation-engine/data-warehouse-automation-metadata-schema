using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    /// <summary>
    /// Optional connectivity information, that can be used for either a DataObject or DataQuery. Can be anything (e.g. key, token, string)
    /// </summary>
    public class DataConnection
    {
        #nullable enable

        /// <summary>
        /// The connection information expressed in a key, token or (connection)string.
        /// </summary>
        [JsonProperty("dataConnectionString")]
        public string dataConnectionString { get; set; }

        /// <summary>
        /// The collection of extension Key/Value pairs.
        /// </summary>
        [JsonProperty("extensions", NullValueHandling = NullValueHandling.Ignore)]
        public List<Extension>? extensions { get; set; }
    }
}
