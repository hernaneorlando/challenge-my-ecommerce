#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ./src/Common/ ./Common/
COPY ./src/SalesManagement/ ./SalesManagement/
RUN dotnet restore "SalesManagement/SalesManagement.WebApi/SalesManagement.WebApi.csproj"
RUN dotnet publish "SalesManagement/SalesManagement.WebApi/SalesManagement.WebApi.csproj" -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "SalesManagement.WebApi.dll"]
EXPOSE 8080