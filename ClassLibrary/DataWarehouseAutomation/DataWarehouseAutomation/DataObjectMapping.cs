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
        /// <summary>
        /// The name of the source-to-target mapping. An optional unique name that identifies the individual mapping.
        /// </summary>
        [JsonProperty("mappingName")]
        public string mappingName { get; set; }

        /// <summary>
        /// Free-form and optional classification for the mapping for use in ETL generation logic (evaluation).
        /// </summary>
        [JsonProperty("mappingClassification")]
        public List<DataObjectMappingClassification> mappingClassification { get; set; }

        /// <summary>
        /// The source object of the mapping.
        /// </summary>
        [JsonProperty("sourceDataObject")]
        public DataObject sourceDataObject { get; set; }

        /// <summary>
        /// The target object of the mapping.
        /// </summary>
        [JsonProperty("targetDataObject")]
        public DataObject targetDataObject { get; set; }

        /// <summary>
        /// The collection of individual attribute (column) mappings.
        /// </summary>
        [JsonProperty("dataItemMapping")]
        public List<DataItemMapping> dataItemMapping { get; set; }

        /// <summary>
        /// The definition of the Business Key for the source-to-target mapping.
        /// </summary>
        [JsonProperty("businessKey")]
        public List<BusinessKey> businessKey { get; set; }

        /// <summary>
        /// Any filtering that needs to be applied to the source-to-target mapping.
        /// </summary>
        [JsonProperty("filterCriterion")]
        public string filterCriterion { get; set; }

        /// <summary>
        /// An indicator (boolean) which can capture enabling / disabling of (the usage of) an individual source-to-target mappping.
        /// </summary>
        [JsonProperty("enabled")]
        public bool enabled { get; set; }
    }
}