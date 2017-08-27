using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Finance.Data 
{
    public class MongoContext 
    {
        public IMongoDatabase Database { get; private set; }

        public MongoContext()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true).Build();
            var mongoUri = config["MongoUri"];
            var url = new MongoUrl(mongoUri);
            IMongoClient client = new MongoClient(url);
            Database = client.GetDatabase(url.DatabaseName);
        }
    }
}