#!/usr/bin/env bash
#
# Generate the API reference, then serve the docs locally with live reload.
#
# Usage:
#   ./scripts/serve.sh
#   ./scripts/serve.sh --source /path/to/Hazel-ScriptCore/Source
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

# apidocgen targets net9.0; allow it to run on a newer (.NET 10+) runtime.
export DOTNET_ROLL_FORWARD=Major

# shellcheck source=_venv.sh
source "$script_dir/_venv.sh"
ensure_venv

gen_args=()
if [[ -n "$SOURCE" ]]; then
  gen_args+=(--source "$SOURCE")
fi
"$script_dir/generate.sh" "${gen_args[@]}"
"$repo/.venv/bin/python" -m mkdocs serve
