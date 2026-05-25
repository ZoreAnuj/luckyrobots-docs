<#
.SYNOPSIS
  Build and publish a documentation version with mike (writes to the gh-pages branch).
.PARAMETER Version
  The version label, e.g. 2026.1 or 2026.2.
.PARAMETER Latest
  Also point the "latest" alias at this version (use for the current release).
.PARAMETER Push
  Push the gh-pages branch to origin. Omit to keep the deploy local for review.
.PARAMETER Source
  Path to Hazel-ScriptCore/Source (forwarded to the generator).
.EXAMPLE
  # Publish 2026.1 locally as the latest version, for preview:
  ./scripts/deploy-version.ps1 -Version 2026.1 -Latest
.EXAMPLE
  # Publish 2026.2 and push it live as the new latest:
  ./scripts/deploy-version.ps1 -Version 2026.2 -Latest -Push
#>
param(
  [Parameter(Mandatory)][string]$Version,
  [switch]$Latest,
  [switch]$Push,
  [string]$Source
)

$ErrorActionPreference = "Stop"
$repo = Split-Path $PSScriptRoot -Parent

# mike shells out to `mkdocs`, so the venv's Scripts dir must be on PATH for this process.
$venvScripts = Join-Path $repo ".venv/Scripts"
$env:PATH = "$venvScripts;$env:PATH"
$mike = Join-Path $venvScripts "mike.exe"

# 1. Regenerate the API reference so the published version matches current source.
& "$PSScriptRoot/generate.ps1" -Source $Source

# 2. Deploy with mike.
$deploy = @("deploy", "--update-aliases")
if ($Push) { $deploy += "--push" }
$deploy += $Version
if ($Latest) { $deploy += "latest" }
& $mike @deploy

# 3. Keep "latest" as the site's default landing version.
if ($Latest) {
  $setDefault = @("set-default", "--allow-empty")
  if ($Push) { $setDefault += "--push" }
  $setDefault += "latest"
  & $mike @setDefault
}

Write-Host "`nPublished docs version '$Version'." -ForegroundColor Green
& $mike list
