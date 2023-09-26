using System.IO.Enumeration;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using DataWarehouseAutomation;

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
    var inputMetadataDirectory = @"D:\TeamEnvironments\VDW\Metadata";
    var outputMetadataDirectory = @"D:\TeamEnvironments\VDW\MetadataV2";

    var exceptionList = new List<string>();
    exceptionList.Add("VDW_Samples_TEAM_Attribute_Mapping.json");
    exceptionList.Add("sample_TEAM_Attribute_Mapping.json");

    foreach (string file in Directory.EnumerateFiles(inputMetadataDirectory, "*.json", SearchOption.TopDirectoryOnly))
    {
        if (!exceptionList.Contains(Path.GetFileName(file)))
        {
            Console.WriteLine(file);

            // Fetch the content of the Json files.
            string jsonFile = File.ReadAllText(file);

            // Create a JSON object, which can be modified at runtime.
            var jsonObject = JsonNode.Parse(jsonFile).AsObject();

            #region Generation Specific Data Object

            if (jsonObject["generationSpecificMetadata"] != null)
            {
                try
                {
                    var generationSpecificDataObjectDataItemList = new List<JsonObject>();
                    foreach (var generationSpecificDataObjectDataItem in jsonObject["generationSpecificMetadata"]!["selectedDataObject"]!["dataItems"].AsArray())
                    {
                        var jsonObjectGenerationSpecificDataObjectDataItem = JsonNode.Parse(generationSpecificDataObjectDataItem.ToJsonString()).AsObject();

                        // Type must be first.
                        jsonObjectGenerationSpecificDataObjectDataItem.Add("dataItemType", "dataItem");

                        // Re-add unchanged properties to manage order.
                        AddUnchangedDataItemProperties(jsonObjectGenerationSpecificDataObjectDataItem);

                        // Replace properties with newer names (upgrade).
                        ReplaceDataObjectProperties(jsonObjectGenerationSpecificDataObjectDataItem);

                        // Add the target data objects to a list so that they can be added to the main object later.
                        generationSpecificDataObjectDataItemList.Add(jsonObjectGenerationSpecificDataObjectDataItem);
                    }

                    // Add to the main object again in updated format.
                    foreach (var dataItem in generationSpecificDataObjectDataItemList)
                    {
                        jsonObject["generationSpecificMetadata"]!["selectedDataObject"]!["dataItems"]![generationSpecificDataObjectDataItemList.IndexOf(dataItem)] = dataItem;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            #endregion


            // Start parsing.
            var dataObjectMappingJsonObjectList = new List<JsonObject>();

            foreach (var dataObjectMapping in jsonObject["dataObjectMappings"].AsArray())
            {
                // Data Object Mapping level.
                var jsonObjectDataObjectMapping = JsonNode.Parse(dataObjectMapping.ToJsonString()).AsObject();

                // Set the mapping name.
                var mappingName = jsonObjectDataObjectMapping["mappingName"];
                jsonObjectDataObjectMapping.Remove("mappingName");
                jsonObjectDataObjectMapping.Add("name", mappingName);

                // Set the mapping classifications.
                var mappingClassifications = jsonObjectDataObjectMapping["mappingClassifications"];
                jsonObjectDataObjectMapping.Remove("mappingClassifications");
                jsonObjectDataObjectMapping.Add("classifications", mappingClassifications);

                #region Source Data Objects
                var sourceDataObjectList = new List<JsonObject>();
                foreach (var sourceDataObject in dataObjectMapping["sourceDataObjects"].AsArray())
                {
                    // Source data object level.
                    var jsonObjectSourceDataObject = JsonNode.Parse(sourceDataObject.ToJsonString()).AsObject();

                    // Type must be first.
                    jsonObjectSourceDataObject.Add("dataObjectType", "dataObject");

                    // Re-add unchanged properties to manage order.
                    AddUnchangedDataObjectProperties(jsonObjectSourceDataObject);

                    // Replace properties with newer names (upgrade).
                    ReplaceDataObjectProperties(jsonObjectSourceDataObject);

                    // Data Items.
                    var sourceDataObjectDataItemList = new List<JsonObject>();
                    foreach (var sourceDataObjectDataItem in jsonObjectSourceDataObject["dataItems"].AsArray())
                    {
                        var jsonObjectSourceDataObjectDataItem = JsonNode.Parse(sourceDataObjectDataItem.ToJsonString()).AsObject();

                        // Type must be first.
                        jsonObjectSourceDataObjectDataItem.Add("dataItemType", "dataItem");

                        // Re-add unchanged properties to manage order.
                        AddUnchangedDataItemProperties(jsonObjectSourceDataObjectDataItem);

                        // Replace properties with newer names (upgrade).
                        ReplaceDataObjectProperties(jsonObjectSourceDataObjectDataItem);

                        sourceDataObjectDataItemList.Add(jsonObjectSourceDataObjectDataItem);
                    }

                    foreach (var dataItem in sourceDataObjectDataItemList)
                    {
                        jsonObjectSourceDataObject["dataItems"]![sourceDataObjectDataItemList.IndexOf(dataItem)] = dataItem;
                    }

                    // Add the target data objects to a list so that they can be added to the main object later.
                    sourceDataObjectList.Add(jsonObjectSourceDataObject);
                }

                foreach (var sourceDataObjectJsonObject in sourceDataObjectList)
                {
                    jsonObjectDataObjectMapping["sourceDataObjects"]![sourceDataObjectList.IndexOf(sourceDataObjectJsonObject)] = sourceDataObjectJsonObject;
                }
                #endregion

                #region Target Data Object

                try
                {
                    var targetDataObjectDataItemList = new List<JsonObject>();
                    foreach (var targetDataObjectDataItem in dataObjectMapping["targetDataObject"]!["dataItems"].AsArray())
                    {
                        var jsonObjectTargetDataObjectDataItem = JsonNode.Parse(targetDataObjectDataItem.ToJsonString()).AsObject();

                        // Type must be first.
                        jsonObjectTargetDataObjectDataItem.Add("dataItemType", "dataItem");

                        // Re-add unchanged properties to manage order.
                        AddUnchangedDataItemProperties(jsonObjectTargetDataObjectDataItem);

                        // Replace properties with newer names (upgrade).
                        ReplaceDataObjectProperties(jsonObjectTargetDataObjectDataItem);

                        // Add the target data objects to a list so that they can be added to the main object later.
                        targetDataObjectDataItemList.Add(jsonObjectTargetDataObjectDataItem);
                    }

                    // Add to the main object again in updated format.
                    foreach (var dataItem in targetDataObjectDataItemList)
                    {
                        jsonObjectDataObjectMapping["targetDataObject"]!["dataItems"]![targetDataObjectDataItemList.IndexOf(dataItem)] = dataItem;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                #endregion

                #region Related Data Objects

                if (dataObjectMapping["relatedDataObjects"] != null)
                {
                    var relatedDataObjectList = new List<JsonObject>();
                    foreach (var relatedDataObject in dataObjectMapping["relatedDataObjects"].AsArray())
                    {
                        var jsonObjectRelatedDataObject = JsonNode.Parse(relatedDataObject.ToJsonString()).AsObject();

                        // Data Items.
                        var relatedDataObjectDataItemList = new List<JsonObject>();
                        foreach (var relatedDataObjectDataItem in jsonObjectRelatedDataObject["dataItems"].AsArray())
                        {
                            var jsonObjectRelatedDataObjectDataItem = JsonNode.Parse(relatedDataObjectDataItem.ToJsonString()).AsObject();

                            // Type must be first.
                            jsonObjectRelatedDataObjectDataItem.Add("dataItemType", "dataItem");

                            // Re-add unchanged properties to manage order.
                            AddUnchangedDataItemProperties(jsonObjectRelatedDataObjectDataItem);

                            // Replace properties with newer names (upgrade).
                            ReplaceDataObjectProperties(jsonObjectRelatedDataObjectDataItem);

                            relatedDataObjectDataItemList.Add(jsonObjectRelatedDataObjectDataItem);
                        }

                        foreach (var dataItem in relatedDataObjectDataItemList)
                        {
                            jsonObjectRelatedDataObject["dataItems"]![relatedDataObjectDataItemList.IndexOf(dataItem)] = dataItem;
                        }

                        relatedDataObjectList.Add(jsonObjectRelatedDataObject);
                    }

                    foreach (var dataObject in relatedDataObjectList)
                    {
                        jsonObjectDataObjectMapping["relatedDataObjects"]![relatedDataObjectList.IndexOf(dataObject)] = dataObject;
                    }
                }

                #endregion

                #region Data Item Mappings
                var dataItemMappingList = new List<JsonObject>();
                if (dataObjectMapping["dataItemMappings"] != null)
                {
                    foreach (var dataItemMappings in dataObjectMapping["dataItemMappings"]?.AsArray())
                    {
                        // Data Item Mapping level.
                        var jsonObjectDataItemMapping = JsonNode.Parse(dataItemMappings.ToJsonString()).AsObject();

                        var sourceDataItemList = new List<JsonObject>();
                        foreach (var sourceDataItem in dataItemMappings["sourceDataItems"].AsArray())
                        {
                            // Source data item level.
                            var jsonObjectSourceDataItem = JsonNode.Parse(sourceDataItem.ToJsonString()).AsObject();

                            jsonObjectSourceDataItem.Add("dataItemType", "dataItem");

                            // Re-add unchanged properties to manage order.
                            AddUnchangedDataItemProperties(jsonObjectSourceDataItem);

                            // Replace properties with newer names (upgrade).
                            ReplaceDataObjectProperties(jsonObjectSourceDataItem);

                            sourceDataItemList.Add(jsonObjectSourceDataItem);
                        }

                        foreach (var sourceDataItemJsonObject in sourceDataItemList)
                        {
                            jsonObjectDataItemMapping["sourceDataItems"]![sourceDataItemList.IndexOf(sourceDataItemJsonObject)] = sourceDataItemJsonObject;
                        }

                        dataItemMappingList.Add(jsonObjectDataItemMapping);
                    }

                    foreach (var dataItemMappingJsonObject in dataItemMappingList)
                    {
                        jsonObjectDataObjectMapping["dataItemMappings"]![dataItemMappingList.IndexOf(dataItemMappingJsonObject)] = dataItemMappingJsonObject;
                    }
                }

                #endregion

                dataObjectMappingJsonObjectList.Add(jsonObjectDataObjectMapping);
            }

            // Put the data object mappings back into the main object.
            foreach (var dataObjectMappingJsonObject in dataObjectMappingJsonObjectList)
            {
                jsonObject["dataObjectMappings"]![dataObjectMappingJsonObjectList.IndexOf(dataObjectMappingJsonObject)] = dataObjectMappingJsonObject;
            }

            // Finalisation
            var options = new JsonSerializerOptions { WriteIndented = true };
            Console.WriteLine(jsonObject!.ToJsonString(options));

            //// Validation
            //var result = JsonValidation.ValidateJsonFileAgainstSchema(jsonSchema, jsonObject!.ToJsonString(options));

            //foreach (var error in result.Errors)
            //{
            //    Console.Write($"   Validation error {error.Message} at line {error.LineNumber} position {error.LinePosition} of error type {error.ErrorType}. This is related to {error.Path}.");
            //}

            try
            {
                var test = JsonSerializer.Deserialize<DataObjectMappingList>(jsonObject.ToJsonString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{file} error {ex}");
                Console.ReadKey();
            }
            finally
            {
                // Save the file to disk.
                var fileName = outputMetadataDirectory + @"\" + Path.GetFileName(file);
                using (var outfile = new StreamWriter(fileName))
                {
                    outfile.Write(jsonObject!.ToJsonString(options));
                    outfile.Close();
                }
            }
        }
    }
}

// Finish the application
Console.ReadKey();

void ReAddProperty(string input, JsonObject jsonObject)
{
    var property = jsonObject[input];

    if (property != null)
    {
        jsonObject.Remove(input);
        jsonObject.Add(input, property);
    }
}

void ReplaceProperty(string inputOld, string inputNew, JsonObject jsonObject)
{
    var property = jsonObject[inputOld];

    if (property != null)
    {
        jsonObject.Remove(inputOld);
        jsonObject.Add(inputNew, property);
    }
}

void AddUnchangedDataItemProperties(JsonObject jsonObject)
{
    ReAddProperty("name", jsonObject);
    ReAddProperty("dataType", jsonObject);
    ReAddProperty("characterLength", jsonObject);
    ReAddProperty("ordinalPosition", jsonObject);
    ReAddProperty("numericScale", jsonObject);
    ReAddProperty("numericPrecision", jsonObject);
}

void AddUnchangedDataObjectProperties(JsonObject jsonObject)
{
    ReAddProperty("name", jsonObject);
    ReAddProperty("dataItems", jsonObject);
    ReAddProperty("dataConnection", jsonObject);
    ReAddProperty("classifications", jsonObject);
    ReAddProperty("extensions", jsonObject);
}

void ReplaceDataObjectProperties(JsonObject jsonObject)
{
    ReplaceProperty("dataObjectConnection", "dataConnection", jsonObject);
    ReplaceProperty("dataObjectClassifications", "classifications", jsonObject);
}