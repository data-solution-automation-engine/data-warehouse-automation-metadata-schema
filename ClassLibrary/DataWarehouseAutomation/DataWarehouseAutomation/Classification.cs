using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    /// <summary>
    /// A free form list of classifications (labels) and notes to add to a source-to-target mapping
    /// </summary>
    public class Classification
    {
        /// <summary>
        /// Optional identifier for the classification.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int id { get; set; }

        /// <summary>
        /// Type mandatory description, usually a keyword used for automation purposes.
        /// </summary>
        [JsonProperty("classification")]
        public string classification { get; set; }

        /// <summary>
        /// Free-format notes on the classification.
        /// </summary>
        [JsonProperty("notes", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string notes { get; set; }
    }
}
