using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataWarehouseAutomation
{
    /// <summary>
    /// IDataItem can be either a data item or data query.
    /// </summary>
    public interface IDataItem
    {
        /// <summary>
        /// Optional identifier as a string value to allow various identifier approaches.
        /// </summary>
        [JsonPropertyName("id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}