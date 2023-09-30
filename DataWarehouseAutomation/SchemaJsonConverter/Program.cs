using System.Drawing;
using System.IO.Enumeration;
using System.Linq;
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
    var inputMetadataDirectory = @"C:\AutomationEnvironments\VDW\Metadata";
    var outputMetadataDirectory = @"C:\AutomationEnvironments\VDW\MetadataV2";

    var exceptionList = new List<string>
    {
        "VDW_Samples_TEAM_Attribute_Mapping.json",
        "sample_TEAM_Attribute_Mapping.json"
    };

    foreach (string file in Directory.EnumerateFiles(inputMetadataDirectory, "*.json", SearchOption.TopDirectoryOnly))
    {
        // TODO
        // Infer group for classification.Set property upon import.

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
                    var dataObject = jsonObject["generationSpecificMetadata"]!["selectedDataObject"];
                    var dataObjectJsonObject = JsonNode.Parse(dataObject.ToJsonString()).AsObject();

                    // Extensions
                    if (dataObjectJsonObject["extensions"] == null)
                    {
                        dataObjectJsonObject.Add("extensions", null);
                    }

                    // Replace properties with newer names (upgrade).
                    ReplaceDataObjectProperties(dataObjectJsonObject);

                    // Connection.
                    UpdateDataConnection(dataObjectJsonObject);

                    jsonObject["generationSpecificMetadata"]!["selectedDataObject"] = dataObjectJsonObject;

                    // Data items.
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
            var dataObjectMappingJsonArray = new JsonArray();

            foreach (var dataObjectMapping in jsonObject["dataObjectMappings"].AsArray())
            {
                // Data Object Mapping level.
                var jsonObjectDataObjectMapping = JsonNode.Parse(dataObjectMapping.ToJsonString()).AsObject();

                // Rename the mapping name.
                var mappingNameNode = jsonObjectDataObjectMapping["mappingName"];
                jsonObjectDataObjectMapping.Remove("mappingName");
                jsonObjectDataObjectMapping.Add("name", mappingNameNode);

                var getName = jsonObjectDataObjectMapping.TryGetPropertyValue("name", out var mappingNameJsonNode).ToString();

                // Add the mapping name as a 'name' to the list of mappings, only once.
                if (jsonObject["dataObjectMappings"].AsArray().IndexOf(dataObjectMapping) == 0)
                {
                    jsonObject["name"] = mappingNameJsonNode.ToString();
                }

                // Rename the mapping classifications.
                var mappingClassificationsNode = jsonObjectDataObjectMapping["mappingClassifications"];
                jsonObjectDataObjectMapping.Remove("mappingClassifications");
                jsonObjectDataObjectMapping.Add("classifications", mappingClassificationsNode);

                // Update the classifications
                if (jsonObjectDataObjectMapping["classifications"] != null)
                {
                    try
                    {
                        foreach (var classification in jsonObjectDataObjectMapping["classifications"].AsArray())
                        {
                            var classificationJsonObject = JsonNode.Parse(classification.ToJsonString()).AsObject();
                            var getClassification = classificationJsonObject
                                .TryGetPropertyValue("classification", out var classificationValue).ToString();


                            //                        public string groupValue) => classificationValue.ToString() 
                            //switch
                            //                            {
                            //                                "CoreBusinessConcept" => new NewSwitch(0xFF, 0x00, 0x00),
                            //                                _ => throw new ArgumentException(message: "error", paramName: nameof(color)),
                            //                            };

                            string groupValue = classificationValue.ToString() switch
                            {
                                "Source" => "Solution Layer",
                                "Core Business Concept" => "Logical",
                                "CoreBusinessConcept" => "Logical",
                                "Integration" => "Solution Layer",
                                "Context" => "Logical",
                                "Persistent Staging" => "Solution Layer",
                                "PersistentStaging" => "Solution Layer",
                                "Staging" => "Solution Layer",
                                "StagingArea" => "Solution Layer",
                                "Thing" => "Conceptual",
                                "Presentation" => "Solution Layer",
                                "Natural Business Relationship" => "Logical",
                                "NaturalBusinessRelationship" => "Logical",
                                "Natural Business Relationship Context" => "Logical",
                                "NaturalBusinessRelationshipContext" => "Logical",
                                "Natural Business Relationship Context Driving Key" => "Logical",
                                "NaturalBusinessRelationshipContextDrivingKey" => "Logical",
                                "Person" => "Conceptual",
                                "Place" => "Conceptual",
                                "Hub" => "Physical",
                                "Event" => "Physical",
                                "Satellite" => "Conceptual",
                                "Link" => "Physical",
                                _ => "Unknown"
                            };

                            classificationJsonObject.Add("group",groupValue);
                        }


                    }
                    catch (Exception ex)
                    {

                    }
                }

                // Rename the business key definitions.
                        var businessKeyDefinitionsNode = jsonObjectDataObjectMapping["businessKeys"];
                jsonObjectDataObjectMapping.Remove("businessKeys");
                jsonObjectDataObjectMapping.Add("businessKeyDefinitions", businessKeyDefinitionsNode);

                #region Source Data Objects

                try
                {
                    var dataObjectList = new List<JsonObject>();
                    foreach (var dataObject in dataObjectMapping["sourceDataObjects"].AsArray())
                    {
                        // Source data object level.
                        var dataObjectJsonObject = JsonNode.Parse(dataObject.ToJsonString()).AsObject();

                        // Type must be first.
                        dataObjectJsonObject.Add("dataObjectType", "dataObject");

                        // Re-add unchanged properties to manage order.
                        AddUnchangedDataObjectProperties(dataObjectJsonObject);

                        // Replace properties with newer names (upgrade).
                        ReplaceDataObjectProperties(dataObjectJsonObject);

                        var getSourceDataObjectName = dataObjectJsonObject.TryGetPropertyValue("name", out var sourceDataObjectNameJsonNode).ToString();

                        // Add the mapping name as a 'name' to the list of mappings, only once.
                        if (jsonObject["dataObjectMappings"].AsArray().IndexOf(dataObjectMapping) == 0)
                        {
                            jsonObjectDataObjectMapping["name"] = sourceDataObjectNameJsonNode.ToString() + " to " + mappingNameJsonNode.ToString();
                        }

                        // Data Items.
                        var dataItems = new List<JsonObject>();
                        foreach (var dataItem in dataObjectJsonObject["dataItems"].AsArray())
                        {
                            var dataItemJsonObject = JsonNode.Parse(dataItem.ToJsonString()).AsObject();

                            // Type must be first.
                            dataItemJsonObject.Add("dataItemType", "dataItem");

                            // Re-add unchanged properties to manage order.
                            AddUnchangedDataItemProperties(dataItemJsonObject);

                            // Replace properties with newer names (upgrade).
                            ReplaceDataObjectProperties(dataItemJsonObject);

                            dataItems.Add(dataItemJsonObject);
                        }

                        foreach (var dataItem in dataItems)
                        {
                            dataObjectJsonObject["dataItems"]![dataItems.IndexOf(dataItem)] = dataItem;
                        }

                        // Extensions
                        if (dataObjectJsonObject["extensions"] == null)
                        {
                            dataObjectJsonObject.Add("extensions", null);
                        }

                        // Connection.
                        UpdateDataConnection(dataObjectJsonObject);

                        // Add the target data objects to a list so that they can be added to the main object later.
                        dataObjectList.Add(dataObjectJsonObject);
                    }

                    // Add the segment back.
                    foreach (var sourceDataObjectJsonObject in dataObjectList)
                    {
                        jsonObjectDataObjectMapping["sourceDataObjects"]![dataObjectList.IndexOf(sourceDataObjectJsonObject)] = sourceDataObjectJsonObject;
                    }
                }
                catch (Exception ex)
                {
                    //
                }

                #endregion

                #region Target Data Object

                try
                {
                    // Data object.
                    var dataObject = jsonObjectDataObjectMapping["targetDataObject"];
                    var dataObjectJsonObject = JsonNode.Parse(dataObject.ToJsonString()).AsObject();

                    // Extensions
                    if (dataObjectJsonObject["extensions"] == null)
                    {
                        dataObjectJsonObject.Add("extensions", null);
                    }

                    // Replace properties with newer names (upgrade).
                    ReplaceDataObjectProperties(dataObjectJsonObject);

                    // Connection.
                    UpdateDataConnection(dataObjectJsonObject);

                    jsonObjectDataObjectMapping["targetDataObject"] = dataObjectJsonObject;

                    /// Data items.
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

                try
                {
                    if (dataObjectMapping["relatedDataObjects"] != null)
                    {
                        var dataObjects = new List<JsonObject>();
                        foreach (var dataObject in dataObjectMapping["relatedDataObjects"].AsArray())
                        {
                            // Data Object.
                            var dataObjectJsonObject = JsonNode.Parse(dataObject.ToJsonString()).AsObject();

                            // Extensions
                            if (dataObjectJsonObject["extensions"] == null)
                            {
                                dataObjectJsonObject.Add("extensions", null);
                            }

                            // Replace properties with newer names (upgrade).
                            ReplaceDataObjectProperties(dataObjectJsonObject);

                            // Connection.
                            UpdateDataConnection(dataObjectJsonObject);

                            // Data Items.
                            var dataItems = new List<JsonObject>();
                            foreach (var dataItem in dataObjectJsonObject["dataItems"].AsArray())
                            {
                                var dataItemJsonObject = JsonNode.Parse(dataItem.ToJsonString()).AsObject();

                                // Type must be first.
                                dataItemJsonObject.Add("dataItemType", "dataItem");

                                // Re-add unchanged properties to manage order.
                                AddUnchangedDataItemProperties(dataItemJsonObject);

                                // Replace properties with newer names (upgrade).
                                ReplaceDataObjectProperties(dataItemJsonObject);

                                dataItems.Add(dataItemJsonObject);
                            }

                            foreach (var dataItem in dataItems)
                            {
                                dataObjectJsonObject["dataItems"]![dataItems.IndexOf(dataItem)] = dataItem;
                            }

                            dataObjects.Add(dataObjectJsonObject);
                        }

                        foreach (var dataObject in dataObjects)
                        {
                            jsonObjectDataObjectMapping["relatedDataObjects"]![dataObjects.IndexOf(dataObject)] = dataObject;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //
                }

                #endregion

                #region Data Item Mappings

                try
                {
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
                                ReplaceDataItemProperties(jsonObjectSourceDataItem);

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
                }
                catch (Exception ex)
                {
                    //
                }

                #endregion

                #region Business Key Definitions

                try
                {
                    if (jsonObjectDataObjectMapping["businessKeyDefinitions"] != null)
                    {
                        // Business Key definitions.
                        var businessKeyDefinitions = new List<JsonObject>();
                        foreach (var businessKeyComponentMapping in jsonObjectDataObjectMapping["businessKeyDefinitions"]?.AsArray())
                        {
                            var businessKeyComponentMappingJsonObject = JsonNode.Parse(businessKeyComponentMapping.ToJsonString()).AsObject();

                            //  Business Key component mapping (data item mapping).
                            var dataItemMappingList = new List<JsonObject>();
                            foreach (var dataItemMapping in businessKeyComponentMapping!["businessKeyComponentMapping"]?.AsArray())
                            {
                                var dataMappingJsonObject = JsonNode.Parse(dataItemMapping.ToJsonString()).AsObject();

                                var dataItemList = new List<JsonObject>();
                                foreach (var dataItem in dataItemMapping!["sourceDataItems"]?.AsArray())
                                {
                                    // Source data item.
                                    var dataItemJsonObject = JsonNode.Parse(dataItem.ToJsonString()).AsObject();

                                    dataItemJsonObject.Add("dataItemType", "dataItem");

                                    // Re-add unchanged properties to manage order.
                                    AddUnchangedDataItemProperties(dataItemJsonObject);

                                    // Replace properties with newer names (upgrade).
                                    ReplaceDataItemProperties(dataItemJsonObject);

                                    // Change isHardCodedValue into extension.
                                    if (dataItemJsonObject.ContainsKey("isHardCodedValue"))
                                    {
                                        var keyExists = dataItemJsonObject.TryGetPropertyValue("key", out var jsonNode).ToString();

                                        if (keyExists == "True")
                                        {
                                            if (jsonNode != null && jsonNode.ToString() == "true")
                                            {
                                                // Create an extension.
                                                var extension = new JsonObject()
                                                {
                                                    ["key"] = "isHardCodedValue",
                                                    ["value"] = "true",
                                                    ["notes"] = "database name"
                                                };

                                                // Extensions
                                                if (dataItemJsonObject["extensions"] == null)
                                                {
                                                    dataItemJsonObject.Add("extensions", extension);
                                                }
                                                else
                                                {
                                                    var extensionArray = new JsonArray();
                                                    extensionArray = dataItemJsonObject["extensions"]?.AsArray();
                                                    extensionArray.Add(extension);
                                                    dataItemJsonObject["extensions"] = extensionArray;
                                                }
                                            }
                                        }

                                        dataItemJsonObject.Remove("isHardCodedValue");
                                    }

                                    dataItemList.Add(dataItemJsonObject);

                                    foreach (var sourceDataItemJsonObject in dataItemList)
                                    {
                                        dataMappingJsonObject["sourceDataItems"]![dataItemList.IndexOf(sourceDataItemJsonObject)] = sourceDataItemJsonObject;
                                    }

                                    dataItemMappingList.Add(dataMappingJsonObject);
                                }
                            }

                            foreach (var dataItemMappingJsonObject in dataItemMappingList)
                            {
                                businessKeyComponentMappingJsonObject["businessKeyComponentMapping"]![dataItemMappingList.IndexOf(dataItemMappingJsonObject)] = dataItemMappingJsonObject;
                            }

                            RenameProperty("businessKeyComponentMapping", "businessKeyComponentMappings", businessKeyComponentMappingJsonObject);
                            businessKeyDefinitions.Add(businessKeyComponentMappingJsonObject);
                        }

                        foreach (var businessKeyComponent in businessKeyDefinitions)
                        {
                            jsonObjectDataObjectMapping["businessKeyDefinitions"]![businessKeyDefinitions.IndexOf(businessKeyComponent)] = businessKeyComponent;
                        }


                    }
                }
                catch (Exception ex)
                {
                    //
                }

                #endregion

                dataObjectMappingJsonArray.Add(jsonObjectDataObjectMapping);
            }

            // Put the data object mappings back into the main object.
            jsonObject["dataObjectMappings"] = dataObjectMappingJsonArray;

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
                var testSerialisation = JsonSerializer.Deserialize<DataObjectMappingList>(jsonObject.ToJsonString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{file} error {ex}");
                Console.ReadKey();
            }
            finally
            {

            }

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

void RenameProperty(string inputOld, string inputNew, JsonObject jsonObject)
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
    ReAddProperty("classifications", jsonObject);
    ReAddProperty("extensions", jsonObject);
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
    RenameProperty("dataObjectConnection", "dataConnection", jsonObject);
    RenameProperty("dataObjectClassifications", "classifications", jsonObject);
}

void ReplaceDataItemProperties(JsonObject jsonObject)
{
    RenameProperty("dataItemClassification", "classifications", jsonObject);
}

void UpdateDataConnection(JsonObject dataObjectJsonObject)
{
    // Connection.
    if (dataObjectJsonObject["dataConnection"] != null)
    {
        var dataConnectionNode = dataObjectJsonObject["dataConnection"];
        var dataConnectionObject = JsonNode.Parse(dataConnectionNode.ToJsonString()).AsObject();

        // Rename the dataObjectConnection to dataConnection.
        RenameProperty("dataObjectConnection", "dataConnection", dataObjectJsonObject);

        // Rename the dataConnectionString to name.
        RenameProperty("dataConnectionString", "name", dataConnectionObject);

        // Extensions.
        if (dataConnectionObject["extensions"] != null)
        {
            var extensionArray = new JsonArray();
            foreach (var extension in dataConnectionObject["extensions"].AsArray())
            {
                var extensionObject = JsonNode.Parse(extension.ToJsonString()).AsObject();

                // Rename the description to notes.
                RenameProperty("description", "notes", extensionObject);

                var keyExists = extensionObject.TryGetPropertyValue("key", out var jsonNode).ToString();

                if (jsonNode.ToString() == "database")
                {
                    extensionObject["key"] = "datastore";
                }

                if (jsonNode.ToString() == "schema")
                {
                    extensionObject["key"] = "location";
                }
                extensionArray.Add(extensionObject);
            }

            // Move the connection extensions to the data object.
            dataConnectionObject.Remove("extensions");
            dataObjectJsonObject["dataConnection"] = dataConnectionObject;

            dataObjectJsonObject!["extensions"] = extensionArray;
        }

        dataObjectJsonObject["dataConnection"] = dataConnectionObject;
    }
}