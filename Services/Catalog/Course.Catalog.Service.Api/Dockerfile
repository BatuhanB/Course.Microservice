FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80

COPY Shared/Course.Shared/Course.Shared.csproj Shared/Course.Shared/
COPY Services/Catalog/Course.Catalog.Service.Api/Course.Catalog.Service.Api.csproj Services/Catalog/Course.Catalog.Service.Api/

RUN dotnet restore Services/Catalog/Course.Catalog.Service.Api/Course.Catalog.Service.Api.csproj
COPY . .

RUN dotnet publish Services/Catalog/Course.Catalog.Service.Api/Course.Catalog.Service.Api.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app

COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Course.Catalog.Service.Api.dll" ]