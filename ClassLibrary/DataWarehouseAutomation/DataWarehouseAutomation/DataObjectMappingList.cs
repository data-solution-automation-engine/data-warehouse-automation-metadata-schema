using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    public class DataObjectMappingList
    {
        [JsonProperty]
        public List<DataObjectMapping> dataObjectMappingList { get; set; }
    }
}
