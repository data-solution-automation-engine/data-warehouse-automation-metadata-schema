using System;
using System.IO;
using DataWarehouseAutomation;
using HandlebarsDotNet;
using Newtonsoft.Json;

namespace Example_Handlebars
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
            stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Templates\TemplateSampleBasic.handlebars");
            var template = Handlebars.Compile(stringTemplate);
            jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Metadata\sampleBasic.json");
            deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappings>(jsonInput);
            result = template(deserialisedMapping);
            Console.WriteLine(result);

            // Example using extensions
            stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Templates\TemplateSampleBasicWithExtensions.handlebars");
            template = Handlebars.Compile(stringTemplate);
            jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Metadata\sampleBasicWithExtensions.json");
            deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappings>(jsonInput);
            result = template(deserialisedMapping);
            Console.WriteLine(result);

            // Example using more than one mapping at data item level
            stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Templates\TemplateSampleMultipleDataItemMappings.handlebars");
            template = Handlebars.Compile(stringTemplate);
            jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Metadata\sampleMultipleDataItemMappings.json");
            deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappings>(jsonInput);
            result = template(deserialisedMapping);
            Console.WriteLine(result);

            // Example generating DDL statements
            stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Templates\TemplateSampleSimpleDDL.handlebars");
            template = Handlebars.Compile(stringTemplate);
            jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Metadata\sampleSimpleDDL.json");
            deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappings>(jsonInput);
            result = template(deserialisedMapping);
            Console.WriteLine(result);

            // Example using OneOf / swapping a source for a query
            stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Templates\TemplateSampleCalculation.handlebars");
            template = Handlebars.Compile(stringTemplate);
            jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Metadata\sampleCalculation.json");
            deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappings>(jsonInput);
            result = template(deserialisedMapping);
            Console.WriteLine(result);

            Console.ReadKey();
        }
    }
}
