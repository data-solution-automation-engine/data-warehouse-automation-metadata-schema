using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    /// <summary>
    /// A Business Key, which consists of one or more components (column mappings) and has its own surrogate key.
    /// </summary>
    public class BusinessKey
    {
        /// <summary>
        /// Items that define the Business Key e.g. the collection of columns for a Business Key.
        /// </summary>
        [JsonProperty("businessKeyComponentMapping")]
        public List<DataItemMapping> businessKeyComponentMapping { get; set; }

        /// <summary>
        /// An optional label for the end result e.g. the target business key attribute.
        /// </summary>
        [JsonProperty("surrogateKey")]
        public string surrogateKey { get; set; }
    }
}
