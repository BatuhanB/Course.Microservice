namespace Course.Catalog.Service.Api.Settings;

public interface IDatabaseSettings
{
    string GetCollectionName<TEntity>();
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
}