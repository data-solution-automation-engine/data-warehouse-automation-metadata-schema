# {{lookupExtension}}

This block helper extension specifically applies to the schema for data solution automation.

The lookupExtension helper allows a lookup of an extension by key value.

Pass in the extensions list and the string key value.

## Usage

```handlebars
{{lookupExtension <extension list> "<string value>"}}
```

## Example

```handlebars
The lookup value for the 'type' extension is '{{lookupExtension sourceDataObjects.0.extensions "type"}}'.
```

Depending on the metadata, this may result in:

```text
The lookup value for the 'type' extension is 'procedure'.
```
