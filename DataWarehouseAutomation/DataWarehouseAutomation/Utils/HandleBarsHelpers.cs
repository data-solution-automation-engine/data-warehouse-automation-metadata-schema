﻿using HandlebarsDotNet;

namespace DataWarehouseAutomation.Utils;

public static class HandlebarsHelpers
{
    /// <summary>
    /// Generate a random integer value, capped by an input (maximum) value.
    /// </summary>
    /// <param name="maxNumber"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static int GetRandomNumber(int maxNumber)
    {
        if (maxNumber < 1)
            throw new Exception("The maximum number value should be greater than 1");

        var b = new byte[4];
        var rng = RandomNumberGenerator.Create();
        rng.GetBytes(b);
        var seed = (b[0] & 0x7f) << 24 | b[1] << 16 | b[2] << 8 | b[3];
        var r = new Random(seed);
        return r.Next(1, maxNumber);
    }

    /// <summary>
    /// Generate a random date (no time component), higher than the input year number (four-digit integer).
    /// </summary>
    /// <param name="startYear"></param>
    /// <returns></returns>
    public static DateTime GetRandomDate(int startYear = 1995)
    {
        var start = new DateTime(startYear, 1, 1);
        var range = (DateTime.Today - start).Days;
        var b = new byte[4];
        var rng = RandomNumberGenerator.Create();
        rng.GetBytes(b);

        var seed = (b[0] & 0x7f) << 24 | b[1] << 16 | b[2] << 8 | b[3];
        return start.AddDays(new Random(seed).Next(1, range)).AddSeconds(new Random(seed).Next(1, 86400));
    }

    /// <summary>
    ///  Convenience method to install all Handlebars extension in one go.
    /// </summary>
    /// <exception cref="HandlebarsException"></exception>
    public static void RegisterHandlebarsHelpers()
    {
        // Extension to the Handlebars templating language. Shows the current date and time in the generated output.
        // Usage: {{now}}
        // Example: The time is {{now}}.
        Handlebars.RegisterHelper("now", (output, _, _) => output.WriteSafeString(DateTime.Now));

        // Generation random date, based on an integer input year value.
        Handlebars.RegisterHelper("randomDate", (output, _, arguments) =>
        {
            if (arguments.Length > 1)
            {
                throw new HandlebarsException("The {{randomDate}} function requires a single integer (year e.g. 1995) value as input.");
            }

            if (arguments.Length == 1)
            {
                bool evaluationResult = int.TryParse(arguments[0].ToString(), out var localInteger);
                if (evaluationResult == false)
                {
                    throw new HandlebarsException($"The {{randomDate}} functions failed because {arguments[0]} could not be converted to an integer.");
                }

                output.WriteSafeString(GetRandomDate(localInteger).Date);
            }
        });

        // Generation random string, based on an integer input value cap.
        Handlebars.RegisterHelper("randomNumber", (output, _, arguments) =>
        {
            if (arguments.Length > 1)
            {
                throw new HandlebarsException("The {{randomNumber}} function requires a single integer value as input.");
            }

            if (arguments.Length == 1)
            {
                bool evaluationResult = int.TryParse(arguments[0].ToString(), out var localInteger);
                if (!evaluationResult)
                {
                    throw new HandlebarsException($"The {{randomNumber}} functions failed because {arguments[0]} could not be converted to an integer.");
                }
                output.WriteSafeString(GetRandomNumber(localInteger));
            }
        });

        // Generation random string, based on an integer input value
        Handlebars.RegisterHelper("randomString", (output, _, arguments) =>
        {
            if (arguments.Length > 1)
            {
                throw new HandlebarsException("The {{randomString}} function requires a single integer value as input.");
            }

            if (arguments.Length == 1)
            {
                bool evaluationResult = int.TryParse(arguments[0].ToString(), out var localInteger);

                if (!evaluationResult)
                {
                    throw new HandlebarsException($"The {{randomString}} functions failed because {arguments[0]} could not be converted to an integer.");
                }

                var array = new[]
                {
                    "0","1","2","3","4","5","6","8","9",
                    "a","b","c","d","e","f","g","h","j","k","m","n","p","q","r","s","t","u","v","w","x","y","z",
                    "A","B","C","D","E","F","G","H","J","K","L","M","N","P","R","S","T","U","V","W","X","Y","Z"
                };
                var sb = new StringBuilder();
                for (var i = 0; i < localInteger; i++) sb.Append(array[GetRandomNumber(53)]);

                output.WriteSafeString(sb.ToString());
            }
        });

        Handlebars.RegisterHelper("stringWrap", (writer, _, args) =>
        {
            if (args.Length != 3) throw new HandlebarsException("The {{stringWrap}} function requires exactly three arguments, an object and the string to wrap its value in.");

            if (args[0].GetType().Name != "UndefinedBindingResult")
            {
                try
                {
                    writer.Write(string.Concat(args[1].ToString(), args[0].ToString(), args[2].ToString()));
                }
                catch (Exception ex)
                {
                    writer.WriteSafeString("An issue has been encountered: " + ex.Message + ".");
                }
            }
        });

        Handlebars.RegisterHelper("stringUpper", (writer, _, args) =>
        {
            if (args.Length != 1) throw new HandlebarsException("The {{stringUpper}} function requires one, and only one, input string value.");

            if (args[0].GetType().Name != "UndefinedBindingResult" && args[0] is string theString)
            {
                try
                {
                    writer.Write(theString.ToUpper());
                }
                catch (Exception ex)
                {
                    writer.WriteSafeString("An issue has been encountered: " + ex.Message + ".");
                }
            }
        });

        Handlebars.RegisterHelper("stringLower", (writer, _, args) =>
        {
            if (args.Length != 1) throw new HandlebarsException("The {{stringLower}} function requires one, and only one, input string value.");

            if (args[0].GetType().Name != "UndefinedBindingResult" && args[0] is string theString)
            {
                try
                {
                    writer.Write(theString.ToLower());
                }
                catch (Exception ex)
                {
                    writer.WriteSafeString("An issue has been encountered: " + ex.Message + ".");
                }
            }
        });

        // Accept two values, and see if they are the same, use as block helper.
        // Usage {{#stringCompare string1 string2}} do something {{else}} do something else {{/stringCompare}}
        // Usage {{#stringCompare string1 string2}} do something {{/stringCompare}}
        Handlebars.RegisterHelper("stringCompare", (output, options, context, arguments) =>
        {
            if (arguments.Length != 2) throw new HandlebarsException("The {{stringCompare}} function requires exactly two arguments.");

            var leftString = arguments[0] == null ? "" : arguments[0].ToString();
            var rightString = arguments[1] == null ? "" : arguments[1].ToString();

            if (leftString == rightString)
            {
                options.Template(output, context);
            }
            else
            {
                options.Inverse(output, context);
            }
        });

        // Accept two values, and do something if they are the different.
        // Usage {{#stringDiff string1 string2}} do something {{else}} do something else {{/stringDiff}}
        // Usage {{#stringDiff string1 string2}} do something {{/stringDiff}}
        Handlebars.RegisterHelper("stringDiff", (output, options, context, arguments) =>
        {
            if (arguments.Length != 2) throw new HandlebarsException("The {{stringDiff}} functions requires exactly two arguments.");

            var leftString = arguments[0] == null ? "" : arguments[0].ToString();
            var rightString = arguments[1] == null ? "" : arguments[1].ToString();

            if (leftString != rightString)
            {
                options.Template(output, (object)context);
            }
            else
            {
                options.Inverse(output, (object)context);
            }
        });

        Handlebars.RegisterHelper("replicate", (output, options, context, arguments) =>
        {
            if (arguments.Length != 1)
            {
                throw new HandlebarsException("The {{replicate}} functions requires a single integer value as input.");
            }

            if (arguments.Length == 1)
            {
                bool evaluationResult = int.TryParse(arguments[0].ToString(), out var localInteger);

                if (!evaluationResult)
                {
                    throw new HandlebarsException($"The {{replicate}} functions failed because {arguments[0]} could not be converted to an integer.");
                }

                for (var i = 0; i < localInteger; ++i)
                {
                    options.Template(output, (object)context);
                }
            }
        });

        // Character spacing not satisfactory? Do not panic, help is near! Make sure the character spacing is righteous using this Handlebars helper.
        // Usage {{space sourceDataObject.name}} will space out (!?) the name of the source to 30 characters and a few tabs for lots of white spaces.
        Handlebars.RegisterHelper("space", (writer, _, arguments) =>
        {
            if (arguments.Length != 1)
            {
                throw new HandlebarsException("The {{space}} functions requires an input string value to space out against.");
            }

            string outputString = arguments[0]?.ToString() ?? "";
            if (outputString.Length < 30)
            {
                outputString = outputString.PadRight(30);
            }
            writer.WriteSafeString(outputString + "\t\t\t\t");

        });

        Handlebars.RegisterHelper("stringReplace", (writer, _, args) =>
        {
            if (args.Length < 3) throw new HandlebarsException("The {{StringReplace}} function requires at least three arguments.");

            if (args[0].GetType().Name != "UndefinedBindingResult")
            {
                try
                {
                    var expression = args[0];

                    if (!string.IsNullOrEmpty(expression.ToString()) && args[0] is JsonElement value)
                    {
                        expression = value.GetString();
                    }
                    if (args[1] is string pattern && args[2] is string replacement)
                    {
                        expression = expression?.ToString()?.Replace(pattern, replacement);
                        writer.WriteSafeString(expression);
                    }
                }
                catch (Exception exception)
                {
                    writer.WriteSafeString("An issue has been encountered: " + exception.Message + ".");
                }
            }
        });

        // Run at data object mapping level.
        Handlebars.RegisterHelper("targetDataItemExists", (output, options, context, arguments) =>
        {
            if (arguments.Length != 1) throw new HandlebarsException("The {{targetDataItemExists}} function requires only one argument.");

            try
            {
                var searchString = arguments[0] == null ? "" : arguments[0].ToString();
                DataObjectMapping? dataObjectMapping = JsonSerializer.Deserialize<DataObjectMapping>(context.Value.ToString());

                var dataItemExists = dataObjectMapping?.DataItemMappings?.FirstOrDefault(x => x.TargetDataItem.Name == searchString);

                if (dataItemExists != null)
                {
                    // Regular block
                    options.Template(output, context);
                }
                else
                {
                    // Else block
                    options.Inverse(output, context);
                }
            }
            catch (Exception exception)
            {
                throw new HandlebarsException("The {{targetDataItemExists}} helper reported a conversion error, and was unable to deserialize the context into a DataObjectMapping. The reported error is " + exception.Message);
            }
        });

        // Run at data object mapping level.
        Handlebars.RegisterHelper("exists", (output, options, context, arguments) =>
        {
            if (arguments.Length == 0 || arguments.Length > 2) throw new HandlebarsException("The {{exists}} function must have one or two arguments, which must be a property of a data object mapping which is mandatory and an optional exception value.");

            var property = arguments[0] == null ? "" : arguments[0].ToString();

            var propertyValue = string.Empty;
            if (arguments.Length == 2)
            {
                propertyValue = arguments[1]?.ToString();
            }

            // Supported:
            // - multiActiveKey
            // - targetDataItem

            try
            {
                var dataObjectMapping = JsonSerializer.Deserialize<DataObjectMapping>(context.Value.ToString(), DefaultJsonOptions.DeserializerOptions);

                var outcome = false;

                if (property == "multiActiveKey")
                {
                    var targetDataItemsWithClassifications = new List<DataItemMapping>();
                    if (string.IsNullOrEmpty(propertyValue))
                    {
                        targetDataItemsWithClassifications = dataObjectMapping?.DataItemMappings?.Where(x => x.TargetDataItem.Classifications != null).ToList();
                    }
                    else
                    {
                        targetDataItemsWithClassifications = dataObjectMapping?.DataItemMappings?.Where(x => x.TargetDataItem.Classifications != null && x.TargetDataItem.Name != propertyValue).ToList();
                    }

                    if (targetDataItemsWithClassifications != null)
                    {
                        var dataItemClassifications = targetDataItemsWithClassifications.SelectMany(x => x.TargetDataItem.Classifications ?? []).ToList();

                        if (dataItemClassifications?.Any() == true)
                        {
                            foreach (var classification in dataItemClassifications)
                            {
                                if (classification.Classification == "MultiActiveKey")
                                {
                                    outcome = true;
                                }
                            }
                        }
                    }
                }
                else if (property == "targetDataItem")
                {
                    var targetDataItem = dataObjectMapping?.DataItemMappings?.Find(x => x.TargetDataItem != null);

                    if (targetDataItem != null)
                    {
                        outcome = true;
                    }
                }
                else
                {
                    throw new HandlebarsException($"The {{{property}}} used is not supported by the exist function. Only multiActiveKey and targetDataItem are currently supported.");
                }

                if (outcome)
                {
                    // Regular block
                    options.Template(output, context);
                }
                else
                {
                    // Else block
                    options.Inverse(output, context);
                }
            }
            catch (Exception exception)
            {
                throw new HandlebarsException($"The exists helper using the property {{{property}}} reported a conversion error, and was unable to deserialize the context into a DataObjectMapping. The reported error is " + exception.Message);
            }
        });

        // Block helper that evaluates is a certain classification exists in the list of classifications.
        Handlebars.RegisterHelper("hasClassification", (output, options, context, parameters) =>
        {
            // Check if the parameters are valid.
            if (parameters.Length != 2 || parameters[1] is not string)
            {
                throw new HandlebarsException("An issue was encountered. The {{hasClassification}} helper expects two arguments: a List<DataClassification> and a string lookup key.");
            }

            try
            {
                var classificationsParameter = parameters[0];

                if (classificationsParameter == null || classificationsParameter.ToString() == "classifications")
                {
                    // Skip, it's really null.
                }
                else
                {
                    var classifications = JsonSerializer.Deserialize<List<DataClassification>>(classificationsParameter.ToString() ?? string.Empty);
                    var classificationName = (string)parameters[1];
                    var result = classifications?.Find(i => i.Classification.Equals(classificationName, StringComparison.OrdinalIgnoreCase))?.Classification;

                    if (!string.IsNullOrEmpty(result))
                    {
                        // Regular block, a classification has been found
                        options.Template(output, context);
                    }
                    else
                    {
                        // Else block, no classification with the input name has been found.
                        options.Inverse(output, context);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new HandlebarsException($"The {{hasClassification}} function encountered an error: the list of classifications provided as the first argument could not be deserialized. The reported error is :{exception.Message}");
            }
        });

        // Block helper that evaluates is a certain classification exists in the list of classifications.
        Handlebars.RegisterHelper("hasClassification", (output, options, context, parameters) =>
        {
            // Check if the parameters are valid.
            if (parameters.Length != 2 || parameters[1] is not string)
            {
                throw new HandlebarsException("An issue was encountered. The {{hasClassification}} helper expects two arguments: a List<DataClassification> and a string lookup key.");
            }

            try
            {
                if (parameters[0] == null || parameters[1] == null || string.IsNullOrEmpty(parameters[1].ToString()) || parameters[0].ToString() == "classifications" || parameters[0]?.ToString()?.Length == 0)
                {
                    // Skip, it's really null.
                    //Console.WriteLine("Something is NULL");
                }
                else
                {
                    var classificationsParameter = parameters[0];
                    var classificationName = (string)parameters[1];

                    //Console.WriteLine(classificationsParameter);

                    var classifications = JsonSerializer.Deserialize<List<DataClassification>>(classificationsParameter.ToString() ?? string.Empty);

                    var result = classifications?.Find(i => i.Classification?.ToString().Equals(classificationName, StringComparison.OrdinalIgnoreCase) == true)?.Classification;

                    if (!string.IsNullOrEmpty(result))
                    {
                        // Regular block, a classification has been found
                        options.Template(output, context);
                    }
                    else
                    {
                        // Else block, no classification with the input name has been found.
                        options.Inverse(output, context);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new HandlebarsException($"The {{{{hasClassification}}}} function encountered an error. The list of classifications provided as the first argument could not be deserialized. The reported error is: '{ex.Message}'.");
            }
        });

        // Block helper that evaluates is a certain string exists in a list of string values.
        Handlebars.RegisterHelper("hasStringValue", (output, options, context, parameters) =>
        {
            // Check if the parameters are valid.
            if (parameters.Length != 2)
            {
                throw new HandlebarsException("An issue was encountered. The {{hasStringValue}} helper expects two arguments: a List<String> and a string lookup key.");
            }

            try
            {
                if (parameters[0] == null || parameters[1] == null || string.IsNullOrEmpty(parameters[1].ToString()) || parameters[0]?.ToString()?.Length == 0)
                {
                    // Skip, it's really null.
                }
                else
                {
                    var stringListParameter = parameters[0];
                    var lookupValue = parameters[1].ToString();

                    // Deserialize the JSON array
                    JsonDocument doc = JsonDocument.Parse(stringListParameter.ToString());

                    JsonElement root = doc.RootElement;

                    bool valueExists = false;

                    // Iterate over the array elements
                    foreach (JsonElement element in root.EnumerateArray())
                    {
                        // Check if the current element matches the value we're looking for
                        if (element.ValueKind == JsonValueKind.String && element.GetString() == lookupValue)
                        {
                            // Value found
                            valueExists = true;
                            break;
                        }
                    }

                    if (valueExists)
                    {
                        // Regular block, a value has been found
                        options.Template(output, context);
                    }
                    else
                    {
                        // Else block, no matching value has been found.
                        options.Inverse(output, context);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new HandlebarsException($"The {{{{hasStringValue}}}} function encountered an error. The list of strings provided as the first argument could not be deserialized. The reported error is: '{ex.Message}'.");
            }
        });

        // lookupExtension allows a lookup of an extension value by key value. Pass in the Extensions list and the string key value.
        Handlebars.RegisterHelper("lookupExtension", (writer, _, parameters) =>
        {
            // Check if the parameters are valid.
            if (parameters.Length != 2 || parameters[1] is not string)
            {
                throw new HandlebarsException("\rThe {{lookupExtension}} helper expects two arguments: a List<Extension> and a string lookup key");
            }

            var extensionList = new List<Extension>();

            // Deserialize the extensions.
            try
            {
                extensionList = JsonSerializer.Deserialize<List<Extension>>(parameters[0].ToString() ?? string.Empty);
            }
            catch (Exception exception)
            {
                throw new HandlebarsException($"\rThe {{{{lookupExtension}}}} helper function encountered an error. \r\rThe list of extensions provided as the first argument could not be deserialized, it probably wasn't available or found. Can you check the location of the extension? \r\rThe code so far is:\r\r{writer}\r\rThe reported error is:\r\r'{exception.Message}'");
            }

            // Write the result.
            string key = "";
            try
            {
                key = (string)parameters[1];
                var result = extensionList?.Find(i => i.Key.Equals(key, StringComparison.OrdinalIgnoreCase))?.Value ?? "";

                writer.WriteSafeString($"{result}");
            }
            catch (Exception exception)
            {
                throw new HandlebarsException($"The {{{{lookupExtension}}}} helper function encountered an error. \r\rNo extension could be found for {key}. \r\rThe reported error is :\r\r'{exception.Message}'");
            }

        });
    }
}
