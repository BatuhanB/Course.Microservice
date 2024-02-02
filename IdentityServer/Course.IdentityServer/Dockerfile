FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5002
EXPOSE 5003

ENV ASPNETCORE_URLS: "https://+:5003;http://+:5002"

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["IdentityServer/Course.IdentityServer/Course.IdentityServer.csproj", "IdentityServer/Course.IdentityServer/"]
RUN dotnet restore "./IdentityServer/Course.IdentityServer/./Course.IdentityServer.csproj"
COPY . .
WORKDIR "/src/IdentityServer/Course.IdentityServer"
RUN dotnet build "./Course.IdentityServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Course.IdentityServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Course.IdentityServer.dll"]