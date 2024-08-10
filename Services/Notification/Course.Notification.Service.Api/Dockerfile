FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80

COPY Shared/Course.Shared/Course.Shared.csproj Shared/Course.Shared/
COPY Services/Notification/Course.Notification.Service.Api/Course.Notification.Service.Api.csproj Services/Notification/Course.Notification.Service.Api/

RUN dotnet restore Services/Notification/Course.Notification.Service.Api/Course.Notification.Service.Api.csproj
COPY . .

RUN dotnet publish Services/Notification/Course.Notification.Service.Api/Course.Notification.Service.Api.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app

COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Course.Notification.Service.Api.dll" ]