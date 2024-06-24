namespace Course.Web.Models
{
    public class ServiceApiSettings
    {
        public string IdentityUrl { get; set; }
        public string BaseUrl { get; set; }
        public string PhotoStockUrl { get; set; }
        public ServiceApi CatalogApi { get; set; }
    }

    public class ServiceApi
    {
        public string Path { get; set; }
    }
}
