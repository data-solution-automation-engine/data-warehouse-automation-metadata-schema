using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataWarehouseAutomation
{
    public class DataObject
    {
        [JsonProperty]
        public int id { get; set; }
        public string name { get; set; }

        public List<DataItem> dataItems { get; set; }

        public DataConnection dataObjectConnection { get; set; }
    }
}
