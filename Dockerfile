FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src

COPY . .

RUN dotnet restore WebApi/WebApi.csproj
RUN dotnet publish WebApi/WebApi.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 10000

ENTRYPOINT ["dotnet", "WebApi.dll"]