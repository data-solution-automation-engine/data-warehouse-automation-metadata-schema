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
        [JsonProperty("mappingClassifications")]
        public List<Classification> mappingClassifications { get; set; }

        /// <summary>
        /// The source object of the mapping.
        /// </summary>
        [JsonProperty("sourceDataObjects")]
        public List<DataObject> sourceDataObjects { get; set; }

        /// <summary>
        /// The target object of the mapping.
        /// </summary>
        [JsonProperty("targetDataObject")]
        public DataObject targetDataObject { get; set; }

        /// <summary>
        /// Optional associated data object (collection) for purposes other than source- and target relationship.
        /// For example Lookups, merge joins etc.
        /// </summary>
        [JsonProperty("relatedDataObjects")]
        public List<DataObject> relatedDataObjects { get; set; }

        /// <summary>
        /// The collection of individual attribute (column) mappings.
        /// </summary>
        [JsonProperty("dataItemMappings", NullValueHandling = NullValueHandling.Ignore)]
        public List<DataItemMapping> dataItemMappings { get; set; }

        /// <summary>
        /// The definition of the Business Key for the source-to-target mapping.
        /// </summary>
        [JsonProperty("businessKeys")]
        public List<BusinessKey> businessKeys { get; set; }

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

        /// <summary>
        /// The collection of extension Key/Value pairs.
        /// </summary>
        [JsonProperty("extensions", NullValueHandling = NullValueHandling.Ignore)]
        public List<Extension> extensions { get; set; }
    }
}