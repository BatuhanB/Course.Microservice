FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80

COPY Shared/Course.Shared/Course.Shared.csproj Shared/Course.Shared/
COPY Services/Discount/Course.Discount.Service.Api/Course.Discount.Service.Api.csproj Services/Discount/Course.Discount.Service.Api/

RUN dotnet restore Services/Discount/Course.Discount.Service.Api/Course.Discount.Service.Api.csproj
COPY . .

RUN dotnet publish Services/Discount/Course.Discount.Service.Api/Course.Discount.Service.Api.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app

COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Course.Discount.Service.Api.dll" ]