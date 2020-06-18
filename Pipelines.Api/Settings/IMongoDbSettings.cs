namespace Pipelines.Api.Settings
{
    public interface IMongoDbSettings
    {
        string ConnectionString { get; }

        string DatabaseName { get; }

        string UsersCollectionName { get; }

        string TasksCollectionName { get; }

        string PipelinesCollectionName { get; }
    }

    internal class MongoDbSettings : IMongoDbSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string UsersCollectionName
        {
            get
            {
                return "Users";
            }
        }

        public string TasksCollectionName
        {
            get
            {
                return "Tasks";
            }
        }

        public string PipelinesCollectionName
        {
            get
            {
                return "Pipelines";
            }
        }
    }
}