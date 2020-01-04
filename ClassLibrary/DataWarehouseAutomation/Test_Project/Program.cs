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

            foreach (string jsonFile in fileList)
            {
                var result = JsonHandling.ValidateJsonFileAgainstSchema(jsonSchema, jsonFile);

                var testOutput = result.Valid ? "OK" : "Failed";

                Console.Write($"The result for {jsonFile} was {testOutput}.");
                foreach (var error in result.Errors)
                {
                    Console.Write($"   {error.Message} at line {error.LineNumber} position {error.LinePosition} of error type {error.ErrorType}. This is related to {error.Path}.");
                }
            }

            Console.ReadKey();
        }
    }
}
