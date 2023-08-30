# {{stringwrap}}

Wraps an input character string with two specified characters.

## Usage

```json
{{stringwrap "<start character>" "<end character>"}}
```

## Example

```json
This adds brackets around the string value: {{stringwrap "Example" "[" "]"}}
```

This results in:

```dotnetcli
[Example]
```
