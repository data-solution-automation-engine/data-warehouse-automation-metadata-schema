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

The schema is available in the Github under  https://github.com/RoelantVos/Data_Warehouse_Automation_Metadata_Interface.

## **Example** file

This is a simple example using the schema definition. Various other examples and use-cases are available in the code sections of this Github.

```json
{
  "dataItemMappingList" :
  [
    {
      // Source
      "sourceDataObject": {
        "mappingObjectId": 2,
        "mappingObjectName": "STG_PROFILER_OFFER",
        "mappingObjectColumns": [
          {
            "columnName": "OfferID",
            "columnDataType": "int",
            "columnCharacterMaximumLength": 4,
            "columnNumericPrecision": 10,
            "columnNumericScale": 0,
            "columnOrdinalPosition": 8,
            "isPrimaryKey": true
          },
          {
            "columnName": "Offer_Long_Description",
            "columnDataType": "nvarchar",
            "columnCharacterMaximumLength": 100,
            "columnNumericPrecision": 0,
            "columnNumericScale": 0,
            "columnOrdinalPosition": 9
          }
        ],
          "mappingConnectionDetails": {
          "mappingObjectSchema": "dbo",
          "mappingObjectDatabase": "STAGING",
          "mappingObjectEnvironment": "BISERVER"
        },
        "businessKey" : {
          "businessKeyComponentBehaviour" : "None",
          "businessKeyComponents" : [      
            {
              "columnName": "OfferID"
            }
          ]
        }     
      // Target    
      },
      "targetObject": {
        "mappingObjectId": 2,
        "mappingObjectName": "HUB_INCENTIVE_OFFER",
        "businessKey" : {
          "businessKeyComponents" : [      
            {
              "columnName": "OfferID"
            }
          ]
        } ,       
        "mappingObjectColumns": [
          {
            "columnName": "OFFER_ID",
            "columnDataType": "nvarchar",
            "columnCharacterMaximumLength": 100,
            "columnOrdinalPosition": 1
          },
          {
            "columnName": "OFFER_DESCRIPTION",
            "columnDataType": "nvarchar",
            "columnCharacterMaximumLength": 100,
            "columnOrdinalPosition": 2
          }
        ],
        "mappingConnectionDetails": {
          "mappingObjectSchema": "dbo",
          "mappingObjectDatabase": "INTEGRATION",
          "mappingObjectEnvironment": "BISERVER"
        },      
        "etlProcessControlColumns": [
          {
            "columnName": "ETL_PROCESS_CONTROL_ID",
            "columnDataType": "int"
          }
        ]
      },
      "filterCriterion": "3=3",
      "classification": ["Hub"],
      "loadVector": ["Source-DataVault"],
      // Mappings
      "columnMappings": [
        {
          "sourceColumn": {"columnName": "OfferId"},
          "targetColumn": {"columnName": "OFFER_ID"
          }
        },
        {
          "sourceColumn": {"columnName": "Offer_Long_Description"},
          "targetColumn": {"columnName": "OFFER_DESCRIPTION"}
        }
      ]
    }  
  ]
}
```

