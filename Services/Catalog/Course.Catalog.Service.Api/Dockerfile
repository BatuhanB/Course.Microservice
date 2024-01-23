FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Catalog/Course.Catalog.Service.Api/Course.Catalog.Service.Api.csproj", "Services/Catalog/Course.Catalog.Service.Api/"]
COPY ["Shared/Course.Shared/Course.Shared.csproj", "Shared/Course.Shared/"]
RUN dotnet restore "Services/Catalog/Course.Catalog.Service.Api/Course.Catalog.Service.Api.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/Course.Catalog.Service.Api"
RUN dotnet build "Course.Catalog.Service.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Course.Catalog.Service.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Course.Catalog.Service.Api.dll"]
