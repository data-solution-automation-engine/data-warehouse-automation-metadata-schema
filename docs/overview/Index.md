# Data Solution Automation Metadata

Welcome to the [documentation of the schema for data solution automation](https://data-solution-automation-engine.github.io/data-warehouse-automation-metadata-schema/).

The **interface for data solution automation metadata** provides an agreed (canonical) format for the exchange of relevant metadata for data solution/warehouse automation. The intent is to define a *sufficiently generic* format, that can be used to record and share information about data solution automation metadata, so that more time can be spent on concepts, patterns, and solution ideas - instead of reinventing the wheel on what exactly is required to automate a data solution.

This in itself aims to facilitate greater interoperability between various data solution / data warehouse automation and data logistics generations approaches and ecosystems.

The schema definition can be directly viewed [here](https://github.com/RoelantVos/Data_Warehouse_Automation_Metadata_Interface/blob/master/GenericInterface/interfaceDataWarehouseAutomationMetadata.json), and is part of [this GitHub repository](https://github.com/RoelantVos/Data_Warehouse_Automation_Metadata_Interface). The repository contains various supporting components such as:

* A simple Class Library (DLL) that has implemented the schema structure, as well as a validation function to test JSON files / messages against the schema
* Starter documentation.
* A sample implementation that generates code using [Handlebars.Net](https://roelantvos.com/blog/using-handlebars-to-generate-data-vault-hub-load-processes/). The example that uses the Handlebars generates code using a sample JSON file that conforms to the interface schema.
* A simple regression test application that demonstrates different usages of the schema.

The schema is and examples are validated / extended using [https://www.jsonschemavalidator.net/](https://www.jsonschemavalidator.net/). Standards are followed from [json-schema.org](https://json-schema.org/).  Also see [some miscellaneous examples](https://json-schema.org/learn/miscellaneous-examples.html).

In principle, the schema can be used to generate an entire Data Warehouse, Data Lake and equivalent and/or similar.

## **Schema**

The JSON schema for data solution automation has standard components for table (DataObjects) and column (DataItem) structures that are reused for sources and targets. At the mapping level only the classification, filter and load direction are added, the rest is generic reuse of definitions.

The schema is available in the GitHub under:  [https://github.com/RoelantVos/Data_Warehouse_Automation_Metadata_Interface](https://github.com/RoelantVos/Data_Warehouse_Automation_Metadata_Interface).

The schema definition itself is located here: [https://github.com/data-solution-automation-engine/data-warehouse-automation-metadata-schema/blob/main/GenericInterface/interfaceDataWarehouseAutomationMetadataV2_0.json](https://github.com/data-solution-automation-engine/data-warehouse-automation-metadata-schema/blob/main/GenericInterface/interfaceDataWarehouseAutomationMetadataV2_0.json).

It is also referenced in the Class Library.

## How does the interface schema work?

The interface is a JSON Schema Definition that has been designed following draft 7 of the JSON schema. It contains a series of reusable defined objects (‘definitions’) that are implemented as a source-to-target mapping object called a ‘Data Object Mapping’.

The Data Object Mapping is literally a mapping between Data Objects. It is a unique ETL mapping / transformation that moves, or interprets, data from a given source to a given destination.

At a high level there are two elements that form the core of a Data Object Mapping, these are the:

* Data Object, which defines the source and target of the Data Object Mapping. A Data Object can optionally have a connection defined as a string or token, and can be a query, file or table.
* Data Item, which belong to a Data Object and represents an individual column or calculation (query) in a Data Object Mapping.

![img](https://roelantvos.com/blog/wp-content/uploads/2020/01/DataObject-3-1024x466.png)

## Mapping metadata

A Data Object Mapping reuses the definitions of the Data Object and Data Item. The Data Object is used twice: as the *SourceDataObject* and as the *TargetDataObject* – both instances of the DataObject class / type.

The other key component of a Data Object Mapping is the *Data Item Mapping*, which describes the column-to-column (or transformation-to-column) and reuses the Data Item class.

The Source Data Object, Target Data Object and Data Item Mapping are the mandatory components of a Data Object Mapping.

There are many other attributes that can be set, and there are mandatory items within the Data Objects and Data Items also. These are described in the JSON schema, and the concept is that the validation functions will make it easy to try out different uses of the schema.

One of the goals of defining this schema has been to find a good balance between being too generic and too specific (restrictive). For this reason there are only a few mandatory elements.

It is possible to add a specific class to a Data Object Mapping: the Business Key Definition. This construct again reuses the earlier definitions but can optionally be added to the Data Object Mapping as an special classified set of transformation.

By combining this, the Data Object Mapping looks as follows at a high level:

![img](https://roelantvos.com/blog/wp-content/uploads/2020/01/DataObjectMapping-1024x453.png)

## Mapping collections

At the top level, one or more Data Object Mappings are grouped into a single Data Object Mapping List. The convention is that, even though only a single Data Object Mapping may be needed in a message or file, a Data Object Mapping is *always* part of a Data Object Mapping List.

In other words, the Data Object Mapping List is an array of individual Data Object Mappings. In code, this means a Data Object Mapping List is defined as a `List<DataObjectMapping>`.

The decision to start the format with an array / list that contains potentially multiple Data Object Mappings relates to the Data Warehouse virtualisation use-case. In this style of implementation, multiple individual mappings together create a single view object. Testing revealed it is much harder to piece the relationships between mappings together at a later stage to create a single (view) object, and having the option to define a collection makes this really easy.

For example, consider the loading of a Core Business Concept (‘Hub’) type entity from various different data sources. If you would use these different mappings to generate ETL processes you would create one physical ETL object for each mapping. However, if you are seeking to generate a view that represents the target table you would use the collection (list) of mappings to generate separate statements that are unioned in a single view object.

## Example

This is a simple example using the schema definition. Various other examples and use-cases are available in the code sections of this Github. The example shows a single DataObjectMapping in a DataObjectMappingList.

```json
{
  "dataObjectMappingList": [
    {
      "mappingName": "Mapping1",
      "sourceDataObject": {
        "name": "SourceTable"
      },
      "targetDataObject": {
        "name": "TargetTable"
      },
      "dataItemMapping": [
        {
          "sourceDataItem": {
            "name": "SourceColumn1"
          },
          "targetDataItem": {
            "name": "TargetColumn1"
          }
        },
        {
          "sourceDataItem": {
            "name": "SourceColumn2"
          },
          "targetDataItem": {
            "name": "TargetColumn2"
          }
        }
      ]
    }
  ]
}
```
