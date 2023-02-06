using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataWarehouseAutomation
{
    public class DataObject
    {
        #nullable enable

        /// <summary>
        /// An optional identifier for the Data Object.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? id { get; set; }

        /// <summary>
        /// The mandatory name of the Data Object.
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }

        /// <summary>
        /// The collection of Data Items associated with this Data Object.
        /// </summary>
        [JsonProperty("dataItems", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<dynamic>? dataItems { get; set; }

        /// <summary>
        /// The connection information associated to the Data Object.
        /// </summary>
        [JsonProperty("dataObjectConnection", NullValueHandling = NullValueHandling.Ignore)]
        public DataConnection? dataObjectConnection { get; set; }

        /// <summary>
        /// Free-form and optional classification for the Data Object for use in ETL generation logic (evaluation).
        /// </summary>
        [JsonProperty("dataObjectClassifications", NullValueHandling = NullValueHandling.Ignore)]
        public List<Classification>? dataObjectClassifications { get; set; }

        /// <summary>
        /// The collection of extension Key/Value pairs.
        /// </summary>
        [JsonProperty("extensions", NullValueHandling = NullValueHandling.Ignore)]
        public List<Extension>? extensions { get; set; }
    }
}
