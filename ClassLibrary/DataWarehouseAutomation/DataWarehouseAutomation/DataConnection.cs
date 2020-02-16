using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    /// <summary>
    /// Optional connectivity information, that can be used for either a DataObject or DataQuery. Can be anything (e.g. key, token, string)
    /// </summary>
    public class DataConnection
    {
        [JsonProperty]
        public string dataConnectionString { get; set; }
    }
}
