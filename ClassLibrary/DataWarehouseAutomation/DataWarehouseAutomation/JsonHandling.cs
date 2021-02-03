using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace DataWarehouseAutomation
{
    public class JsonHandling
    {
        /// <summary>
        /// Struct to contain a Json.Net validation result with the collection of errors (if any)
        /// </summary>
        public struct ValidationResult
        {
            public bool Valid { get; set; }
            public IList<ValidationError> Errors { get; set; }
        }

        /// <summary>
        /// Validate a Json file against a Json schema using Json.Net
        /// </summary>
        /// <param name="jsonSchemaFile"></param>
        /// <param name="jsonFile"></param>
        /// <returns></returns>
        public static ValidationResult ValidateJsonFileAgainstSchema(string jsonSchemaFile, string jsonFile)
        {
            // Read the Json file
            string jsonSchemaContent="";
            try
            {
                using (StreamReader sr = new StreamReader(jsonSchemaFile))
                {
                    jsonSchemaContent = sr.ReadToEnd();
                }
            }
            catch
            {
                // TBD
            }

            // Read the Json file
            string jsonFileContent = "";
            try
            {
                using (StreamReader sr = new StreamReader(jsonFile))
                {
                    jsonFileContent = sr.ReadToEnd();
                }
            }
            catch
            {
                // TBD
            }

            // Parse the content
            JSchema jsonSchema = new JSchema();
            try
            {
                jsonSchema = JSchema.Parse(jsonSchemaContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occurred. The error message is: "+ex);
            }
            
            JToken jsonFileToken = "";
            try
            {
                jsonFileToken = JToken.Parse(jsonFileContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occurred. The error message is: " + ex);
            }

            // Validate the file against the schema
            IList<ValidationError> errors;
            bool valid = jsonFileToken.IsValid(jsonSchema, out errors);

            // return outcome + errors and line info
            return new ValidationResult
            {
                Valid = valid,
                Errors = errors
            };
        }
    }
}
