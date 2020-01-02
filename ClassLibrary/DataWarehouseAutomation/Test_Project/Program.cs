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
            string jsonSchema = @"D:\Git_Repositories\Data_Warehouse_Automation_Metadata_Interface\ClassLibrary\DataWarehouseAutomation\DataWarehouseAutomation\interfaceDataWarehouseAutomationMetadata.json";

            string jsonFile = @"D:\Git_Repositories\Data_Warehouse_Automation_Metadata_Interface\ClassLibrary\DataWarehouseAutomation\Test_Project\SampleFiles\sample.json";
            //string jsonFile = @"D:\Git_Repositories\Data_Warehouse_Automation_Metadata_Interface\ClassLibrary\DataWarehouseAutomation\Test_Project\SampleFiles\sample2.json";

            //string jsonSchema = "";
            //string jsonFile = "";

            var result = JsonHandling.ValidateJsonFileAgainstSchema(jsonSchema, jsonFile);

            Console.Write($"The result was {result.Valid}");
            foreach (var error in result.Errors)
            {
                Console.Write($"   {error.Message} at line {error.LineNumber} position {error.LinePosition} of error type {error.ErrorType}. This is related to {error.Path}.");
            }
            Console.ReadKey();
        }
    }
}
