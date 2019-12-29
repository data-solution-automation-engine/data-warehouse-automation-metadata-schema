# Checklist for Data Vault conformance	

This is a checklist to sanity check the file format against the known / listed Data Vault use-cases.

The use-case provides the support for a given scenario to be validated. The example provided is intended to explain how this use case is reflected in a sample interface file.

**Note:** just because the interface intends to support all known scenarios does not mean that you have to use them. Implementation and modelling should not be part of this discussion, and everyone should be able to model / implement according to his/her views and understanding.

| Use-case                        | Example provided              | Supported by generic interface (Y/N) | Comments                                                     | Example                                   |
| ------------------------------- | ----------------------------- | ------------------------------------ | ------------------------------------------------------------ | ----------------------------------------- |
| Normal Hub                      | HUB_INCENTIVE_OFFER           | Being tested                         | Simplest version of  Hub, only one Business Key.             | [Example normal Hub](#Example-Normal-Hub) |
| Composite Key Hub               | HUB_MEMBERSHIP_PLAN           |                                      | Concatenated and/or composite Business Key for a Hub.        |                                           |
| Regular Satellite               | SAT_CUSTOMER                  |                                      | Standard history tracking Satellite.                         |                                           |
| Multi-Active Satellite          | SAT_MEMBERSHIP_PLAN_VALUATION |                                      | Multi-active, or Multi-Variant flavour of a Satellite - including an additional non-Business Key attribute as part of the Primary Key. |                                           |
| Regular Link-Satellite          | LSAT_MEMBERSHIP               |                                      | Standard history tracking Link-Satellite                     |                                           |
| Driving Key Link-Satellite      | LSAT_CUSTOMER_OFFER           |                                      | Driving-Key Link-Satellite, which closes off relationships across selected Business Keys. |                                           |
| Multi-Active Link-Satellite     | LSAT_CUSTOMER_COSTING         |                                      | Multi-active, or Multi-Variant version of the Link-Satellite. |                                           |
| Normal Link                     | LNK_MEMBERSHIP                |                                      | Regular Link, intersection entity for storing distinct relationships. |                                           |
| Link with degenerate attribute  | LNK_MEMBERSHIP                |                                      | Degenerate attribute Link scenario, where a non-Business Key attribute is added to the Primary Key. |                                           |
| Same-As Link  Hierarchical LInk | LNK_RENEWAL_MEMBERSHIP        |                                      | Recursive and hierarchical structure support                 |                                           |
| Transactional Link              | TBD                           |                                      | Merger of relationship storage and history tracking (i.e. Link and Link_Satellite combined). |                                           |

Regression testing model used: https://app.sqldbm.com/SQLServer/Share/SwuyaPFdIML9QDEbH03eskGFrngIE8md_DYjF4jNYw0.



##### Example Normal Hub

```json
{
  "sourceToTargetMappingList" :
  [
    {
      // Source
      "sourceObject": 
      {
        "mappingObjectId": 2,
        "mappingObjectName": "STG_PROFILER_OFFER",
        "businessKey" : {
          "businessKeyComponentBehaviour" : "None",
          "businessKeyComponents" : [      
            {
              "columnName": "OfferID"
            }
          ]
        }   
      },
      // Target    
      "targetObject": 
      {
        "mappingObjectId": 2,
        "mappingObjectName": "HUB_INCENTIVE_OFFER",
        "businessKey" : {
          "businessKeyComponents" : [      
            {
              "columnName": "INCENTIVE_OFFER_HSH"
            }
          ]
        }
      },       
      // Mappings
      "filterCriterion": "3=3",
      "columnMapping": [
        {
          "sourceColumn": {"columnName": "OfferId"},
          "targetColumn": {"columnName": "OFFER_ID"}
        }
      ]
    }
  ]
}
```

