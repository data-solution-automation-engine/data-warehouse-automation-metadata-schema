# {{targetDataItemExists}}

This block helper extension specifically applies to the schema for data solution automation.

The helper performs a search in the target data items within a data item mapping, so evaluate if the input value exists there as a data item name.

This is used in some cases to handle special column names.

## Usage

``` handlebars
{{stringwrap "<character value>"}}
```

## Example

``` handlebars
{{#targetDataItemExists "FIRST_NAME"}}FIRST_NAME exists{{else}}FIRST_NAME does not exist{{/targetDataItemExists}}
```

This results in:

```dotnetcli
FIRST_NAME exists
```
