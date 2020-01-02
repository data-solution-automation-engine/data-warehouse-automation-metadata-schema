using System;
using System.IO;
using HandlebarsDotNet;
using Newtonsoft.Json;

namespace VEDW_Handlebars
{
    class Program
    {
        static void Main(string[] args)
        {
            // Compile the template
            var stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\HubTemplate.handlebars");

            var template = Handlebars.Compile(stringTemplate);

            // Retrieve metadata and store in a data table object
            var jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\sample.json");
            
            DataObjectMappingList deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappingList>(jsonInput);

            // Return the result to the user
            var result = template(deserialisedMapping);
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
