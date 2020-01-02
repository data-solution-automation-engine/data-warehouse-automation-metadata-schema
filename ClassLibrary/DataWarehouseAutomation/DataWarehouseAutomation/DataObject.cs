using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataWarehouseAutomation
{
    public class DataObject
    {
        [JsonProperty]
        public int dataObjectid { get; set; }
        public string dataObjectName { get; set; }
        public BusinessKey businessKey { get; set; }
    }
}
