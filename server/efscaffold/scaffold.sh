#!/bin/bash

# -------------------------------
# Safe EF Scaffold Script (Obviously made with LLM AI assistance)
# -------------------------------

# Exit immediately on errors
set -e

# -------------------------------
# Load environment variables safely
# -------------------------------
# Skip BOM if present
if [ -f ".env" ]; then
    # Source the env file safely (ignore BOM)
    source <(tail -c +4 .env 2>/dev/null || cat .env)
else
    echo ".env file not found! Aborting."
    exit 1
fi

# -------------------------------
# Basic validation
# -------------------------------
if [ -z "$CONN_STR" ]; then
    echo "CONN_STR is empty! Please check your .env file."
    exit 1
fi

# -------------------------------
# Create output directories if needed
# -------------------------------
OUTPUT_DIR="./Entities"
CONTEXT_DIR="."

mkdir -p "$OUTPUT_DIR"
mkdir -p "$CONTEXT_DIR"

# -------------------------------
# Run EF scaffolding
# -------------------------------
dotnet ef dbcontext scaffold \
"$CONN_STR" \
Npgsql.EntityFrameworkCore.PostgreSQL \
--output-dir "$OUTPUT_DIR" \
--context-dir "$CONTEXT_DIR" \
--context MyDbContext \
--no-onconfiguring \
--namespace efscaffold.Entities \
--context-namespace Infrastructure.Postgres.Scaffolding \
--schema birdy \
--force

echo "EF scaffolding completed successfully."
