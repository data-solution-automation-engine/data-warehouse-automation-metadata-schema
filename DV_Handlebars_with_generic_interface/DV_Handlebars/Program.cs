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
            //var stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\HubTemplate_Original.handlebars");
            var stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\HubTemplate_Modified.handlebars");

            var template = Handlebars.Compile(stringTemplate);

            // Retrieve metadata and store in a data table object
            var jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\..\inputMapping.json");
            
            MappingList deserialisedMapping = JsonConvert.DeserializeObject<MappingList>(jsonInput);

            // Return the result to the user
            var result = template(deserialisedMapping);
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
