namespace TaskPilot.Api.Configurations
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
        public string TodoCollection { get; set; } = String.Empty;

        public string UserCollection { get; set; } = String.Empty;
    }
}