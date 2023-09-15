# Overview

As part of the schema definitions, a number of extensions in the templating language have been developed:

## Block helpers

Block helpers have a starting and closing tag, and often support if-then-else constructs.

* [{{#stringcompare}}](extension_stringcompare.md)
* [{{#stringdiff}}](extension_stringdiff.md)
* [{{#replicate}}](extension_replicate.md)
* [{{#exists}}](extension_exists.md)
* [{{#targetDataItemExists}}](extension_targetdataitemexists.md)

## String helpers

String helpers do not have the #, and no end tag. Instead, they accept one or more parameter values and return a string value to use in the templates.

* [{{now}}](extension_now.md)
* [{{randomdate}}](extension_randomdate.md)
* [{{randomnumber}}](extension_randomnumber.md)
* [{{randomstring}}](extension_randomstring.md)
* [{{space}}](extension_space.md)
* [{{stringwrap}}](extension_stringwrap.md)
* [{{stringreplace}}](extension_stringreplace.md)
* [{{lookupExtension}}](extension_lookupextension.md)
