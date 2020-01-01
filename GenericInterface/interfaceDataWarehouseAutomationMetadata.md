# Interface specification - Data Warehouse Automation Metadata

This is a work-in-progress section to define the target format. Just started / in development, focusing on the schema definition first and back-filling documentation after.

The schema is and examples are validated / extended using <https://www.jsonschemavalidator.net/>. 

Standards are followed from http://json-schema.org/.  Also see http://json-schema.org/learn/miscellaneous-examples.html.

This schema is implemented in the TEAM / Virtual Data Warehouse tool combination as interface format. Also various samples are provided in this Github repository.

## Requirements:

- Needs to be able to generate transformation logic using and externally applied template based on metadata available in the Json.
- Needs to encompass all known Ensemble Modelling use-cases (see checklist for Data Vault conformance) in a single object.
- Make sure business keys are created in collections, to support concatenation and composition.
- Allow for flexibility in source usage, including code to support complex definitions.

## **Schema**

The proposed Json schema has standard components for table and column structures that are reused for sources and targets. At the mapping level only the classification, filter and load direction are added, the rest is generic reuse of definitions.

The schema is available in the Github under  https://github.com/RoelantVos/Data_Warehouse_Automation_Metadata_Interface, specifically here: [https://github.com/RoelantVos/Data_Warehouse_Automation_Metadata_Interface/blob/master/Generic%20interface/interfaceDataWarehouseAutomationMetadata.json](https://github.com/RoelantVos/Data_Warehouse_Automation_Metadata_Interface/blob/master/Generic interface/interfaceDataWarehouseAutomationMetadata.json). 

It is also referenced in the Class Library.

## **Example** file

This is a simple example using the schema definition. Various other examples and use-cases are available in the code sections of this Github.

```json
{
  "dataObjectMapping": 
    {
      "sourceDataObject": {
        "name": "STG_PROFILER_OFFER"
      },
      "targetDataObject": {
        "name": "PSA_PROFILER_OFFER"
      },        
      "dataItemMapping": [
        {
          "sourceDataItem": { "name": "OfferId" },
          "targetDataItem": { "name": "OFFER_ID" }
        },
        {
          "sourceDataItem": { "name": "Offer_Long_Description" },
          "targetDataItem": { "name": "OFFER_DESCRIPTION" }
        }
      ]
    }
 }
```

