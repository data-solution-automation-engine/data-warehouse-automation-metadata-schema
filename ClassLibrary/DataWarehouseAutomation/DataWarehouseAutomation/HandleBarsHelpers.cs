using System;
using System.IO;
using HandlebarsDotNet;

namespace DataWarehouseAutomation
{
    public class HandleBarsHelpers
    {
        public static void RegisterHandleBarsHelpers()
        {
            // Generation Date/Time functional helper
            Handlebars.RegisterHelper("now", (writer, context, parameters) => { writer.WriteSafeString(DateTime.Now); });

            // Normal helper
            Handlebars.RegisterHelper("samecheck", (output, options, context, data) =>
            {
                //if (data.Length > 10)
                //    output.Write("More than 10!");
                //else
                //    output.Write("Something else! "+data[0]);

                if (data[0].ToString() == data[1].ToString())
                    output.Write("It's the same: " + (object)context);
                else
                    output.Write("Something else! " + data[0]);

            });

            // Block helper
            Handlebars.RegisterHelper("TenOrMore", (output, options, context, data) =>
            {
                if (data[0].ToString() == data[1].ToString())
                    options.Template(output, (object)context);

            });

            // Character spacing not satisfactory? Do not panic, help is near! Make sure the character spacing is righteous using this Handlebars helper.
            // Usage {{space sourceDataObject.name}} will space out (!?) the name of the source to 30 characters and a few tabs for lots of white spaces.
            Handlebars.RegisterHelper("space", (writer, context, args) =>
            {
                string outputString = args[0].ToString();
                if (outputString.Length < 30)
                {
                    outputString = outputString.PadRight(30);
                }
                writer.WriteSafeString(outputString + "\t\t\t\t");

            });

            Handlebars.RegisterHelper("StringReplace", (writer, context, args) =>
            {
                if (args.Length < 3) throw new HandlebarsException("The {{StringReplace}} function requires at least three arguments.");

                string expression = args[0] as string;

                if (args[0] is Newtonsoft.Json.Linq.JValue)
                {
                    expression = ((Newtonsoft.Json.Linq.JValue)args[0]).Value.ToString();
                }

                string pattern = args[1] as string;
                string replacement = args[2] as string;

                expression = expression.Replace(pattern, replacement);
                writer.WriteSafeString(expression);

            });


            // BLOCK HELPER
            //_handlebars.RegisterHelper("if_kpi", (writer, options, context, parameters) =>
            //{
            //    string group = Convert.ToString(parameters[0]);

            //    if (group == Enum.GetName(typeof(KPICategoryGroupEnum), KPICategoryGroupEnum.KPI))
            //    {
            //        options.Template(writer, (object)context);
            //    }
            //    else
            //    {
            //        options.Inverse(writer, (object)context);
            //    }
            //});

            //{
            //    {#if_equal x "my_string"}}
            //        x is "my_string"
            //        { {else} }
            //        x isn't "my_string"
            //        { {/ if_equal} }

            //public void RegisterHandleBarsHelperEvaluateClassificationType()
            //{
            //    Handlebars.RegisterHelper("ShowContactList", (output, context, parameters) =>
            //    {
            //        var contacts = string.Empty;
            //        for (var i = 0; i < context.Buyers.Length; i++)
            //        {
            //            contacts += context.Buyers[i].FirstName + " " + context.Buyers[i].LastName + ",";
            //        }

            //        output.WriteSafeString("Contacts: " + contacts);
            //    });
            //}


            //Handlebars.registerHelper('if_equal', function(a, b, opts) {
            //    if (a == b)
            //    {
            //        return opts.fn(this)
            //    }
            //    else
            //    {
            //        return opts.inverse(this)
            //    }
            //})


            // Accept two values, and see if they are the same, use as block helper.
            // Usage {{#stringcompare string1 string2}} do something {{else}} do something else {{/stringcompare}}
            // Usage {{#stringcompare string1 string2}} do something {{/stringcompare}}
            Handlebars.RegisterHelper("stringequal", (output, options, context, arguments) =>
            {
                if (arguments.Length != 2) throw new HandlebarsException("The {{stringcompare}} functions requires exactly two arguments.");

                var leftString = arguments[0] == null ? "" : arguments[0].ToString();
                var rightString = arguments[1] == null ? "" : arguments[1].ToString();

                if (leftString == rightString)
                {
                    options.Template(output, (object)context);
                }
                else
                {
                    options.Inverse(output, (object)context);
                }
            });

            // Accept two values, and do something if they are the different.
            // Usage {{#stringdiff string1 string2}} do something {{else}} do something else {{/stringdiff}}
            // Usage {{#stringdiff string1 string2}} do something {{/strinstringdiffgcompare}}
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
        }

    }
}
