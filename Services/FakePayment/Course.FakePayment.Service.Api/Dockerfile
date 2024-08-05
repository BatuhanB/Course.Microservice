FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80

COPY Shared/Course.Shared/Course.Shared.csproj Shared/Course.Shared/
COPY Services/FakePayment/Course.FakePayment.Service.Api/Course.FakePayment.Service.Api.csproj Services/FakePayment/Course.FakePayment.Service.Api/

RUN dotnet restore Services/FakePayment/Course.FakePayment.Service.Api/Course.FakePayment.Service.Api.csproj
COPY . .

RUN dotnet publish Services/FakePayment/Course.FakePayment.Service.Api/Course.FakePayment.Service.Api.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app

COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Course.FakePayment.Service.Api.dll" ]