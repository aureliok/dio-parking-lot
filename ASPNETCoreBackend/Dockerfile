FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY ./bin/Release/net8.0/publish/ .

EXPOSE 80

ENTRYPOINT ["dotnet", "ASPNETCoreBackend.dll"]
