using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    /// <summary>
    /// The individual column-to-column mapping
    /// </summary>
    public class ColumnMapping
    {
        [JsonProperty]
        public Column sourceColumn { get; set; }
        public Column targetColumn { get; set; }
    }
}
