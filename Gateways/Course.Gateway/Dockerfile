FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80

COPY Gateways/Course.Gateway/Course.Gateway.csproj Gateways/Course.Gateway/

RUN dotnet restore Gateways/Course.Gateway/Course.Gateway.csproj
COPY . .

RUN dotnet publish Gateways/Course.Gateway/Course.Gateway.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app

COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Course.Gateway.dll" ]