# {{randomdate}}

Generate a random date somewhere higher than the provided characters as four-digit integer (year) input.

This was originally added to generate test data, and is used for referential-integrity testing purposes.

## Usage

``` handlebars
{{randomdate 2020}}
```

## Example

``` handlebars
Here is a random date: {{randomdate 2020}}.
```

This may return:

```dotnetcli
2022-08-03T00:00:00.000000
```
