#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo "Taking down running containers..."
docker compose -f "$SCRIPT_DIR/compose.yaml" down

echo "Removing old images..."
docker image rm studieplusplus.api 2>/dev/null && echo "  Removed studieplusplus.api" || echo "  No old image to remove"

echo "Building and starting..."
docker compose -f "$SCRIPT_DIR/compose.yaml" up --build -d

echo "Containers running:"
docker compose -f "$SCRIPT_DIR/compose.yaml" ps

echo "Done. API available at http://localhost:5168/scalar/"
