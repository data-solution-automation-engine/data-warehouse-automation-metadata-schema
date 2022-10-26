using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DataWarehouseAutomation
{
    public class DataItem
    {
        [JsonProperty]
        public string name { get; set; } // Mandatory

        /// <summary>
        /// The data object to which the data item belongs. This can be used to construct fully qualified names.
        /// </summary>
        [JsonProperty("dataObject", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DataObject dataObject { get; set; }

        [JsonProperty("dataType", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string dataType { get; set; }

        [JsonProperty("characterLength", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? characterLength { get; set; }

        [JsonProperty("numericPrecision", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? numericPrecision { get; set; }

        [JsonProperty("numericScale", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? numericScale { get; set; }

        [JsonProperty("ordinalPosition", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? ordinalPosition { get; set; }

        [JsonProperty("isPrimaryKey", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool isPrimaryKey { get; set; }

        [JsonProperty("isHardCodedValue", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool isHardCodedValue { get; set; }

        /// <summary>
        /// Free-form and optional classification for the Data Item for use in generation logic (evaluation).
        /// </summary>
        [JsonProperty("dataItemClassification", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Classification> dataItemClassification { get; set; }

        /// <summary>
        /// The collection of extension Key/Value pairs.
        /// </summary>
        [JsonProperty("extensions", NullValueHandling = NullValueHandling.Ignore)]
        public List<Extension> extensions { get; set; }
    }
}
