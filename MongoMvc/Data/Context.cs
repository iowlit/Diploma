using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoMvc.Model;

namespace MongoMvc.Data
{
    public class Context
    {
        private readonly IMongoDatabase _database = null;

        public Context(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Discipline> Disciplines
        {
            get
            {
                return _database.GetCollection<Discipline>("Discipline");
            }
        }

        public IMongoCollection<Lecturer> Lecturers
        {
            get
            {
                return _database.GetCollection<Lecturer>("Lector");
            }
        }
    }
}
