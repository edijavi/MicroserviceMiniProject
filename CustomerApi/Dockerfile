FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY CustomerApi/CustomerApi.csproj CustomerApi/
RUN dotnet restore CustomerApi/CustomerApi.csproj
COPY . .
WORKDIR /src/CustomerApi
RUN dotnet build CustomerApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish CustomerApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CustomerApi.dll"]