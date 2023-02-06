using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    /// <summary>
    /// A Business Key, which consists of one or more components (column mappings) and has its own surrogate key.
    /// </summary>
    public class BusinessKey
    {
        #nullable enable

        /// <summary>
        /// Items that define the Business Key e.g. the collection of columns for a Business Key.
        /// </summary>
        [JsonProperty("businessKeyComponentMapping")]
        public List<DataItemMapping> businessKeyComponentMapping { get; set; }

        /// <summary>
        /// An optional label for the end result e.g. the target business key attribute.
        /// </summary>
        [JsonProperty("surrogateKey")]
        public string? surrogateKey { get; set; }

        /// <summary>
        /// Free-form and optional classification for the Business Key for use in generation logic (evaluation).
        /// </summary>
        [JsonProperty("businessKeyClassification", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Classification>? businessKeyClassification { get; set; }

        /// <summary>
        /// The collection of extension Key/Value pairs.
        /// </summary>
        [JsonProperty("extensions", NullValueHandling = NullValueHandling.Ignore)]
        public List<Extension>? extensions { get; set; }
    }
}
