using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    /// <summary>
    /// A free form list of classifications (labels) and notes to add to a source-to-target mapping
    /// </summary>
    public class DataObjectMappingClassification
    {
        [JsonProperty]
        public int id { get; set; }
        public string classification { get; set; }
        public string notes { get; set; }
    }
}
