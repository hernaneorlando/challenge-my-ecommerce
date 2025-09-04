#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ./src/Common/ ./Common/
COPY ./src/CatalogManagement/ ./CatalogManagement/
RUN dotnet restore "CatalogManagement/CatalogManagement.WebApi/CatalogManagement.WebApi.csproj"
RUN dotnet publish "CatalogManagement/CatalogManagement.WebApi/CatalogManagement.WebApi.csproj" -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "CatalogManagement.WebApi.dll"]
EXPOSE 8080