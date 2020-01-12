using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    public class DataObjectMappingList
    {
        /// <summary>
        /// The top-level object that contains a list of one or more source-to-target mappings.
        /// </summary>
        [JsonProperty ("dataObjectMappingList")]
        public List<DataObjectMapping> dataObjectMappingList { get; set; }
    }
}
