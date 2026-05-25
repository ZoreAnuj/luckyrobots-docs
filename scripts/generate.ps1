<#
.SYNOPSIS
  Generate the API-reference Markdown from the LuckyEngine C# source.
.PARAMETER Source
  Path to Hazel-ScriptCore/Source. If omitted, apidocgen resolves it from
  $env:LUCKYENGINE_SCRIPTCORE or the candidates in apidocgen.config.json.
.EXAMPLE
  ./scripts/generate.ps1 -Source C:\Dev\LuckyRobots\LuckyEngine\Hazel-ScriptCore\Source
#>
param([string]$Source)

$ErrorActionPreference = "Stop"
$repo = Split-Path $PSScriptRoot -Parent

dotnet build "$repo/tools/ApiDocGen/ApiDocGen.csproj" -c Release -v quiet
$exe = Join-Path $repo "tools/ApiDocGen/bin/Release/net9.0/apidocgen.exe"

$genArgs = @("--root", $repo)
if ($Source) { $genArgs += @("--source", $Source) }
& $exe @genArgs
