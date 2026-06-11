#!/usr/bin/env bash
#
# Generate the API-reference Markdown from the LuckyEngine C# source.
#
# Usage:
#   ./scripts/generate.sh [--source PATH]
#
# --source  Path to Hazel-ScriptCore/Source. If omitted, apidocgen resolves it
#           from $LUCKYENGINE_SCRIPTCORE or the candidates in apidocgen.config.json.
#
# Example:
#   ./scripts/generate.sh --source ~/Dev/LuckyRobots/LuckyEngine/Hazel-ScriptCore/Source
set -euo pipefail

SOURCE=""
while [[ $# -gt 0 ]]; do
  case "$1" in
    --source) SOURCE="$2"; shift 2 ;;
    *) echo "Unknown argument: $1" >&2; exit 1 ;;
  esac
done

script_dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
repo="$(dirname "$script_dir")"

dotnet build "$repo/tools/ApiDocGen/ApiDocGen.csproj" -c Release -v quiet
exe="$repo/tools/ApiDocGen/bin/Release/net9.0/apidocgen"

gen_args=(--root "$repo")
if [[ -n "$SOURCE" ]]; then
  gen_args+=(--source "$SOURCE")
fi
"$exe" "${gen_args[@]}"
