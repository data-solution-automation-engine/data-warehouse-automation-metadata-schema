using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    /// <summary>
    /// A free form list of classifications (labels) and notes to add to a source-to-target mapping
    /// </summary>
    public class DataClassification
    {
        #nullable enable

        /// <summary>
        /// Optional identifier for the classification.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? Id { get; set; }

        /// <summary>
        /// Type mandatory description, usually a keyword used for automation purposes.
        /// </summary>
        [JsonProperty("classification")]
        public string Classification { get; set; } = "NewClassification";

        /// <summary>
        /// Free-format notes on the classification.
        /// </summary>
        [JsonProperty("notes", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? Notes { get; set; }
    }
}
