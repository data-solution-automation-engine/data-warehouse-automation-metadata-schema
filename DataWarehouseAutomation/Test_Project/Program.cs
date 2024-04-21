using System;
using System.Collections.Generic;
using DataWarehouseAutomation.Utils;

namespace Test_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            string jsonSchema = string.Empty;
            try
            {
                jsonSchema = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\..\GenericInterface\interfaceDataWarehouseAutomationMetadataV2_0.json";
            }
            catch
            {
                Console.WriteLine("An issue was detected loading the JSON schema definition.");
            }

            if (!string.IsNullOrEmpty(jsonSchema))
            {
                var sampleTemplateDirectory = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\Sample_Metadata\";

                List<string> fileList = new List<string>();
                //fileList.Add(sampleTemplateDirectory + @"sampleBasic.json");
                //fileList.Add(sampleTemplateDirectory + @"sampleBasicWithExtensions.json");
                //fileList.Add(sampleTemplateDirectory + @"sampleCalculation.json");
                //fileList.Add(sampleTemplateDirectory + @"sampleFreeForm.json"); // Expected to fail, intentional.
                //fileList.Add(sampleTemplateDirectory + @"sampleJsonStagingWithPsaDetails.json");
                //fileList.Add(sampleTemplateDirectory + @"sampleMultipleDataItemMappings.json");
                //fileList.Add(sampleTemplateDirectory + @"sampleSimpleDDL.json"); // Simple test using one of the column mappings as calculation
                //fileList.Add(sampleTemplateDirectory + @"sampleSourceQuery.json");
                //fileList.Add(sampleTemplateDirectory + @"sampleCustomFunctions.json");
                fileList.Add(sampleTemplateDirectory + @"sampleDataVaultHub.json");

                foreach (string jsonFile in fileList)
                {
                    var result = JsonValidation.ValidateJsonFileAgainstSchema(jsonSchema, jsonFile);

                    var testOutput = result.Valid ? "OK" : "Failed";

                    Console.Write($"The result for {jsonFile} was {testOutput}.");
                    foreach (var error in result.Errors)
                    {
                        Console.Write($"   {error.Message} at line {error.LineNumber} position {error.LinePosition} of error type {error.ErrorType}. This is related to {error.Path}.");
                    }

                    Console.WriteLine();
                    Console.WriteLine();
                }
            }

            // Finish the application
            Console.ReadKey();
        }
    }
}
