# {{stringdiff}}

Block helper to evaluate if the input does *not* match a specified string. Also optionally supports an {{else}} block.

## Usage

``` handlebars
{{#stringdiff string1 string2}} do something {{else}} do something else {{/stringdiff}}
```

## Example

``` handlebars
A and B {{#stringdiff "A" "B"}}are not the same. {{else}}are the same. {{/stringdiff}}
```

This results in:

```dotnetcli
A and B are not the same.
```
