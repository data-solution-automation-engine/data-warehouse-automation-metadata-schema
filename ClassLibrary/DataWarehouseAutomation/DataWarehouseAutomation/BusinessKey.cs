using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    /// <summary>
    /// A Business Key, which consists of one or more components (column mappings) and has its own surrogate key.
    /// </summary>
    public class BusinessKey
    {
        [JsonProperty]
        public List<ColumnMapping> businessKeyComponentMapping { get; set; }
        public string surrogateKey { get; set; }
    }
}
