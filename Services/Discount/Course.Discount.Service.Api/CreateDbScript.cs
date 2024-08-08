namespace Discount.Course.Discount.Service.Api;

public static class CreateDbScript
{
    public const string CreateDbScriptSql = @"CREATE SCHEMA IF NOT EXISTS discount;

CREATE TABLE IF NOT EXISTS discount.discount (
    Id SERIAL PRIMARY KEY,
    Code VARCHAR(50) NOT NULL,
    Rate INT NOT NULL,
    CreatedTime TIMESTAMP WITHOUT TIME ZONE,
    UserId VARCHAR(50) NOT NULL 
);";
}