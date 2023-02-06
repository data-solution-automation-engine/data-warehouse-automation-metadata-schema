using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    public class DataQuery
    {
        #nullable enable

        /// <summary>
        /// An optional name for the query.
        /// </summary>
        [JsonProperty("dataQueryName", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string dataQueryName { get; set; }

        /// <summary>
        /// The actual code that constitutes the query. This is a mandatory field.
        /// </summary>
        [JsonProperty("dataQueryCode")]
        public string? dataQueryCode { get; set; }

        /// <summary>
        /// The language that the code was written in (e.g. SQL).
        /// </summary>
        [JsonProperty("dataQueryLanguage", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? dataQueryLanguage { get; set; }

        /// <summary>
        /// The connection for the query.
        /// </summary>
        [JsonProperty("dataQueryConnection", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DataConnection? dataQueryConnection { get; set; }

        /// <summary>
        /// Free-form and optional classification for the Data Query for use in generation logic (evaluation).
        /// </summary>
        [JsonProperty("dataQueryClassification", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Classification>? dataQueryClassification { get; set; }

        /// <summary>
        /// The collection of extension Key/Value pairs.
        /// </summary>
        [JsonProperty("extensions", NullValueHandling = NullValueHandling.Ignore)]
        public List<Extension>? extensions { get; set; }
    }
}
