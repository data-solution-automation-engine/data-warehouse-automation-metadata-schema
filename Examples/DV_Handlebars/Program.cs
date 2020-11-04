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
            // Local variables
            string stringTemplate = "" ;
            string jsonInput = "";
            string result = "";
            DataObjectMappings deserialisedMapping = new DataObjectMappings();

            // Simple example template
            stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\TemplateSampleBasic.handlebars");
            var template = Handlebars.Compile(stringTemplate);
            jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\sampleBasic.json");
            deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappings>(jsonInput);
            result = template(deserialisedMapping);
            Console.WriteLine(result);

            // Example using extensions
            stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\TemplateSampleBasicWithExtensions.handlebars");
            template = Handlebars.Compile(stringTemplate);
            jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\sampleBasicWithExtensions.json");
            deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappings>(jsonInput);
            result = template(deserialisedMapping);
            Console.WriteLine(result);

            // Example using more than one mapping at data item level
            stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\TemplateSampleMultipleDataItemMappings.handlebars");
            template = Handlebars.Compile(stringTemplate);
            jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\sampleMultipleDataItemMappings.json");
            deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappings>(jsonInput);
            result = template(deserialisedMapping);
            Console.WriteLine(result);

            // Example generating DDL statements
            stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\TemplateSampleSimpleDDL.handlebars");
            template = Handlebars.Compile(stringTemplate);
            jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\sampleSimpleDDL.json");
            deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappings>(jsonInput);
            result = template(deserialisedMapping);
            Console.WriteLine(result);

            Console.ReadKey();
        }
    }
}
