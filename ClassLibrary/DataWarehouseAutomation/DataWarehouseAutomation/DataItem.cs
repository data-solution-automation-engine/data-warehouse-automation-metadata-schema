using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataWarehouseAutomation
{
    public class DataItem
    {
        [JsonProperty]
        public string name { get; set; } // Mandatory
        public string dataType { get; set; }
        public int characterLength { get; set; }
        public int numericPrecision { get; set; }
        public int numericScale { get; set; }
        public int ordinalPosition { get; set; }
        public bool isPrimaryKey { get; set; }
    }
}
