using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataWarehouseAutomation
{
    public class DataQuery
    {
        /// <summary>
        /// An optional name for the query.
        /// </summary>
        [JsonProperty("dataQueryName")]
        public string dataQueryName { get; set; }

        /// <summary>
        /// The actual code that constitutes the query.
        /// </summary>
        [JsonProperty("dataQueryCode")]
        public string dataQueryCode { get; set; }

        /// <summary>
        /// The language that the code was written in (e.g. SQL).
        /// </summary>
        [JsonProperty("dataQueryLanguage")]
        public string dataQueryLanguage { get; set; }

        /// <summary>
        /// The connection for the query.
        /// </summary>
        [JsonProperty("dataQueryConnection")]
        public DataConnection dataQueryConnection { get; set; }

        /// <summary>
        /// Free-form and optional classification for the Data Query for use in generation logic (evaluation).
        /// </summary>
        [JsonProperty("dataQueryClassification")]
        public List<Classification> dataQueryClassification { get; set; }
    }
}
