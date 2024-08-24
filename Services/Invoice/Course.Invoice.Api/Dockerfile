FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80

COPY Shared/Course.Shared/Course.Shared.csproj Shared/Course.Shared/
COPY Services/Invoice/Course.Invoice.Application/Course.Invoice.Application.csproj Services/Invoice/Course.Invoice.Application/
COPY Services/Invoice/Course.Invoice.Presentation/Course.Invoice.Presentation.csproj Services/Invoice/Course.Invoice.Presentation/
COPY Services/Invoice/Course.Invoice.Domain/Course.Invoice.Domain.csproj Services/Invoice/Course.Invoice.Domain/
COPY Services/Invoice/Course.Invoice.Infrastructure/Course.Invoice.Infrastructure.csproj Services/Invoice/Course.Invoice.Infrastructure/
COPY Services/Invoice/Course.Invoice.Api/Course.Invoice.API.csproj Services/Invoice/Course.Invoice.Api/

RUN dotnet restore Services/Invoice/Course.Invoice.Api/Course.Invoice.Api.csproj
COPY . .

RUN dotnet publish Services/Invoice/Course.Invoice.Api/Course.Invoice.Api.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app

COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Course.Invoice.Api.dll" ]