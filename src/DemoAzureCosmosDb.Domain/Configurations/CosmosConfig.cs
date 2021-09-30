namespace DemoAzureCosmosDb.Domain.Configurations
{
    public class CosmosConfig
    {
        public string Account { get; set; }
        public string Key { get; set; }
        public string DatabaseName { get; set; }
        public string ContainerName { get; set; }
    }
}
