# Frequently Asked Questions

## Why have a schema definition for Data Warehouse Automation?

The reason work on the schema definition started is based on a desire to collaborate with various like-minded professionals in the industry, but who all had different (proprietary) ways to record metadata. The challenge was how to collaborate on at least the patterns, without necessarily agreeing on ‘the best’ back-end solution to store metadata.

The interface for Data Warehouse Automation was started as an initiative that would support each to use the back-end of their choosing, while still be able to cooperate on the design and improvement of Data Warehouse implementation patterns and ETL generation concepts.

## Why is the top-level object a list?

The convention is that, even though only a single Data Object Mapping may be needed for a transformation or copy, a Data Object Mapping is always part of a Data Object Mapping List. This means that at the top level, one or more Data Object Mappings are always grouped into a single Data Object Mapping List.

In other words, the Data Object Mapping List is an array of individual Data Object Mappings. In code, this means a Data Object Mapping List is defined as a List (‘dataObjectMappings’).

The decision to start the format with an array / list that contains potentially multiple Data Object Mappings relates to the Data Warehouse virtualisation use-case. In this style of implementation, multiple individual mappings together create a single view object. Testing has shown that it is much harder to piece the relationships between mappings together at a later stage in order to create a single (view) object, and having the option to define a collection makes this really easy.

For example, consider the loading of a Core Business Concept (‘Hub’) type entity from various different data sources. If you would use these different mappings to generate ETL processes you would create one physical ETL object for each mapping. However, if you are seeking to generate a view that represents the target table you would use the collection (list) of mappings to generate separate statements that are 'unioned' in a single view object.

Example: [https://github.com/RoelantVos/Data-Warehouse-Automation-Metadata-Schema/blob/master/ClassLibrary/DataWarehouseAutomation/Sample_Metadata/sampleBasic.json](https://github.com/RoelantVos/Data-Warehouse-Automation-Metadata-Schema/blob/master/ClassLibrary/DataWarehouseAutomation/Sample_Metadata/sampleBasic.json).

Or below, an even more simplified example of a single Data Object Mapping (with only one source, one target and a single Data Item Mapping) being part of the list dataObjectMappings.

```json
{
  "dataObjectMappings": [
    {
      "mappingName": "MappingOne",
      "sourceDataObjects": [
        {
          "name": "TableOneSource"
        }
      ],
      "targetDataObject": {
        "name": "TableOneTarget"
      },
      "dataItemMappings": [
        {
          "sourceDataItems": [
            {
              "name": "ColumnOneSource"
            }
          ],
          "targetDataItem": {
            "name": "ColumnOneTarget"
          }
        }
      ]
    }
  ]
}
```

## Why are Data Objects and Data Items defined as arrays when they are used as sources?

This is implemented to facilitate a greater degree of flexibility. Since the application of the metadata is managed in the templates / patterns it can be useful to be able to map multiple sources to a single target. This way you can just select the first one for most easier use-cases, and use the collection for more complex scenarios.

The downside is that either a loop or array index needs to be used to pinpoint the source in question, but only needs to be implemented once.

Example: using Handlebars will give you the name of the first source object:

```handlebars
{{sourceDataObjects.0.name}}
```

## How can I use this format to work with the Virtual Data Warehouse (VDW) tool?

The Virtual Data Warehouse tool has been developed to leverage the Data Warehouse Automation schema. In fact, the front-end is generated based on the information made available in this format. All that is needed is to point the ‘input’ directory to a directory / folder that contains JSON files that conform to the schema definition.

Once this is in place, the code can be generated using the available metadata.

## How can I use this format to work with my own code generator?

The GitHub repository for the generic schema for Data Warehouse Automation contains various projects such as a class library, an example project and a regression testing project.

The easiest way to get started is to either copy and modify the example project, or add the DataWarehouseAutomation.dll library to your solution. This library contains the various classes required to deserialize (‘load’) JSON files conforming to the format of the Data Warehouse Automation schema into memory for further use.

A simple C# example to generate some quick ETL (taken from the example project):

```cs
// Load a template (pattern) from file
stringTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\\..\\TemplateSampleBasic.handlebars");

// Compile the template
var template = Handlebars.Compile(stringTemplate);

// Load a metadata Json file into memory as a string
jsonInput = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"..\\..\\sampleBasic.json");

// Map the Json to the Data Warehouse Automation classes
deserialisedMapping = JsonConvert.DeserializeObject<DataObjectMappings>(jsonInput);

// Generate the code, by merging the metadata with the pattern
result = template(deserialisedMapping);

// Display the results to the user
Console.WriteLine(result);
```
