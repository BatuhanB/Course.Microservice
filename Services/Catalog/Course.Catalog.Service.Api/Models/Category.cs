using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Course.Catalog.Service.Api.Models;

public class Category : BaseEntity
{
    public required string Name { get; set; }
}