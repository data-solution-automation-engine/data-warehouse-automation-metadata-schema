using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    /// <summary>
    /// The mapping between a source and target data set / table / file.
    /// </summary>
    public class DataObjectMapping
    {
        [JsonProperty]
        public string mappingName { get; set; }
        public string classification { get; set; }
        public Boolean enabled { get; set; }

        //public string sourceTable { get; set; }
        public DataObject sourceDataObject { get; set; }
        //public string targetTable { get; set; }
        public DataObject targetDataObject { get; set; }

        public string lookupTable { get; set; }
        public string targetTableHashKey { get; set; }

        public List<BusinessKey> businessKey { get; set; }
        public string filterCriterion { get; set; }
        public List<DataItemMapping> dataItemMapping { get; set; }
    }
}
