using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

using DataWarehouseAutomation.Utils;

using HandlebarsDotNet;

namespace Example_Handlebars;

class Program
{
    static void Main(string[] args)
    {
        HandlebarsHelpers.RegisterHandlebarsHelpers();

        var sampleTemplateDirectory = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\Sample_Templates\";
        var sampleMetadataDirectory = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\Sample_Metadata\";

        DisplayPatternResult(sampleTemplateDirectory + @"myFirstTemplate.handlebars", sampleMetadataDirectory + @"myFirstMapping.json");
        DisplayPatternResult(sampleTemplateDirectory + @"TemplateSampleBasic.handlebars", sampleMetadataDirectory + @"sampleBasic.json");
        DisplayPatternResult(sampleTemplateDirectory + @"TemplateSampleBasicWithExtensions.handlebars", sampleMetadataDirectory + @"sampleBasicWithExtensions.json");
        DisplayPatternResult(sampleTemplateDirectory + @"TemplateSampleMultipleDataItemMappings.handlebars", sampleMetadataDirectory + @"sampleMultipleDataItemMappings.json");
        DisplayPatternResult(sampleTemplateDirectory + @"TemplateSampleSourceQuery.handlebars", sampleMetadataDirectory + @"sampleSourceQuery.json");
        DisplayPatternResult(sampleTemplateDirectory + @"TemplateSampleSimpleDDL.handlebars", sampleMetadataDirectory + @"sampleSimpleDDL.json");
        DisplayPatternResult(sampleTemplateDirectory + @"TemplateSampleCalculation.handlebars", sampleMetadataDirectory + @"sampleCalculation.json");
        DisplayPatternResult(sampleTemplateDirectory + @"TemplateSampleFreeForm.handlebars", sampleMetadataDirectory + @"sampleFreeForm.json");
        DisplayPatternResult(sampleTemplateDirectory + @"TemplateSampleCustomFunctions.handlebars", sampleMetadataDirectory + @"sampleCustomFunctions.json");

        Console.ReadKey();
    }

    private static void DisplayPatternResult(string patternFile, string jsonMetadataFile)
    {
        try
        {
            Console.Clear();

            // Fetch and compile the template
            string stringTemplate = File.ReadAllText(patternFile);
            var template = Handlebars.Compile(stringTemplate);

            // Fetch the content of the Json files
            string jsonInput = File.ReadAllText(jsonMetadataFile);

            //var deserializedMapping = JsonConvert.DeserializeObject<ExpandoObject>(jsonInput, new ExpandoObjectConverter()); -- This is the old Newtonsoft expando object approach
            //var deserializedMapping = System.Text.Json.JsonSerializer.Deserialize<ExpandoObject>(jsonInput);
            //DataObjectMappingList deserializedMapping = JsonSerializer.Deserialize<DataObjectMappingList>(jsonInput);

            JsonNode deserializedMapping = JsonSerializer.Deserialize<JsonNode>(jsonInput);

            var result = template(deserializedMapping);
            Console.WriteLine(result);
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An issue was encountered while generating {patternFile} from {jsonMetadataFile}: {ex}");
            Console.ReadKey();
        }
    }
}
