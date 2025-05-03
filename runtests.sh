#!/bin/bash

# Exit immediately if a command exits with a non-zero status
set -e

echo "🔵 Building the project..."
dotnet build

echo 🔵 Ensuring Playwright browsers are installed...on Mac / Windows
echo "playwright install || pwsh bin/Debug/net6.0/playwright.ps1 install"

echo 🔵 Installing Playwright browsers...
~/.dotnet/tools/playwright install || powershell -ExecutionPolicy Bypass -File "bin/Debug/net6.0/playwright.ps1"


echo "🔵 Running tests and creating .trx file..."
dotnet test --logger "trx;LogFileName=./TestResults.trx"

echo "🔵 Generating HTML report..."
reportgenerator -reports:TestResults/TestResults.trx -targetdir:Reports -reporttypes:Html

echo "✅ Report generated at: Reports/index.html"
echo "🔵 Opening HTML report..."
