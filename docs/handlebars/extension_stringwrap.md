# {{stringwrap}}

Wraps an input character string with two specified characters.

## Usage

``` handlebars
{{stringwrap "<start character>" "<end character>"}}
```

## Example

``` handlebars
This adds brackets around the string value: {{stringwrap "Example" "[" "]"}}
```

This results in:

```dotnetcli
[Example]
```
