using System;
using System.IO;
using System.Text.Json;
using HandlebarsDotNet;
using System.Text.Json.Nodes;
using DataWarehouseAutomation.Utils;

namespace Example_Handlebars
{
    class Program
    {
        static void Main(string[] args)
        {
            HandleBarsHelpers.RegisterHandleBarsHelpers();

            var sampleTemplateDirectory = AppDomain.CurrentDomain.BaseDirectory+@"..\..\..\..\Sample_Templates\";
            var sampleMetadataDirectory = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\Sample_Metadata\";

            DisplayPatternResult(sampleTemplateDirectory + @"myFirstTemplate.handlebars", sampleMetadataDirectory + @"myFirstMapping.json");
            Console.Clear();
            DisplayPatternResult(sampleTemplateDirectory + @"TemplateSampleBasic.handlebars", sampleMetadataDirectory + @"sampleBasic.json");
            Console.Clear();
            DisplayPatternResult(sampleTemplateDirectory + @"TemplateSampleBasicWithExtensions.handlebars", sampleMetadataDirectory + @"sampleBasicWithExtensions.json");
            Console.Clear();
            DisplayPatternResult(sampleTemplateDirectory + @"TemplateSampleMultipleDataItemMappings.handlebars", sampleMetadataDirectory + @"sampleMultipleDataItemMappings.json");
            Console.Clear();
            DisplayPatternResult(sampleTemplateDirectory + @"TemplateSampleSourceQuery.handlebars", sampleMetadataDirectory + @"sampleSourceQuery.json");
            Console.Clear();
            DisplayPatternResult(sampleTemplateDirectory + @"TemplateSampleSimpleDDL.handlebars", sampleMetadataDirectory + @"sampleSimpleDDL.json");
            Console.Clear();
            DisplayPatternResult(sampleTemplateDirectory + @"TemplateSampleCalculation.handlebars", sampleMetadataDirectory + @"sampleCalculation.json");
            Console.Clear();
            DisplayPatternResult(sampleTemplateDirectory + @"TemplateSampleFreeForm.handlebars", sampleMetadataDirectory + @"sampleFreeForm.json");
            Console.Clear();
            DisplayPatternResult(sampleTemplateDirectory + @"TemplateSampleCustomFunctions.handlebars", sampleMetadataDirectory + @"sampleCustomFunctions.json");

            Console.ReadKey();
        }

        private static void DisplayPatternResult(string patternFile, string jsonMetadataFile)
        {
            try
            {
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
                Console.WriteLine($"An issue was encountered: {ex}");
            }
        }
    }
}
