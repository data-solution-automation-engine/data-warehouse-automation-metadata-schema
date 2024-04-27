# {{replicate}}

Returns the input string value a number of times, as specified by the input integer.

This is often used to generate test data.

## Usage

```handlebars
{{#replicate 10}}
```

## Example

```handlebars
{{#replicate 3}}
This value is replicated 3 times!
{{/replicate}}
```

This returns:

```text
This value is replicated 3 times!
This value is replicated 3 times!
This value is replicated 3 times!
```
