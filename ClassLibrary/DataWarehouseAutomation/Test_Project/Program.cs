using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataWarehouseAutomation;

namespace Test_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            string jsonSchema = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\..\GenericInterface\interfaceDataWarehouseAutomationMetadata.json";

            List<string> fileList = new List<string>();
            fileList.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\SampleFiles\sampleBasic.json"); // Most basic test
            fileList.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\SampleFiles\sampleSourceQuery.json"); // Simple test using a query as source
            fileList.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\SampleFiles\sampleCalculation.json"); // Simple test using one of the column mappings as calculation
            fileList.Add(AppDomain.CurrentDomain.BaseDirectory + @"..\..\SampleFiles\samplesSimpleDDL.json"); // Simple test using one of the column mappings as calculation

            foreach (string jsonFile in fileList)
            {
                var result = JsonHandling.ValidateJsonFileAgainstSchema(jsonSchema, jsonFile);

                var testOutput = result.Valid ? "OK" : "Failed";

                Console.Write($"The result for {jsonFile} was {testOutput}.");
                foreach (var error in result.Errors)
                {
                    Console.Write($"   {error.Message} at line {error.LineNumber} position {error.LinePosition} of error type {error.ErrorType}. This is related to {error.Path}.");
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            // Finish the application
            Console.ReadKey();
        }
    }
}
