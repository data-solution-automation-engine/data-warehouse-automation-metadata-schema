using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    public class DataObjectMappingList
    {
        [JsonProperty ("dataObjectMappingList")]
        public List<DataObjectMapping> dataObjectMappingList { get; set; }
    }
}
