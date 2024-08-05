FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80

COPY Shared/Course.Shared/Course.Shared.csproj Shared/Course.Shared/
COPY Services/Order/Course.Order.Application/Course.Order.Application.csproj Services/Order/Course.Order.Application/
COPY Services/Order/Course.Order.Core/Course.Order.Core.csproj Services/Order/Course.Order.Core/
COPY Services/Order/Course.Order.Domain/Course.Order.Domain.csproj Services/Order/Course.Order.Domain/
COPY Services/Order/Course.Order.Infrastructure/Course.Order.Infrastructure.csproj Services/Order/Course.Order.Infrastructure/
COPY Services/Order/Course.Order.API/Course.Order.API.csproj Services/Order/Course.Order.API/

RUN dotnet restore Services/Order/Course.Order.API/Course.Order.API.csproj
COPY . .

RUN dotnet publish Services/Order/Course.Order.API/Course.Order.API.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app

COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Course.Order.API.dll" ]