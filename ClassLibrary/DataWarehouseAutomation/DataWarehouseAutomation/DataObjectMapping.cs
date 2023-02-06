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
        #nullable enable

        /// <summary>
        /// The name of the source-to-target mapping. An optional unique name that identifies the individual mapping.
        /// </summary>
        [JsonProperty("mappingName", NullValueHandling = NullValueHandling.Ignore)]
        public string? mappingName { get; set; }

        /// <summary>
        /// Free-form and optional classification for the mapping for use in ETL generation logic (evaluation).
        /// </summary>
        [JsonProperty("mappingClassifications", NullValueHandling = NullValueHandling.Ignore)]
        public List<Classification>? mappingClassifications { get; set; }

        /// <summary>
        /// The source object of the mapping.
        /// </summary>
        [JsonProperty("sourceDataObjects")]
        public List<dynamic> sourceDataObjects { get; set; }

        /// <summary>
        /// The target object of the mapping.
        /// </summary>
        [JsonProperty("targetDataObject")]
        public DataObject targetDataObject { get; set; }

        /// <summary>
        /// Optional associated data object (collection) for purposes other than source- and target relationship.
        /// For example Lookups, merge joins etc.
        /// </summary>
        [JsonProperty("relatedDataObjects", NullValueHandling = NullValueHandling.Ignore)]
        public List<DataObject>? relatedDataObjects { get; set; }

        /// <summary>
        /// The collection of individual attribute (column) mappings.
        /// </summary>
        [JsonProperty("dataItemMappings", NullValueHandling = NullValueHandling.Ignore)]
        public List<DataItemMapping>? dataItemMappings { get; set; }

        /// <summary>
        /// The definition of the Business Key for the source-to-target mapping.
        /// </summary>
        [JsonProperty("businessKeys", NullValueHandling = NullValueHandling.Ignore)]
        public List<BusinessKey>? businessKeys { get; set; }

        /// <summary>
        /// Any filtering that needs to be applied to the source-to-target mapping.
        /// </summary>
        [JsonProperty("filterCriterion", NullValueHandling = NullValueHandling.Ignore)]
        #nullable enable
        public string? filterCriterion { get; set; }

        /// <summary>
        /// An indicator (boolean) which can capture enabling / disabling of (the usage of) an individual source-to-target mappping.
        /// </summary>
        [JsonProperty("enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? enabled { get; set; }

        /// <summary>
        /// The collection of extension Key/Value pairs.
        /// </summary>
        [JsonProperty("extensions", NullValueHandling = NullValueHandling.Ignore)]
        public List<Extension>? extensions { get; set; }
    }
}