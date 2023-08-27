# Generation Metadata Schema for Data Warehouse Automation

## Intent

To provide a collaborative space to discuss an exchange format concerning data logistics ('ETL') generation metadata, supporting Data Warehouse Automation. This adapter should contain all metadata necessary to generate the transformation logic for a Data Warehouse solution.

## Why is this relevant?

Many, many, people have developed their own Data Warehouse automation and /or code generation solution to suit their specific needs. These tools often reinvent the wheel to a certain extent. Some of these are built inside existing tools (i.e. ERwin, Powerdesigner) using SDKs or macros. Others use different development frameworks (.net, Java) and most use differently modelled repositories or file formats to persist data on disk.

This is in addition to the many off-the-shelf DWA platforms, each of which has their own repository and format as well.

Rather than focusing on which solution is ‘best’, there might be a way where everyone can use different tools and technologies while still collaborating on an data warehouse automation ecosystem.

This means that various functions in this ecosystem need to be decoupled. The interface schema could potentially be used for this – as an agreement on how source-to-target (mapping) metadata can be captured for used by different tools in an ecosystem.

In the broader sense of meritocracy, it is worth pursuing if a common exchange format for metadata can be defined in a way that any developer can develop to in whatever technology or way their passion drives them.

## Requirements

The interface schema has the following requirements:

- Needs to be able to generate transformation logic using and externally applied template based on metadata available in the JSON.
- Needs to encompass all known Ensemble Modelling use-cases (see checklist for Data Vault conformance) in a single object.
- Make sure business keys are created in collections, to support concatenation and composition.
- Allow for flexibility in source usage, including code to support complex definitions.

### Structure

The following directories have been set up:

- Generic interface, containing the JSON schema definition.
- Class Library (DataWarehouseAutomation) containing the object model for deserialization, as well as various utility classes such as validation of files against the JSON schema definition.
- Code examples (examples_handlebars), containing C# examples using the generic interface for various purposes.
- Regression test project (test_project)

Across most, if not all, metadata models there is a core set of information that is required for any generation of data logistics code. If we can separate this from the UI / management of metadata we could have an exchange format that allows anyone to 'plug in' their own desired technology.

### Working guidelines

For any change, create a new branch (no direct commits to master branch).
