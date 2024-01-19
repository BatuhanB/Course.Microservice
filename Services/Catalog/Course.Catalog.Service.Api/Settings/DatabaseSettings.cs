namespace Course.Catalog.Service.Api.Settings;

public class DatabaseSettings : IDatabaseSettings
{
    public required string ConnectionString { get; set; }
    public required  string DatabaseName { get; set; }
    
    public Dictionary<string, string> CollectionNames { get; set; }

    public string GetCollectionName<TEntity>()
    {
        string entityName = typeof(TEntity).Name;
        
        if (CollectionNames.TryGetValue(entityName, out var collectionName))
        {
            return collectionName;
        }
        else
        {
            throw new InvalidOperationException($"Collection name not defined for entity type {entityName}");
        }
    }
}