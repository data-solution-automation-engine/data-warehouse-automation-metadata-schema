# {{stringreplace}}

Parses an input string value, and replaces a specified part of the contents with a replacement value.

## Usage

```json
{{stringreplace "<input string value>" "<lookup character>" "<replacement character>"}}
```

## Example

```json
This replaces the o's in Hello Word with an !: {{StringReplace "Hello World" "o" "!"}}
```

This results in:

```dotnetcli
This replaces the o's in Hello Word with an !: Hell! W!rld
```
