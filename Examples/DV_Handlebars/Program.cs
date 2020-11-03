using System;
using System.IO;
using DataWarehouseAutomation;
using HandlebarsDotNet;
using Newtonsoft.Json;

namespace VEDW_Handlebars
{
    class Program
    {
        static void Main(string[] args)
        {
            // Compile the template
            
            // Uncomment out the difference examples

            //var stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\TemplateSampleBasic.handlebars");
            var stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\TemplateSampleBasicWithExtensions.handlebars");
            //var stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\TemplateSampleSimpleDDL.handlebars");


            var template = Handlebars.Compile(stringTemplate);


            // Retrieve metadata from Json and store in a data table object

            //var jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\sampleBasic.json");
            var jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\sampleBasicWithExtensions.json");
            //var jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\sampleSimpleDDL.json");



            DataObjectMappings deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappings>(jsonInput);

            // Return the result to the user
            var result = template(deserialisedMapping);
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
