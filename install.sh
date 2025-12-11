#!/bin/bash
dotnet run --project app/Install/Install.csproj
cd banco-app
ng build
exit 0