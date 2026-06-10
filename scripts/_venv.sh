# Shared helper: ensure the Python venv exists and has deps installed.
# Sourced (not executed) by serve.sh / deploy-version.sh.
# Expects $repo to be set by the caller.
ensure_venv() {
  local venv="$repo/.venv"
  local py="$venv/bin/python"
  if [[ ! -x "$py" ]]; then
    echo "Creating venv at $venv ..."
    python3 -m venv "$venv"
    "$py" -m pip install --upgrade pip -q
    "$py" -m pip install -r "$repo/requirements.txt" -q
  elif [[ "$repo/requirements.txt" -nt "$venv/.deps-installed" ]]; then
    echo "Updating venv deps ..."
    "$py" -m pip install -r "$repo/requirements.txt" -q
  fi
  touch "$venv/.deps-installed"
}
