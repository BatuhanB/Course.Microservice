FROM mcr.microsoft.com/dotnet/sdk:3.1 as build
WORKDIR /app
EXPOSE 80

COPY IdentityServer/Course.IdentityServer/Course.IdentityServer.csproj IdentityServer/Course.IdentityServer/
RUN dotnet restore IdentityServer/Course.IdentityServer/Course.IdentityServer.csproj
COPY . .

RUN dotnet publish IdentityServer/Course.IdentityServer/Course.IdentityServer.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:3.1 as runtime
WORKDIR /app

COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Course.IdentityServer.dll" ]