using System.Text.Json;
using System.Text.Json.Nodes;
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

    var exceptionList = new List<string>
    {
        "VDW_Samples_TEAM_Attribute_Mapping.json",
        "sample_TEAM_Attribute_Mapping.json"
    };

    #region GUIDs
    // GUID for object, item, connection, classification, businessKeyDefinition, dataObjectMapping, dataItemMapping, mappinglist
    Dictionary<string, Guid> dataObjectGuids = new Dictionary<string, Guid>();
    Dictionary<string, Guid> dataItemGuids = new Dictionary<string, Guid>();
    Dictionary<string, Guid> connectionGuids = new Dictionary<string, Guid>();
    Dictionary<string, Guid> classificationGuids = new Dictionary<string, Guid>();
    Dictionary<string, Guid> dataObjectMappingGuids = new Dictionary<string, Guid>();
    Dictionary<string, Guid> businessKeyDefinitionGuids = new Dictionary<string, Guid>();
    Dictionary<string, Guid> dataItemMappingGuids = new Dictionary<string, Guid>();

    #endregion

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
                    var dataObject = jsonObject["generationSpecificMetadata"]!["selectedDataObject"];
                    var dataObjectJsonObject = JsonNode.Parse(dataObject.ToJsonString()).AsObject();

                    // Extensions
                    if (dataObjectJsonObject["extensions"] == null)
                    {
                        dataObjectJsonObject.Add("extensions", null);
                    }

                    // Ensure each object has a GUID
                    AddGuid(dataObjectJsonObject, dataObjectGuids, "name");

                    // Replace properties with newer names (upgrade).
                    ReplaceDataObjectProperties(dataObjectJsonObject);

                    // Connection.
                    UpdateDataConnection(dataObjectJsonObject, connectionGuids);

                    // Update the classifications
                    UpdateClassifications(dataObjectJsonObject, classificationGuids);

                    jsonObject["generationSpecificMetadata"]!["selectedDataObject"] = dataObjectJsonObject;

                    // Data items.
                    var generationSpecificDataObjectDataItemList = new List<JsonObject>();
                    foreach (var generationSpecificDataObjectDataItem in jsonObject["generationSpecificMetadata"]!["selectedDataObject"]!["dataItems"].AsArray())
                    {
                        var jsonObjectGenerationSpecificDataObjectDataItem = JsonNode.Parse(generationSpecificDataObjectDataItem.ToJsonString()).AsObject();

                        // Type must be first.
                        jsonObjectGenerationSpecificDataObjectDataItem.Add("dataItemType", "dataItem");

                        // Ensure each item has a GUID
                        AddGuid(jsonObjectGenerationSpecificDataObjectDataItem, dataItemGuids, "name");

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
                UpdateClassifications(jsonObjectDataObjectMapping, classificationGuids);

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

                        // Ensure each object has a GUID
                        AddGuid(dataObjectJsonObject, dataObjectGuids, "name");

                        // Re-add unchanged properties to manage order.
                        AddUnchangedDataObjectProperties(dataObjectJsonObject);

                        // Replace properties with newer names (upgrade).
                        ReplaceDataObjectProperties(dataObjectJsonObject);

                        // Update the classifications
                        UpdateClassifications(dataObjectJsonObject, classificationGuids);

                        var getSourceDataObjectName = dataObjectJsonObject.TryGetPropertyValue("name", out var sourceDataObjectNameJsonNode).ToString();

                        // Add the mapping name as a 'name' to the list of mappings, only once.
                        //if (jsonObject["dataObjectMappings"].AsArray().IndexOf(dataObjectMapping) == 0)
                        //{
                            var tempMappingName = sourceDataObjectNameJsonNode.ToString() + " to " + mappingNameJsonNode.ToString();
                            jsonObjectDataObjectMapping["name"] = tempMappingName;
                        //}

                        // Data Items.
                        var dataItems = new List<JsonObject>();
                        foreach (var dataItem in dataObjectJsonObject["dataItems"].AsArray())
                        {
                            var dataItemJsonObject = JsonNode.Parse(dataItem.ToJsonString()).AsObject();

                            // Type must be first.
                            dataItemJsonObject.Add("dataItemType", "dataItem");

                            // Ensure each item has a GUID
                            AddGuid(dataItemJsonObject, dataItemGuids, "name");

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
                        UpdateDataConnection(dataObjectJsonObject, connectionGuids);

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

                    // Ensure each object has a GUID
                    AddGuid(dataObjectJsonObject, dataObjectGuids, "name");

                    // Replace properties with newer names (upgrade).
                    ReplaceDataObjectProperties(dataObjectJsonObject);

                    // Connection.
                    UpdateDataConnection(dataObjectJsonObject, connectionGuids);

                    // Update the classifications
                    UpdateClassifications(dataObjectJsonObject, classificationGuids);

                    jsonObjectDataObjectMapping["targetDataObject"] = dataObjectJsonObject;

                    // Data items.
                    var targetDataObjectDataItemList = new List<JsonObject>();
                    foreach (var targetDataObjectDataItem in dataObjectMapping["targetDataObject"]!["dataItems"].AsArray())
                    {
                        var jsonObjectTargetDataObjectDataItem = JsonNode.Parse(targetDataObjectDataItem.ToJsonString()).AsObject();

                        // Type must be first.
                        jsonObjectTargetDataObjectDataItem.Add("dataItemType", "dataItem");

                        // Ensure each item has a GUID
                        AddGuid(jsonObjectTargetDataObjectDataItem, dataItemGuids, "name");

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

                            // Ensure each object has a GUID
                            AddGuid(dataObjectJsonObject, dataObjectGuids, "name");

                            // Replace properties with newer names (upgrade).
                            ReplaceDataObjectProperties(dataObjectJsonObject);

                            // Connection.
                            UpdateDataConnection(dataObjectJsonObject, connectionGuids);

                            // Update the classifications
                            UpdateClassifications(dataObjectJsonObject, classificationGuids);

                            // Data Items.
                            var dataItems = new List<JsonObject>();
                            foreach (var dataItem in dataObjectJsonObject["dataItems"].AsArray())
                            {
                                var dataItemJsonObject = JsonNode.Parse(dataItem.ToJsonString()).AsObject();

                                // Type must be first.
                                dataItemJsonObject.Add("dataItemType", "dataItem");

                                // Ensure each item has a GUID
                                AddGuid(dataItemJsonObject, dataItemGuids, "name");

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

                                // Ensure each item has a GUID
                                AddGuid(jsonObjectSourceDataItem, dataItemGuids, "name");

                                sourceDataItemList.Add(jsonObjectSourceDataItem);
                            }

                            // Target data item
                            var jsonNodeTargetDataItem = jsonObjectDataItemMapping["targetDataItem"];
                            var jsonObjectTargetDataItem = JsonNode.Parse(jsonNodeTargetDataItem.ToJsonString()).AsObject();
                            AddGuid(jsonObjectTargetDataItem, dataItemGuids, "name");
                            jsonObjectDataItemMapping["targetDataItem"] = jsonObjectTargetDataItem;
                            
                            foreach (var sourceDataItemJsonObject in sourceDataItemList)
                            {
                                jsonObjectDataItemMapping["sourceDataItems"]![sourceDataItemList.IndexOf(sourceDataItemJsonObject)] = sourceDataItemJsonObject;
                            }

                            // Ensure each object has a GUID
                            AddGuid(jsonObjectDataItemMapping, dataItemMappingGuids);

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

                                // Source data items.
                                var sourceDataItemList = new List<JsonObject>();
                                foreach (var dataItem in dataItemMapping!["sourceDataItems"]?.AsArray())
                                {
                                    // Source data item.
                                    var dataItemJsonObject = JsonNode.Parse(dataItem.ToJsonString()).AsObject();

                                    dataItemJsonObject.Add("dataItemType", "dataItem");

                                    // Re-add unchanged properties to manage order.
                                    AddUnchangedDataItemProperties(dataItemJsonObject);

                                    // Replace properties with newer names (upgrade).
                                    ReplaceDataItemProperties(dataItemJsonObject);

                                    // Ensure each item has a GUID
                                    AddGuid(dataItemJsonObject, dataItemGuids, "name");

                                    // Change isHardCodedValue into extension.
                                    if (dataItemJsonObject.ContainsKey("isHardCodedValue"))
                                    {
                                        var keyExists = dataItemJsonObject.TryGetPropertyValue("isHardCodedValue", out var jsonNode).ToString();

                                        if (keyExists == "True")
                                        {
                                            if (jsonNode != null && jsonNode.ToString() == "true")
                                            {
                                                try
                                                {
                                                    // Create an extension.
                                                    var extension = new JsonObject()
                                                    {
                                                        ["key"] = "isHardCodedValue",
                                                        ["value"] = "true",
                                                        ["notes"] = "Hard-coded value"
                                                    };

                                                    // Extensions
                                                    var extensionArray = new JsonArray();
                                                    if (dataItemJsonObject["extensions"] == null)
                                                    {
                                                        dataItemJsonObject.Add("extensions", extensionArray);
                                                    }

                                                    extensionArray = dataItemJsonObject["extensions"]?.AsArray();
                                                    extensionArray.Add(extension);
                                                    dataItemJsonObject["extensions"] = extensionArray;
                                                }
                                                catch (Exception ex)
                                                {
                                                    //
                                                }
                                            }
                                        }

                                        dataItemJsonObject.Remove("isHardCodedValue");
                                    }

                                    sourceDataItemList.Add(dataItemJsonObject);

                                    foreach (var sourceDataItemJsonObject in sourceDataItemList)
                                    {
                                        dataMappingJsonObject["sourceDataItems"]![sourceDataItemList.IndexOf(sourceDataItemJsonObject)] = sourceDataItemJsonObject;
                                    }

                                    // Target data item
                                    var jsonNodeTargetDataItem = dataMappingJsonObject["targetDataItem"];
                                    var jsonObjectTargetDataItem = JsonNode.Parse(jsonNodeTargetDataItem.ToJsonString()).AsObject();
                                    AddGuid(jsonObjectTargetDataItem, dataItemGuids, "name");
                                    dataMappingJsonObject["targetDataItem"] = jsonObjectTargetDataItem;

                                    dataItemMappingList.Add(dataMappingJsonObject);
                                }
                            }

                            // Ensure each item has a GUID
                            AddGuid(businessKeyComponentMappingJsonObject, businessKeyDefinitionGuids);

                            // Ensdure that each business key definition has a name.
                            var businessKeyDefinitionName = $"{jsonObjectDataObjectMapping["name"]} for {businessKeyComponentMappingJsonObject["surrogateKey"]}";

                            businessKeyComponentMappingJsonObject.Add("name", businessKeyDefinitionName);

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

                #region Extension

                try
                {
                    var extensionArray = new JsonArray();

                    if (jsonObjectDataObjectMapping["extensions"] != null)
                    {
                        // Use the existing values.
                        extensionArray = jsonObjectDataObjectMapping["extensions"]?.AsArray();
                    }
                    else
                    {
                        // Create a new property.
                        jsonObjectDataObjectMapping.Add("extensions", extensionArray);
                    }

                    #region Control Framework
                    // Create a control framework extension.
                    var extensionControlFramework = new JsonObject()
                    {
                        ["key"] = "hasControlFramework",
                        ["value"] = "true",
                        ["notes"] = "Integration with Control Framework"
                    };
                    extensionArray.Add(extensionControlFramework);

                    var extensionControlFrameworkDataStore = new JsonObject()
                    {
                        ["key"] = "controlFrameworkDataStore",
                        ["value"] = "900_Direct_Framework",
                        ["notes"] = "Control Framework data store"
                    };
                    extensionArray.Add(extensionControlFrameworkDataStore);

                    var extensionControlFrameworkLocation = new JsonObject()
                    {
                        ["key"] = "controlFrameworkLocation",
                        ["value"] = "omd",
                        ["notes"] = "Control Framework location"
                    };
                    extensionArray.Add(extensionControlFrameworkLocation);
                    #endregion

                    #region Testing Framework
                    // Create a testing framework extension.
                    var extensionTestingFramework = new JsonObject()
                    {
                        ["key"] = "hasTestingFramework",
                        ["value"] = "true",
                        ["notes"] = "Integration with Testing Framework"
                    };
                    extensionArray.Add(extensionTestingFramework);

                    var extensionTestingFrameworkDataStore = new JsonObject()
                    {
                        ["key"] = "controlTestingDataStore",
                        ["value"] = "testing-framework",
                        ["notes"] = "Testing Framework data store"
                    };
                    extensionArray.Add(extensionTestingFrameworkDataStore);

                    var extensionTestingFrameworkLocation = new JsonObject()
                    {
                        ["key"] = "controlTestingLocation",
                        ["value"] = "ut",
                        ["notes"] = "Testing Framework location"
                    };
                    extensionArray.Add(extensionTestingFrameworkLocation);
                    #endregion

                    jsonObjectDataObjectMapping["extensions"] = extensionArray;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                #endregion

                // Ensure each object has a GUID
                AddGuid(jsonObjectDataObjectMapping, dataObjectMappingGuids, "name");

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
                // To do.
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

void AddGuid(JsonObject jsonObject, Dictionary<string, Guid> objectGuids, string propertyName = null)
{
    var property = "";

    if (propertyName == null)
    {
        property = jsonObject.ToString();
    }
    else
    {
        property = jsonObject[propertyName].ToString();
    }

    if (!objectGuids.ContainsKey(property))
    {
        objectGuids.Add(property, Guid.NewGuid());
    }

    var propertyGuid = objectGuids.TryGetValue(property, out Guid guid);

    jsonObject["id"] = guid;
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
    //ReAddProperty("id", jsonObject);
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
    //ReAddProperty("id", jsonObject);
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

void UpdateClassifications(JsonObject jsonObject, Dictionary<string, Guid> objectGuids)
{
    if (jsonObject["classifications"] != null)
    {
        var classificationArray = new JsonArray();
        try
        {
            foreach (var classification in jsonObject["classifications"].AsArray())
            {
                var classificationJsonObject = JsonNode.Parse(classification.ToJsonString()).AsObject();
                var getClassification = classificationJsonObject.TryGetPropertyValue("classification", out var classificationValue).ToString();

                string groupValue = classificationValue.ToString() switch
                {
                    "Source" => "Solution Layer",
                    "Core Business Concept" => "Logical",
                    "CoreBusinessConcept" => "Logical",
                    "Integration" => "Solution Layer",
                    "Context" => "Logical",
                    "Persistent Staging" => "Solution Layer",
                    "PersistentStaging" => "Solution Layer",
                    "PersistentStagingArea" => "Solution Layer",
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

                classificationJsonObject.Add("group", groupValue);

                // Ensure each object has a GUID
                AddGuid(classificationJsonObject, objectGuids, "classification");

                classificationArray.Add(classificationJsonObject);
            }
        }
        catch (Exception ex)
        {
            // To do
        }

        jsonObject["classifications"] = classificationArray;
    }
}

void UpdateDataConnection(JsonObject dataObjectJsonObject, Dictionary<string, Guid> objectGuids)
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

        // Ensure each object has a GUID
        AddGuid(dataConnectionObject, objectGuids, "name");

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