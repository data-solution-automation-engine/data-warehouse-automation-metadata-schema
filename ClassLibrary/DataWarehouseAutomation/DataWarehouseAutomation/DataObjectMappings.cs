using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    public class DataObjectMappings
    {
        /// <summary>
        /// The top-level object that contains a list of one or more source-to-target mappings.
        /// </summary>
        [JsonProperty ("dataObjectMappings")]
        public List<DataObjectMapping> dataObjectMappings { get; set; }
    }
}
