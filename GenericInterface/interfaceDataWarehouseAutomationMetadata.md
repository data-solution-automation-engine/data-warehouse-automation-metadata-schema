# Interface specification - Data Warehouse Automation Metadata

The generic interface for Data Warehouse (DWH) automation metadata is intended to provide an agreed format (canonical) for the exchange of relevant DWH automation metadata. This in itself aims to facilitate greater interoperability between various DWH automation and ETL generations approaches and ecosystems.

The schema is and examples are validated / extended using <https://www.jsonschemavalidator.net/>. Standards are followed from http://json-schema.org/.  Also see http://json-schema.org/learn/miscellaneous-examples.html.

This schema is implemented in the TEAM / Virtual Data Warehouse tool combination as interface format. Also various samples are provided in this Github repository.

## Requirements

The interface schema has the following requirements:

- Needs to be able to generate transformation logic using and externally applied template based on metadata available in the Json.
- Needs to encompass all known Ensemble Modelling use-cases (see checklist for Data Vault conformance) in a single object.
- Make sure business keys are created in collections, to support concatenation and composition.
- Allow for flexibility in source usage, including code to support complex definitions.

## **Schema**

The proposed Json schema has standard components for table (DataObjects) and column () structures that are reused for sources and targets. At the mapping level only the classification, filter and load direction are added, the rest is generic reuse of definitions.

The schema is available in the Github under:  https://github.com/RoelantVos/Data_Warehouse_Automation_Metadata_Interface.

The schema definition specifically is located here: [https://github.com/RoelantVos/Data_Warehouse_Automation_Metadata_Interface/blob/master/Generic%20interface/interfaceDataWarehouseAutomationMetadata.json](https://github.com/RoelantVos/Data_Warehouse_Automation_Metadata_Interface/blob/master/Generic interface/interfaceDataWarehouseAutomationMetadata.json). 

It is also referenced in the Class Library.

## High level structure

### DataObjectMappingList

The schema's top-level object is a 'DataObjectMappingList', which is an array of individual source-to-target mappings called 'DataObjectMappings', commonly referred to as 'mappings'. In code, this means a DataObjectMappingList is defined as a List<DataObjectMapping>. 

A DataObjectMapping is a unique ETL mapping / transformation that moves, or interprets, data from a given source to a given destination. 

The decision to start the format with an array / list that contains potentially multiple DataObjectMappings relates to the Data Warehouse virtualisation use-case. In these implementations, multiple individual mappings together create a single view object. 

As an example, consider the loading of a Core Business Concept / Hub type entity from various different sources. If you would use these different mappings to generate ETL processes you would create one physical ETL object for each mapping. However, if you are seeking to generate a view that represents the target table the result you would use the collection (list) of mappings to generate separate statements that are unioned in the single view object.

### DataObjectMapping

The DataObjectMapping is the element that defines an individual source-to-target mapping / ETL process. It is a mapping between a source and target object - referred to as DataObjects. The DataObject is in fact a reusable definition in the Json schema. 

This definition is used twice in the DataObjectMapping: as the *SourceDataObject* and as the *TargetDataObject* - both instances of the DataObject class / type.

The other key component of a DataObjectMapping is the *DataItemMapping*, which describes the column-to-column (or transformation-to-column) .

The SourceDataObject, TargetDataObject and DataItemMapping are the mandatory components of a DataObjectMapping. There are many other attributes that can be set, and there are mandatory items within the DataObjects and DataItems. These are all described in the Json schema.

## Example file

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

