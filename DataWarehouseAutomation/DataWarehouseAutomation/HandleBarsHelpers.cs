namespace DataWarehouseAutomation;

public static class HandleBarsHelpers
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
    public static void RegisterHandleBarsHelpers()
    {
        // Extension to the Handlebars templating language. Shows the current date and time in the generated output.
        // Usage: {{now}}
        // Example: The time is {{now}}.
        Handlebars.RegisterHelper("now", (output, context, arguments) => { output.WriteSafeString(DateTime.Now); });

        // Generation random date, based on an integer input year value.
        Handlebars.RegisterHelper("randomdate", (output, context, arguments) =>
        {
            if (arguments.Length > 1)
            {
                throw new HandlebarsException("The {{randomdate}} function requires a single integer (year e.g. 1995) value as input.");
            }

            if (arguments.Length == 1)
            {
                bool evaluationResult = Int32.TryParse(arguments[0].ToString(), out var localInteger);
                if (evaluationResult == false)
                {
                    throw new HandlebarsException($"The {{randomdate}} functions failed because {arguments[0]} could not be converted to an integer.");
                }

                output.WriteSafeString(GetRandomDate(localInteger).Date);
            }
        });

        // Generation random string, based on an integer input value cap.
        Handlebars.RegisterHelper("randomnumber", (output, context, arguments) =>
        {
            if (arguments.Length > 1)
            {
                throw new HandlebarsException("The {{randomnumber}} function requires a single integer value as input.");
            }

            if (arguments.Length == 1)
            {
                bool evaluationResult = Int32.TryParse(arguments[0].ToString(), out var localInteger);
                if (evaluationResult == false)
                {
                    throw new HandlebarsException($"The {{randomnumber}} functions failed because {arguments[0]} could not be converted to an integer.");
                }
                output.WriteSafeString(GetRandomNumber(localInteger));
            }
        });

        // Generation random string, based on an integer input value
        Handlebars.RegisterHelper("randomstring", (output, context, arguments) =>
        {
            if (arguments.Length > 1)
            {
                throw new HandlebarsException("The {{randomstring}} function requires a single integer value as input.");
            }

            if (arguments.Length == 1)
            {
                bool evaluationResult = Int32.TryParse(arguments[0].ToString(), out var localInteger);

                if (evaluationResult == false)
                {
                    throw new HandlebarsException($"The {{randomstring}} functions failed because {arguments[0]} could not be converted to an integer.");
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

        Handlebars.RegisterHelper("stringwrap", (writer, context, args) =>
        {
            if (args.Length != 3) throw new HandlebarsException("The {{stringwrap}} function requires exactly three arguments, an object and the string to wrap its value in.");

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

        Handlebars.RegisterHelper("stringupper", (writer, context, args) =>
        {
            if (args.Length != 1) throw new HandlebarsException("The {{stringupper}} function requires one, and only one, input string value.");

            if (args[0].GetType().Name != "UndefinedBindingResult")
            {
                try
                {
                    writer.Write(args[0].ToString().ToUpper());
                }
                catch (Exception ex)
                {
                    writer.WriteSafeString("An issue has been encountered: " + ex.Message + ".");
                }
            }
        });

        Handlebars.RegisterHelper("stringlower", (writer, context, args) =>
        {
            if (args.Length != 1) throw new HandlebarsException("The {{stringlower}} function requires one, and only one, input string value.");

            if (args[0].GetType().Name != "UndefinedBindingResult")
            {
                try
                {
                    writer.Write(args[0].ToString().ToLower());
                }
                catch (Exception ex)
                {
                    writer.WriteSafeString("An issue has been encountered: " + ex.Message + ".");
                }
            }
        });

        // Accept two values, and see if they are the same, use as block helper.
        // Usage {{#stringcompare string1 string2}} do something {{else}} do something else {{/stringcompare}}
        // Usage {{#stringcompare string1 string2}} do something {{/stringcompare}}
        Handlebars.RegisterHelper("stringcompare", (output, options, context, arguments) =>
        {
            if (arguments.Length != 2) throw new HandlebarsException("The {{stringcompare}} function requires exactly two arguments.");

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
        // Usage {{#stringdiff string1 string2}} do something {{else}} do something else {{/stringdiff}}
        // Usage {{#stringdiff string1 string2}} do something {{/stringdiff}}
        Handlebars.RegisterHelper("stringdiff", (output, options, context, arguments) =>
        {
            if (arguments.Length != 2) throw new HandlebarsException("The {{stringdiff}} functions requires exactly two arguments.");

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
                bool evaluationResult = Int32.TryParse(arguments[0].ToString(), out var localInteger);

                if (evaluationResult == false)
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
        Handlebars.RegisterHelper("space", (writer, context, arguments) =>
        {
            if (arguments.Length != 1)
            {
                throw new HandlebarsException("The {{space}} functions requires an input string value to space out against.");
            }

            string outputString = arguments[0].ToString();
            if (outputString.Length < 30)
            {
                outputString = outputString.PadRight(30);
            }
            writer.WriteSafeString(outputString + "\t\t\t\t");

        });

        Handlebars.RegisterHelper("stringReplace", (writer, context, args) =>
        {
            if (args.Length < 3) throw new HandlebarsException("The {{StringReplace}} function requires at least three arguments.");

            if (args[0].GetType().Name != "UndefinedBindingResult")
            {
                try
                {
                    var expression = args[0];

                    if (!String.IsNullOrEmpty(expression.ToString()) && args[0] is JsonElement value)
                    {
                        expression = value.GetString();
                    }

                    string pattern = args[1] as string;
                    string replacement = args[2] as string;

                    expression = expression.ToString().Replace(pattern, replacement);
                    writer.WriteSafeString(expression);
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
                DataObjectMapping dataObjectMapping = JsonSerializer.Deserialize<DataObjectMapping>(context.Value.ToString());

                var dataItemExists = dataObjectMapping.DataItemMappings.Where(x => x.TargetDataItem.Name == searchString).FirstOrDefault();

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
                throw new HandlebarsException($"The {{targetDataItemExists}} helper reported a conversion error, and was unable to deserialize the context into a DataObjectMapping. The reported error is " + exception.Message);
            }
        });

        // Run at data object mapping level.
        Handlebars.RegisterHelper("exists", (output, options, context, arguments) =>
        {
            if (arguments.Length == 0 || arguments.Length > 2) throw new HandlebarsException("The {{exists}} function must have one or two arguments, which must be a property of a data object mapping which is mandatory and an optional exception value.");

            var property = arguments[0] == null ? "" : arguments[0].ToString();

            var propertyValue = String.Empty;
            if (arguments.Length == 2)
            {
                propertyValue = arguments[1]?.ToString();
            }

            // Supported:
            // - multiActiveKey
            // - targetDataItem

            try
            {
                DataObjectMapping dataObjectMapping = JsonSerializer.Deserialize<DataObjectMapping>(context.Value.ToString());

                var outcome = false;

                if (property == "multiActiveKey")
                {
                    var targetDataItemsWithClassifications = new List<DataItemMapping>();
                    if (String.IsNullOrEmpty(propertyValue))
                    {
                        targetDataItemsWithClassifications = dataObjectMapping?.DataItemMappings?.Where(x => x.TargetDataItem.Classifications != null).ToList();
                    }
                    else
                    {
                        targetDataItemsWithClassifications = dataObjectMapping?.DataItemMappings?.Where(x => x.TargetDataItem.Classifications != null && x.TargetDataItem.Name != propertyValue).ToList();
                    }

                    if (targetDataItemsWithClassifications != null)
                    {
                        var dataItemClassifications = targetDataItemsWithClassifications.SelectMany(x => x.TargetDataItem.Classifications).ToList();

                        if (dataItemClassifications != null && dataItemClassifications.Any())
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

        // lookupExtension allows a lookup of an extension value by key value. Pass in the Extensions list and the string key value.
        Handlebars.RegisterHelper("lookupExtension", (writer, context, parameters) =>
        {
            // Check if the parameters are valid.
            if (parameters.Length != 2 || parameters[1] is not string)
            {
                throw new HandlebarsException("{{lookupExtension}} helper expects two arguments: a List<Extension> and a string lookup key");
            }

            try
            {
                var extensionList = JsonSerializer.Deserialize<List<Extension>>(parameters[0].ToString() ?? string.Empty);
                var key = (string)parameters[1];
                var result = extensionList?.Find(i => i.Key.Equals(key, StringComparison.OrdinalIgnoreCase))?.Value ?? "";


                writer.WriteSafeString($"{result}");
            }
            catch (Exception exception)
            {
                throw new HandlebarsException($"{{lookupExtension}} encountered an error: the list of extensions provided as the first argument could not be deserialized. The reported error is :{exception.Message}");
            }
        });
    }
}
