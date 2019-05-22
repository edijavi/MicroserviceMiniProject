#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see http://aka.ms/containercompat 

FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY CustomerApi/CustomerApi.csproj CustomerApi/
COPY SharedModels/SharedModels.csproj SharedModels/
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