FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY src/backend/src/InnovaPrev/bin/Release/net8.0/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "InnovaPrev.dll"]