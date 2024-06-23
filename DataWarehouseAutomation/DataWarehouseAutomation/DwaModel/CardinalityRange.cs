using System.Buffers.Text;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DataWarehouseAutomation.DwaModel;

/// <summary>
/// The possible range for a from/to component for the cardinality.
/// For eaxmple "min": 1, "max": "N"
/// This way, you can define at least 1 to many. Or 0 or 1 to 1.
/// </summary>
public class CardinalityRange
{
    public string? Min { get; set; }
    public string? Max { get; set; }
}