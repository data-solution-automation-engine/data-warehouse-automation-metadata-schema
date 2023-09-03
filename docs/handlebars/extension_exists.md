# {{exists}}

This block helper extension specifically applies to the schema for data solution automation.

The helper can target specific segments in the metadata, and evaluates if either the segment exists at all (without specifying an optional search value) or if the provided optional search value exists in that segment.

Only the *multiActiveKey* and *targetDataItem* properties are currently supported.

## Usage

```json
{{#exists "<category / json segment>" "<optional value>"}}
```

## Example

```json
{{#exists multiActiveKey}}There is a multi-active key!{{else}}No multi-active key is found in this data object mapping.{{/exists}}
{{#exists multiActiveKey "DATE_OF_BIRTH"}}There is a multi-active key which is not DATE_OF_BIRTH{{else}}No multi-active key with DATE_OF_BIRTH is found in this data object mapping.{{/exists}}
{{#exists targetDataItem}}There is a target data item in this mapping!{{else}}No target data items are defined in this mapping.{{/exists}}
```

Depending on the metadata, this may result in:

```dotnetcli
There is a multi-active key!
No multi-active key with DATE_OF_BIRTH is found in this data object mapping.
There is a target data item in this mapping!
```
