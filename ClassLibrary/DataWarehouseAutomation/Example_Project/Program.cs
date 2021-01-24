using System;
using System.IO;
using DataWarehouseAutomation;
using HandlebarsDotNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Example_Handlebars
{
    class Program
    {
        static void Main(string[] args)
        {
            HandleBarsHelpers.RegisterHandleBarsHelpers();;

            // Local variables, for reuse
            string stringTemplate = "";
            string jsonInput = "";
            string result = "";
            //DataObjectMappings deserialisedMapping = new DataObjectMappings();
            var deserialisedMapping = new JObject();
            var template = Handlebars.Compile("");

            // Simple example template
            try
            {
                stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Templates\TemplateSampleBasic.handlebars");
                template = Handlebars.Compile(stringTemplate);
                jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Metadata\sampleBasic.json");
                //deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappings>(jsonInput);
                deserialisedMapping = JObject.Parse(jsonInput);
                result = template(deserialisedMapping);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An issue was encountered: {ex}");
            }

            // Example using extensions
            stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Templates\TemplateSampleBasicWithExtensions.handlebars");
            template = Handlebars.Compile(stringTemplate);
            jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Metadata\sampleBasicWithExtensions.json");
            //deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappings>(jsonInput);
            deserialisedMapping = JObject.Parse(jsonInput);
            result = template(deserialisedMapping);
            Console.WriteLine(result);

            // Example using more than one mapping at data item level
            stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Templates\TemplateSampleMultipleDataItemMappings.handlebars");
            template = Handlebars.Compile(stringTemplate);
            jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Metadata\sampleMultipleDataItemMappings.json");
            //deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappings>(jsonInput);
            deserialisedMapping = JObject.Parse(jsonInput);
            result = template(deserialisedMapping);
            Console.WriteLine(result);

            // Example generating DDL statements
            stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Templates\TemplateSampleSimpleDDL.handlebars");
            template = Handlebars.Compile(stringTemplate);
            jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Metadata\sampleSimpleDDL.json");
            //deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappings>(jsonInput);
            deserialisedMapping = JObject.Parse(jsonInput);
            result = template(deserialisedMapping);
            Console.WriteLine(result);

            // Example using OneOf / swapping a source for a query
            stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Templates\TemplateSampleCalculation.handlebars");
            template = Handlebars.Compile(stringTemplate);
            jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Metadata\sampleCalculation.json");
            //deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappings>(jsonInput);
            deserialisedMapping = JObject.Parse(jsonInput);
            result = template(deserialisedMapping);
            Console.WriteLine(result);

            // Data Vault Satellite example
            stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Templates\TemplateSatelliteView.handlebars");
            template = Handlebars.Compile(stringTemplate);
            jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Metadata\sampeVDW_Sat_Customer_v161.json");
            //deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappings>(jsonInput);
            deserialisedMapping = JObject.Parse(jsonInput);
            result = template(deserialisedMapping);
            Console.WriteLine(result);

            //Free form example
            stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Templates\TemplateSampleFreeForm.handlebars");
            template = Handlebars.Compile(stringTemplate);
            jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_Metadata\sampleFreeForm.json");
            //deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappings>(jsonInput);
            deserialisedMapping = JObject.Parse(jsonInput);
            result = template(deserialisedMapping);
            Console.WriteLine(result);

            Console.ReadKey();
        }
    }
}
