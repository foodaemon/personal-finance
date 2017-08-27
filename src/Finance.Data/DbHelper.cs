using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Finance.Data
{
    public class DbHelper
    {
        public static IMongoDatabase GetConnection() 
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true).Build();
            var mongoUri = config["MongoUri"];
            var client = new MongoClient(mongoUri);
            var db = config["Database"];
            var database = client.GetDatabase(db);
            return database;
        }
    }
}
