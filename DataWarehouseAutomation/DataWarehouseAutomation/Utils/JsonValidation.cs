using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace DataWarehouseAutomation.Utils;

public static class JsonValidation
{
    /// <summary>
    /// Struct to contain a Json.Net validation result with the collection of errors (if any).
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
        string jsonSchemaContent = "";
        try
        {
            using StreamReader sr = new(jsonSchemaFile);
            jsonSchemaContent = sr.ReadToEnd();
        }
        catch (Exception exception)
        {
            Console.WriteLine("An error has occurred: " + exception.Message);
        }

        // Read the Json file
        string jsonFileContent = "";
        try
        {
            using StreamReader sr = new(jsonFile);
            jsonFileContent = sr.ReadToEnd();
        }
        catch (Exception exception)
        {
            Console.WriteLine("An error has occurred: " + exception.Message);
        }

        // Parse the content
        JSchema jsonSchema = new();
        try
        {
            jsonSchema = JSchema.Parse(jsonSchemaContent);
        }
        catch (Exception exception)
        {
            Console.WriteLine("An error has occurred. The error message is: " + exception);
        }

        JToken jsonFileToken = "";
        try
        {
            jsonFileToken = JToken.Parse(jsonFileContent);
        }
        catch (Exception exception)
        {
            Console.WriteLine("An error has occurred. The error message is: " + exception);
        }

        // Validate the file against the schema
        bool valid = jsonFileToken.IsValid(jsonSchema, out IList<ValidationError> errors);

        // return outcome + errors and line info
        return new ValidationResult
        {
            Valid = valid,
            Errors = errors
        };
    }
}
