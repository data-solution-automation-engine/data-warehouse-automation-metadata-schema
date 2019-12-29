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

The schema is available in the Github under 

The schema is as follows:

```json
{
  "$schema": "http://json-schema.org/draft-07/schema#",
  "$id": "",
  "title": "interfaceDataWarehouseAutomationMetadata",
  "description": "Standardardised format for the required metadata to generate ETL and DDL structures available for a Data Warehouse solution. The intent is to separate / decouple how individual software stores Data Warehouse Automation metadata and how this metadata can be exposed to other components, technologies and solutions for ETL and database structure generation.",
  //
  // Definitions: the overview of reusable objects.
  //
  "definitions": 
  {
  	"dataObject": // The generic table of file definition.
    {
      "$id": "#/definitions/dataObject",
      "description": "Any kind of entity, i.e. data set, query, object, file or table.",
      "type": "object",
      "properties": 
      {
      	"dataObjectId": {
          "description": "Optional unique identifier for a file/table.",
          "type": "integer"
        },
        "dataObjectName": {
          "description": "Mandatory unique name of the file/table.",
          "type": "string"          
        },
        "dataObjectType": {
          "description": "Optional classification of the object (query, file, table).",
          "type": "string"          
        },          
        "dataItems": { // Optional - collection of dataItems for a table or file.
          "description": "Elements that define the dataset.",
          "type": "array","items": {
            "$ref": "#/definitions/dataItem"
          }
        }
      },
      // Mandatory elements for a data object, only the name is really required.
  	  "required" : ["name"] 
    }, 
    // End of dataObject segment.
    //
    // Start of dataItem segment.
    "dataItem": { // The generic definition of a column.
      "description": "A column, attribute or item in a dataObject.",     
      "type": "object",
      "properties": {
        "name": {
          "description": "Unique name which identifies the column / attribute.",
          "type": "string"
        },
        "dataType": {
          "description": "Optional. Label of the data type",
          "type": "string"
        },
        "characterLength": {
          "description": "Optional. Length of the item in case it is text.",
          "type": "integer"
        },
        "numericPrecision": {
          "description": "The number of digits in a numeric value (number).",
          "type": "integer"
        },
        "numericScale": {
          "description": "The number of digits to the right of the decimal point.",
          "type": "integer"
        },
        "ordinalPosition": {
          "description": "Optional. The position of items in the data object.",
          "type": "integer"
        },
        "isPrimaryKey": {
          "description": "Boolean value indicating if the item is a Primary Key.",
          "type": "boolean"
        }
      },
      // Mandatory elements for a data item, only the name is really required.
  	  "required" : ["name"] 
    },
    // End of dataItem.
    //
    // Start of key definition.
    "businessKeyDefinition": 
    { // The generic definition of business key.
      "description": "The generic definition of business key.",
      "required": ["businessKeyComponents"],
      "type": "object",
      "properties": {
        "businessKeyComponentBehaviour": { // Supporting composition, concatentation.
          "description": "Specification of what to do with the Business Key.",
          "type": "string"
        },
        "businessKeyComponents": { // The collection of columns for a Business Key.
          "description": "Items that define the Business Key.",
          "type": "array","items": {
            "$ref": "#/definitions/dataItem"
          }
        },
      },
    }
    // End of key definition
  }, 
  // 
  // End of reusable objects
  //
  // Main object definition
  //
  "dataObjectMapping" :
  {
    "description" : "List of source-to-target mappings",
    "type" : "array",
    "items" : 
    {
      "dataObjectMappingName": {
          "description": "Unique name which identifies the mapping.",
          "type": "string"
      },
      "dataObjectMappingClassification": {
        "description": "Classification for the mapping (free form).",
        "type": "array",
        "items": {
          "type": "string"
        },
        "minItems": 0,
        "uniqueItems": true
      },        
      "sourceDataObject": {
        "description": "The source object of the mapping.",
        "$ref": "#/definitions/dataObject"
      },  
      "targetDataObject": {
        "description": "The target object of the mapping.",
        "$ref": "#/definitions/dataObject"
      },  
      "enabled": {
        "description": "The source object of the mapping.",
        "type" : "boolean"
      },
      "dataItemMapping": {
        "description": "The collection of column-to-column mappings",
        "type": "array",
        "items": {
          "description": "Individual item mappings",
          "type": "object",
          "oneOf": [
            {
              "properties": {
                "sourceDataItem": {
                  "$ref": "#/definitions/dataItem"
                },
                "targetDataItem": {
                  "$ref": "#/definitions/dataItem"
                }
              },
              "required": ["sourceDataItem","targetDataItem"]
            }
          ]
        },
        "minItems": 1,
        "uniqueItems": true
      }      
    },
    "required": ["sourceDataObject","targetDataObject", "dataItemMapping"]   
  }
} 
```



## **Example** code

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

