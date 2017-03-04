using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace MongoMvc.Model
{
    public class Lecturer
    {
        public Lecturer()
        {
            Id = ObjectId.GenerateNewId.ToString();
        }
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Descr { get; set; }
    }
}
