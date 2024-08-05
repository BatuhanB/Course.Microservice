FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80

COPY Shared/Course.Shared/Course.Shared.csproj Shared/Course.Shared/
COPY Services/Basket/Course.Basket.Service.Api/Course.Basket.Service.Api.csproj Services/Basket/Course.Basket.Service.Api/

RUN dotnet restore Services/Basket/Course.Basket.Service.Api/Course.Basket.Service.Api.csproj
COPY . .

RUN dotnet publish Services/Basket/Course.Basket.Service.Api/Course.Basket.Service.Api.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app

COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Course.Basket.Service.Api.dll" ]