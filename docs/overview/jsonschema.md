# JSON Schema Definition

```json
{
  "$schema": "http://json-schema.org/draft-07/schema#",
  "$id": "https://github.com/RoelantVos/Data_Warehouse_Automation_Metadata_Interface/GenericInterface/interfaceDataWarehouseAutomationMetadataV2_0.json",
  "title": "Schema for Data Solution code generation.",
  "description": "Standardized format for the required metadata to generate data logistics (ETL) and object creation (DDL) for Data Solutions such as a Data Warehouse or Data Lake. The intent is to separate / decouple how individual software stores Data Warehouse Automation metadata and how this metadata can be exposed to other components, technologies and solutions for data processing and data structure generation.",
  "type": "object",
  "required": [
    "dataObjectMappings"
  ],
  "properties": {
    "dataObjectMappings": {
      "$ref": "#/definitions/dataObjectMappings"
    },
    "name": {
      "$id": "#/definitions/dataObjectMappings/properties/name",
      "title": "Name",
      "description": "Optional name of the data object mapping list.",
      "type": "string"
    },
    "classifications": {
      "$id": "#/definitions/dataObjectMappings/properties/classifications",
      "title": "Classifications",
      "description": "Classification for the data object mapping list.",
      "type": [ "array", "null" ],
      "items": {
        "$ref": "#/definitions/dataClassification"
      },
      "minItems": 0,
      "uniqueItems": true
    },
    "extensions": {
      "$id": "#/definitions/dataObjectMappings/properties/extensions",
      "description": "Key/Value pair extension object.",
      "type": [ "array", "null" ],
      "items": {
        "$ref": "#/definitions/extension"
      },
      "minItems": 0,
      "uniqueItems": true
    }
  },
  "definitions": {
    "dataObjectMappings": {
      "$id": "#/definitions/dataObjectMappings",
      "title": "Data Object Mappings",
      "description": "The list (array) of source-to-target mappings between Data Objects. This is the top-level object that contains one or more data object mappings.",
      "type": [ "array", "null" ],
      "items": {
        "$ref": "#/definitions/dataObjectMapping"
      }
    },
    "dataObjectMapping": {
      "$id": "#/definitions/dataObjectMapping",
      "title": "Data Object Mapping.",
      "description": "An individual mapping between a source data object and a target data object.",
      "type": "object",
      "required": [
        "name",
        "sourceDataObjects",
        "targetDataObject"
      ],
      "properties": {
        "id": {
          "$id": "#/definitions/dataObjectMapping/properties/id",
          "title": "Id",
          "description": "An optional unique identifier for a Data Object Mapping.",
          "type": [ "string", "null" ]
        },
        "name": {
          "$id": "#/definitions/dataObjectMapping/properties/name",
          "title": "Name",
          "description": "Unique name which identifies the mapping.",
          "type": [ "string", "null" ]
        },
        "classifications": {
          "$id": "#/definitions/dataObjectMapping/properties/classifications",
          "title": "Classifications",
          "description": "Classification for the Data Object Mapping.",
          "type": [ "array", "null" ],
          "items": {
            "$ref": "#/definitions/dataClassification"
          },
          "minItems": 0,
          "uniqueItems": true
        },
        "sourceDataObjects": {
          "$id": "#/definitions/dataObjectMapping/properties/sourceDataObjects",
          "title": "Source Data Objects",
          "description": "The source object of the mapping. This can either be an object or a query.",
          "oneOf": [
            {
              "type": [ "array", "null" ],
              "items": {
                "$ref": "#/definitions/dataObject"
              }
            },
            {
              "type": [ "array", "null" ],
              "items": {
                "$ref": "#/definitions/dataObjectQuery"
              }
            }
          ]
        },
        "targetDataObject": {
          "$id": "#/definitions/dataObjectMapping/properties/targetDataObject",
          "title": "Target Data Object",
          "description": "The target object of the mapping. This is always a Data Object type.",
          "$ref": "#/definitions/dataObject"
        },
        "relatedDataObjects": {
          "$id": "#/definitions/dataObjectMapping/properties/relatedDataObjects",
          "title": "Related Data Objects",
          "description": "The collection of associated data object for purposes other than source-target relationship. For example for lookups, merge joins, lineage.",
          "type": [ "array", "null" ],
          "items": {
            "$ref": "#/definitions/dataObject"
          }
        },
        "dataItemMappings": {
          "$id": "#/definitions/dataObjectMapping/properties/dataItemMappings",
          "title": "Data Item Mappings",
          "description": "The collection of individual attribute (column or query) mappings.",
          "type": [ "array", "null" ],
          "items": {
            "$ref": "#/definitions/dataItemMapping"
          }
        },
        "businessKeyDefinitions": {
          "$id": "#/definitions/dataObjectMapping/properties/businessKeyDefinitions",
          "title": "Business Keys",
          "description": "The definition of the Business Key(s) for the Data Object Mapping.",
          "type": [ "array", "null" ],
          "items": {
            "$ref": "#/definitions/businessKeyDefinition"
          }
        },
        "filterCriterion": {
          "$id": "#/definitions/dataObjectMapping/properties/filterCriterion",
          "title": "Filter Criterion",
          "description": "Any filtering that needs to be applied to the Data Object Mapping.",
          "type": [ "string", "null" ]
        },
        "enabled": {
          "$id": "#/definitions/dataObjectMapping/properties/enabled",
          "title": "Enabled",
          "description": "An indicator (boolean) which can capture enabling / disabling of (the usage of) an individual source-to-target mapping.",
          "type": [ "boolean", "null" ]
        },
        "extensions": {
          "$id": "#/definitions/dataObjectMapping/properties/extensions",
          "description": "The collection of extension Key/Value pairs.",
          "type": [ "array", "null" ],
          "items": {
            "$ref": "#/definitions/extension"
          },
          "minItems": 0,
          "uniqueItems": true
        },
        "notes": {
          "$id": "#/definitions/dataObjectMapping/properties/notes",
          "title": "Notes",
          "description": "Any additional information to be added to the Data Object Mapping.",
          "type": "string"
        }
      }
    },
    "dataClassification": {
      "$id": "#/definitions/classification/properties/dataClassification",
      "title": "Data Classification",
      "description": "Classification for the source-to-target mapping (free form), used to add various tags and notes if required.",
      "type": "object",
      "required": [ "classification" ],
      "properties": {
        "id": {
          "$id": "#/definitions/classification/properties/id",
          "title": "Id",
          "description": "An optional unique identifier for the classification, for sorting or identification purposes.",
          "type": [ "string", "null" ]
        },
        "classification": {
          "$id": "#/definitions/classification/properties/classification",
          "title": "Classification",
          "description": "A free-form name that adds documentation / classification to an object.",
          "type": "string"
        },
        "notes": {
          "$id": "#/definitions/classification/properties/notes",
          "title": "Notes",
          "description": "Any additional information to be added to the classification.",
          "type": "string"
        }
      }
    },
    "extension": {
      "$id": "#/definitions/extension",
      "title": "Extension",
      "description": "Free-format key/value pair that can be used for additional context.",
      "type": "object",
      "required": [ "key" ],
      "properties": {
        "id": {
          "$id": "#/definitions/extension/properties/id",
          "title": "Id",
          "description": "Optional unique identifier for an extension",
          "type": [ "string", "null" ]
        },
        "key": {
          "$id": "#/definitions/extension/properties/key",
          "title": "Key",
          "description": "The Key in a Key/Value pair.",
          "type": "string"
        },
        "value": {
          "$id": "#/definitions/extension/properties/value",
          "title": "Value",
          "description": "The Value in a Key/Value pair.",
          "type": "string"
        },
        "description": {
          "$id": "#/definitions/extension/properties/notes",
          "title": "Notes",
          "description": "Any additional information.",
          "type": "string"
        }
      }
    },
    "dataObject": {
      "title": "Data Object",
      "description": "The generic table of file definition. Any kind of entity, i.e. data set, query, object, file or table.",
      "type": "object",
      "required": [
        "name"
      ],
      "properties": {
        "id": {
          "$id": "#/definitions/dataObject/properties/id",
          "title": "Id",
          "description": "Optional unique identifier for a file/table.",
          "type": [ "string", "null" ]
        },
        "name": {
          "$id": "#/definitions/dataObject/properties/name",
          "title": "Name",
          "description": "Mandatory unique name of the file/table.",
          "type": "string"
        },
        "dataItems": {
          "$id": "#/definitions/dataObject/properties/dataItems",
          "title": "Data Items",
          "type": [ "array", "null" ],
          "description": "Collection of dataItems for a table or file. Elements that define the dat set.",
          "items": {
            "$ref": "#/definitions/dataItem"
          }
        },
        "dataConnection": {
          "$id": "#/definitions/dataObject/properties/dataConnection",
          "title": "Data Connection",
          "type": [ "object", "null" ],
          "description": "Data Connection",
          "$ref": "#/definitions/dataConnection"
        },
        "classifications": {
          "$id": "#/definitions/dataObject/properties/classifications",
          "title": "Classifications",
          "description": "Classification for the Data Object (free form).",
          "type": [ "array", "null" ],
          "items": {
            "$ref": "#/definitions/dataClassification"
          },
          "minItems": 0,
          "uniqueItems": true
        },
        "extensions": {
          "$id": "#/definitions/dataObject/properties/extensions",
          "description": "Key/Value pair extension object.",
          "type": [ "array", "null" ],
          "items": {
            "$ref": "#/definitions/extension"
          },
          "minItems": 0,
          "uniqueItems": true
        },
        "description": {
          "$id": "#/definitions/extension/properties/notes",
          "title": "Notes",
          "description": "Any additional information.",
          "type": "string"
        }
      }
    },
    "dataItemQuery": {
      "$id": "#/definitions/dataItemQuery",
      "title": "Data Item Query",
      "description": "A concise piece of transformation or logic at Data Item level e.g. a query which can act as a source for a dataItem.",
      "required": [ "queryCode" ],
      "type": "object",
      "properties": {
        "id": {
          "$id": "#/definitions/dataItemQuery/properties/id",
          "title": "Id",
          "description": "Optional unique identifier for a Data Item Query.",
          "type": [ "string", "null" ]
        },
        "name": {
          "$id": "#/definitions/dataItemQuery/properties/name",
          "title": "Name",
          "type": "string",
          "description": "An optional name for the query"
        },
        "queryCode": {
          "$id": "#/definitions/dataItemQuery/properties/queryCode",
          "title": "Query Code",
          "type": "string",
          "description": "The code that constitutes the query."
        },
        "queryLanguage": {
          "$id": "#/definitions/dataItemQuery/properties/queryLanguage",
          "title": "Query Language",
          "type": "string",
          "description": "The language that the code was written in (e.g. SQL)."
        },
        "dataType": {
          "$id": "#/definitions/dataItemQuery/properties/dataType",
          "title": "Data type",
          "description": "Data type of the Data Item.",
          "type": [ "string", "null" ]
        },
        "characterLength": {
          "$id": "#/definitions/dataItemQuery/properties/characterLength",
          "title": "Character Length",
          "description": "Optional. Length of the item in case it is text.",
          "type": [ "integer", "null" ]
        },
        "numericPrecision": {
          "$id": "#/definitions/dataItemQuery/properties/numericPrecision",
          "title": "Numeric Precision",
          "description": "The number of digits in a numeric value (number).",
          "type": [ "integer", "null" ]
        },
        "numericScale": {
          "$id": "#/definitions/dataItemQuery/properties/numericScale",
          "title": "Numeric Scale",
          "description": "The number of digits to the right of the decimal point.",
          "type": [ "integer", "null" ]
        },
        "ordinalPosition": {
          "$id": "#/definitions/dataItemQuery/properties/ordinalPosition",
          "title": "Ordinal Position",
          "description": "Optional. The position in the data object.",
          "type": [ "integer", "null" ]
        },
        "isPrimaryKey": {
          "$id": "#/definitions/dataItemQuery/properties/isPrimaryKey",
          "title": "Is Primary Key",
          "description": "Boolean value indicating if the item is a Primary Key.",
          "type": [ "boolean", "null" ]
        },
        "classifications": {
          "$id": "#/definitions/dataItemQuery/properties/classifications",
          "title": "Classifications",
          "description": "Classification for the Data Item Query.",
          "type": [ "array", "null" ],
          "items": {
            "$ref": "#/definitions/dataClassification"
          },
          "minItems": 0,
          "uniqueItems": true
        },
        "extensions": {
          "$id": "#/definitions/dataItemQuery/properties/extensions",
          "title": "Extensions",
          "description": "Key/Value pair extension object.",
          "type": [ "array", "null" ],
          "items": {
            "$ref": "#/definitions/extension"
          },
          "minItems": 0,
          "uniqueItems": true
        },
        "description": {
          "$id": "#/definitions/dataItemQuery/properties/notes",
          "title": "Notes",
          "description": "Any additional information.",
          "type": "string"
        }
      }
    },
    "dataObjectQuery": {
      "$id": "#/definitions/dataObjectQuery",
      "title": "Data Object Query",
      "description": "A concise piece of transformation or logic at Data Object level e.g. a query which can act as a source in a Data Object Mapping.",
      "required": [ "queryCode" ],
      "type": "object",
      "properties": {
        "id": {
          "$id": "#/definitions/dataObjectQuery/properties/id",
          "title": "Id",
          "description": "Optional unique identifier for a Data Object Query",
          "type": [ "string", "null" ]
        },
        "name": {
          "$id": "#/definitions/dataObjectQuery/properties/name",
          "title": "Name",
          "type": "string",
          "description": "An optional name for the query"
        },
        "queryCode": {
          "$id": "#/definitions/dataObjectQuery/properties/queryCode",
          "title": "Query Code",
          "type": "string",
          "description": "The code that constitutes the query."
        },
        "queryLanguage": {
          "$id": "#/definitions/dataObjectQuery/properties/queryLanguage",
          "title": "Query Language",
          "type": "string",
          "description": "The language that the code was written in (e.g. SQL)."
        },
        "dataConnection": {
          "$id": "#/definitions/dataObjectQuery/properties/dataConnection",
          "title": "Data Connection",
          "type": [ "object", "null" ],
          "description": "Data Connection",
          "$ref": "#/definitions/dataConnection"
        },
        "classifications": {
          "$id": "#/definitions/dataObjectQuery/properties/classifications",
          "title": "Classifications",
          "description": "Classification for the Data Item Query.",
          "type": [ "array", "null" ],
          "items": {
            "$ref": "#/definitions/dataClassification"
          },
          "minItems": 0,
          "uniqueItems": true
        },
        "extensions": {
          "$id": "#/definitions/dataObjectQuery/properties/extensions",
          "title": "Extensions",
          "description": "Key/Value pair extension object.",
          "type": [ "array", "null" ],
          "items": {
            "$ref": "#/definitions/extension"
          },
          "minItems": 0,
          "uniqueItems": true
        },
        "description": {
          "$id": "#/definitions/dataObjectQuery/properties/notes",
          "title": "Notes",
          "description": "Any additional information.",
          "type": "string"
        }
      }
    },
    "dataConnection": {
      "$id": "#/definitions/dataConnection",
      "title": "Data Connection",
      "description": "Connectivity details, that can be used for either a DataObject or DataQuery.",
      "required": [ "name" ],
      "type": [ "object", "null" ],
      "properties": {
        "id": {
          "$id": "#/definitions/dataConnection/properties/id",
          "title": "Id",
          "description": "An optional identifier.",
          "type": [ "string", "null" ]
        },
        "name": {
          "$id": "#/definitions/dataConnection/properties/name",
          "title": "Name",
          "type": [ "string", "null" ],
          "description": "A connection token, key or string"
        },
        "classifications": {
          "$id": "#/definitions/dataConnection/properties/classifications",
          "title": "Classifications",
          "description": "Classification for the Data Item Query.",
          "type": [ "array", "null" ],
          "items": {
            "$ref": "#/definitions/dataClassification"
          },
          "minItems": 0,
          "uniqueItems": true
        },
        "extensions": {
          "$id": "#/definitions/dataConnection/properties/extensions",
          "title": "Extensions",
          "description": "Key/Value pair extension object.",
          "type": [ "array", "null" ],
          "items": {
            "$ref": "#/definitions/extension"
          },
          "minItems": 0,
          "uniqueItems": true
        },
        "description": {
          "$id": "#/definitions/dataConnection/properties/notes",
          "title": "Notes",
          "description": "Any additional information.",
          "type": "string"
        }
      }
    },
    "businessKeyDefinition": {
      "$id": "#/definitions/businessKeyDefinition",
      "title": "The definition of the business key",
      "description": "The generic definition of business key.",
      "type": "object",
      "required": [ "businessKeyComponentMapping" ],
      "properties": {
        "id": {
          "$id": "#/definitions/businessKeyDefinition/properties/id",
          "title": "Id",
          "description": "An optional identifier.",
          "type": [ "string", "null" ]
        },
        "businessKeyComponentMappings": {
          "$id": "#/definitions/businessKeyDefinition/properties/businessKeyComponentMappings",
          "title": "businessKeyComponentMappings",
          "description": "Items that define the Business Key e.g. the collection of columns for a Business Key.",
          "type": "array",
          "items": {
            "$ref": "#/definitions/dataItemMapping"
          }
        },
        "surrogateKey": {
          "$id": "#/definitions/businessKeyDefinition/properties/surrogateKey",
          "title": "Surrogate Key",
          "description": "An optional label for the end result e.g. the target business key attribute.",
          "type": [ "string", "null" ]
        },
        "classification": {
          "$id": "#/definitions/businessKeyDefinition/properties/classifications",
          "title": "Classifications",
          "description": "Classification for the dataObject (free form).",
          "type": [ "array", "null" ],
          "items": {
            "$ref": "#/definitions/dataClassification"
          },
          "minItems": 0,
          "uniqueItems": true
        },
        "extensions": {
          "$id": "#/definitions/businessKeyDefinition/properties/extensions",
          "title": "Extensions",
          "description": "Key/Value pair extension object.",
          "type": [ "array", "null" ],
          "items": {
            "$ref": "#/definitions/extension"
          },
          "minItems": 0,
          "uniqueItems": true
        },
        "description": {
          "$id": "#/definitions/dataConnection/properties/notes",
          "title": "Notes",
          "description": "Any additional information.",
          "type": "string"
        }
      }
    },
    "dataItem": {
      "$id": "#/definitions/dataItem",
      "title": "The individual value, query or attribute within a Data Object",
      "description": "The generic definition of a column. A column, attribute or item in a dataObject",
      "type": "object",
      "required": [ "name" ],
      "properties": {
        "id": {
          "$id": "#/definitions/dataItem/properties/id",
          "title": "Id",
          "description": "An optional identifier.",
          "type": [ "string", "null" ]
        },
        "name": {
          "$id": "#/definitions/dataItem/properties/name",
          "title": "Name",
          "description": "Unique name which identifies the column / attribute.",
          "type": "string"
        },
        "dataObject": {
          "$id": "#/definitions/dataItem/properties/dataObject",
          "title": "Data Object",
          "description": "The data object of the data item, which may be required when the data item is used in a data item mapping context.",
          "type": [ "object", "null" ],
          "$ref": "#/definitions/dataObject"
        },
        "dataType": {
          "$id": "#/definitions/dataItem/properties/dataType",
          "title": "Data type",
          "description": "Data type of the Data Item.",
          "type": [ "string", "null" ]
        },
        "characterLength": {
          "$id": "#/definitions/dataItem/properties/characterLength",
          "title": "Character Length",
          "description": "Optional. Length of the item in case it is text.",
          "type": [ "integer", "null" ]
        },
        "numericPrecision": {
          "$id": "#/definitions/dataItem/properties/numericPrecision",
          "title": "Numeric Precision",
          "description": "The number of digits in a numeric value (number).",
          "type": [ "integer", "null" ]
        },
        "numericScale": {
          "$id": "#/definitions/dataItem/properties/numericScale",
          "title": "Numeric Scale",
          "description": "The number of digits to the right of the decimal point.",
          "type": [ "integer", "null" ]
        },
        "ordinalPosition": {
          "$id": "#/definitions/dataItem/properties/ordinalPosition",
          "title": "Ordinal Position",
          "description": "Optional. The position of items in the data object.",
          "type": [ "integer", "null" ]
        },
        "isPrimaryKey": {
          "$id": "#/definitions/dataItem/properties/isPrimaryKey",
          "title": "Is Primary Key",
          "description": "Boolean value indicating if the item is a Primary Key.",
          "type": [ "boolean", "null" ]
        },
        "classifications": {
          "$id": "#/definitions/dataItem/properties/classifications",
          "title": "Classifications",
          "description": "Classification for the dataObject (free form).",
          "type": [ "array", "null" ],
          "items": {
            "$ref": "#/definitions/dataClassification"
          },
          "minItems": 0,
          "uniqueItems": true
        },
        "extensions": {
          "$id": "#/definitions/dataItem/properties/extensions",
          "title": "Extensions",
          "description": "Key/Value pair extension object.",
          "type": [ "array", "null" ],
          "items": {
            "$ref": "#/definitions/extension"
          },
          "minItems": 0,
          "uniqueItems": true
        },
        "description": {
          "$id": "#/definitions/dataItem/properties/notes",
          "title": "Notes",
          "description": "Any additional information.",
          "type": "string"
        }
      }
    },
    "dataItemMapping": {
      "$id": "#/definitions/dataItemMapping",
      "title": "The individual mappings between Data Items",
      "description": "A mapping between a source and a target columns or attributes",
      "type": "object",
      "required": [
        "sourceDataItems",
        "targetDataItem"
      ],
      "properties": {
        "id": {
          "$id": "#/definitions/dataItem/properties/id",
          "title": "Id",
          "description": "An optional identifier.",
          "type": [ "string", "null" ]
        },
        "sourceDataItems": {
          "$id": "#/definitions/dataItemMapping/properties/sourceDataItems",
          "title": "Source Data Items",
          "description": "The source item of the mapping. This can either be an column or a query.",
          "oneOf": [
            {
              "type": [ "array", "null" ],
              "items": {
                "$ref": "#/definitions/dataItem"
              }
            },
            {
              "type": [ "array", "null" ],
              "items": {
                "$ref": "#/definitions/dataItemQuery"
              }
            }
          ]
        },
        "targetDataItem": {
          "$id": "#/definitions/dataItemMapping/properties/targetDataItem",
          "title": "Target Data Item",
          "description": "The target item of the mapping.",
          "$ref": "#/definitions/dataItem"
        },
        "extensions": {
          "$id": "#/definitions/dataItemMapping/properties/extensions",
          "title": "Extensions",
          "description": "Key/Value pair extension object.",
          "type": [ "array", "null" ],
          "items": {
            "$ref": "#/definitions/extension"
          },
          "minItems": 0,
          "uniqueItems": true
        }
      }
    }
  }
} 
```
