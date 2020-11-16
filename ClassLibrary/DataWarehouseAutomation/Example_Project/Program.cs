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
            HandleBarsHelpers.RegisterHandleBarsHelpers();

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



            Console.ReadKey();
        }
    }
}
