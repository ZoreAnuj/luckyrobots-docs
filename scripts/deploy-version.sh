#!/usr/bin/env bash
#
# Build and publish a documentation version with mike (writes to the gh-pages branch).
#
# Usage:
#   ./scripts/deploy-version.sh --version VERSION [--latest] [--push] [--source PATH]
#
# --version  The version label, e.g. 2026.1 or 2026.2. (required)
# --latest   Also point the "latest" alias at this version (use for the current release).
# --push     Push the gh-pages branch to origin. Omit to keep the deploy local for review.
# --source   Path to Hazel-ScriptCore/Source (forwarded to the generator).
#
# Examples:
#   # Publish 2026.1 locally as the latest version, for preview:
#   ./scripts/deploy-version.sh --version 2026.1 --latest
#
#   # Publish 2026.2 and push it live as the new latest:
#   ./scripts/deploy-version.sh --version 2026.2 --latest --push
set -euo pipefail

VERSION=""
LATEST=0
PUSH=0
SOURCE=""
while [[ $# -gt 0 ]]; do
  case "$1" in
    --version) VERSION="$2"; shift 2 ;;
    --latest) LATEST=1; shift ;;
    --push) PUSH=1; shift ;;
    --source) SOURCE="$2"; shift 2 ;;
    *) echo "Unknown argument: $1" >&2; exit 1 ;;
  esac
done

if [[ -z "$VERSION" ]]; then
  echo "Error: --version is required." >&2
  exit 1
fi

script_dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
repo="$(dirname "$script_dir")"

# shellcheck source=_venv.sh
source "$script_dir/_venv.sh"
ensure_venv

# mike shells out to `mkdocs`, so the venv's bin dir must be on PATH for this process.
venv_bin="$repo/.venv/bin"
export PATH="$venv_bin:$PATH"
mike="$venv_bin/mike"

# 1. Regenerate the API reference so the published version matches current source.
gen_args=()
if [[ -n "$SOURCE" ]]; then
  gen_args+=(--source "$SOURCE")
fi
"$script_dir/generate.sh" "${gen_args[@]}"

# 2. Deploy with mike.
deploy=(deploy --update-aliases)
if [[ $PUSH -eq 1 ]]; then deploy+=(--push); fi
deploy+=("$VERSION")
if [[ $LATEST -eq 1 ]]; then deploy+=(latest); fi
"$mike" "${deploy[@]}"

# 3. Keep "latest" as the site's default landing version.
if [[ $LATEST -eq 1 ]]; then
  set_default=(set-default --allow-empty)
  if [[ $PUSH -eq 1 ]]; then set_default+=(--push); fi
  set_default+=(latest)
  "$mike" "${set_default[@]}"
fi

printf '\nPublished docs version '\''%s'\''.\n' "$VERSION"
"$mike" list
