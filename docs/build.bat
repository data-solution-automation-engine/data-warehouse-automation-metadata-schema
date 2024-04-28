@echo off

REM Make sure we're running this from the context of the script file location
pushd %~dp0

REM Build through docfx, using the docfx.json configuration file
REM This will generate the static site in the _site folder
REM note that docfx needs to be installed and available in the PATH

REM Install docfx: dotnet tool install -g docfx
REM Docos: https://dotnet.github.io/docfx/tutorial/docfx_getting_started.html

docfx docfx.json
