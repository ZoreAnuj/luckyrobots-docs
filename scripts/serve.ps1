<#
.SYNOPSIS
  Generate the API reference, then serve the docs locally with live reload.
.EXAMPLE
  ./scripts/serve.ps1
  ./scripts/serve.ps1 -Source C:\path\to\Hazel-ScriptCore\Source
#>
param([string]$Source)

$ErrorActionPreference = "Stop"
$repo = Split-Path $PSScriptRoot -Parent

& "$PSScriptRoot/generate.ps1" -Source $Source
& "$repo/.venv/Scripts/python.exe" -m mkdocs serve
