using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    public class DataObjectMappingList
    {
        [JsonProperty]
        public List<DataObjectMapping> dataObjectMappingList { get; set; }
        public GenerationSpecificMetadata generationSpecificMetadata { get; set; }
    }

    /// <summary>
    /// Specific metadata related for generation purposes, but which is relevant to use in templates.
    /// </summary>
    public class GenerationSpecificMetadata
    {
        public string selectedDataObject { get; set; }
        public DateTime generationDateTime { get; } = DateTime.Now;
    }
}
