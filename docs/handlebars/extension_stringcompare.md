# {{stringcompare}}

Block helper to evaluate if the input matches a specified string. Also optionally supports an {{else}} block.

## Usage

```json
{{#stringcompare string1 string2}} do something {{else}} do something else {{/stringcompare}}
```

## Example

```json
A and B {{#stringcompare "A" "B"}}are the same. {{else}}are not the same. {{/stringcompare}}
```

This results in:

```dotnetcli
A and B are not the same.
```
