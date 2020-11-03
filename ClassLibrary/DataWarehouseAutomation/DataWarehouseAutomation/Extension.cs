using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    /// <summary>
    /// A free form key/value pair addition that can contain additional context.
    /// </summary>
    public class Extension
    {
        /// <summary>
        /// The Key in a Key/Value pair.
        /// </summary>
        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string key { get; set; }

        /// <summary>
        /// The Value in a Key/Value pair.
        /// </summary>
        [JsonProperty("value")]
        public string value { get; set; }

        /// <summary>
        /// Any additional information to explain the intent of extension key/value pair.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string description { get; set; }
    }
}
