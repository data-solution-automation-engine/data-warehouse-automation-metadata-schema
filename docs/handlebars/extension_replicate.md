# {{replicate}}

Returns the input string value a number of times, as specified by the input integer.

This is often used to generate test data.

## Usage

```json
{{#replicate 10}}
```

## Example

```json
{{#replicate 3}}
This value is replicated 3 times!
{{/replicate}}
```

This returns:

```dotnetcli
This value is replicated 3 times!
This value is replicated 3 times!
This value is replicated 3 times!
```
