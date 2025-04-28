#!/bin/bash

# Exit immediately if a command exits with a non-zero status
set -e

echo "🔵 Building the project..."
dotnet build

echo "🔵 Running tests and creating .trx file..."
dotnet test --logger "trx;LogFileName=./TestResults.trx"

echo "🔵 Generating HTML report..."
reportgenerator -reports:TestResults/TestResults.trx -targetdir:Reports -reporttypes:Html

echo "✅ Report generated at: Reports/index.html"
echo "🔵 Opening HTML report..."
open Reports/index.html