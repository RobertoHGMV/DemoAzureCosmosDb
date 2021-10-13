namespace DemoAzureCosmosDb.Domain.Configurations
{
    public class CosmosConfig
    {
        public string EndpointUri { get; set; }
        public string PrimaryKey { get; set; }
        public string DatabaseId { get; set; }
        public string containerId { get; set; }
    }
}
