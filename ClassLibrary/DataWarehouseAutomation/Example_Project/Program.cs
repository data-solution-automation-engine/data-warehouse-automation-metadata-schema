using System;
using System.IO;
using DataWarehouseAutomation;
using HandlebarsDotNet;
using Newtonsoft.Json.Linq;


namespace Example_Handlebars
{
    class Program
    {
        static void Main(string[] args)
        {
            HandleBarsHelpers.RegisterHandleBarsHelpers();

            DisplayPatternResult(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Templates\TemplateSampleBasic.handlebars", AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Metadata\sampleBasic.json");
            //DisplayPatternResult(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Templates\TemplateSampleBasicWithExtensions.handlebars", AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Metadata\sampleBasicWithExtensions.json");
            //DisplayPatternResult(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Templates\TemplateSampleMultipleDataItemMappings.handlebars", AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Metadata\sampleMultipleDataItemMappings.json");
            //DisplayPatternResult(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Templates\TemplateSampleSimpleDDL.handlebars", AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Metadata\sampleSimpleDDL.json");
            //DisplayPatternResult(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Templates\TemplateSampleCalculation.handlebars", AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Metadata\sampleCalculation.json");
            //DisplayPatternResult(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Templates\TemplateSatelliteView.handlebars", AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Metadata\sampleVDW_Sat_Customer_v161.json");
            //DisplayPatternResult(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Templates\TemplateSampleFreeForm.handlebars", AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Metadata\sampleFreeForm.json");
            //DisplayPatternResult(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Templates\TemplateSampleCustomFunctions.handlebars", AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Metadata\sampleCustomFunctions.json");

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
                
                //var deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappings>(jsonInput);
                var deserialisedMapping = JObject.Parse(jsonInput);
                
                var result = template(deserialisedMapping);
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
