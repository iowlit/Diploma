using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Security.Cryptography;

namespace MongoMvc.Model
{
    public class User
    {        
        public User(UserView uv)
        {
            Id = uv.Id;
            Email = uv.Email;
            using (var deriveBytes = new Rfc2898DeriveBytes(uv.Password, 20))
            {
                Salt = deriveBytes.Salt;
                Hash = deriveBytes.GetBytes(20);                
            }
        }
        [BsonId]
        public string Id { get; set; }
        
        public string Email { get; set; }
       
        public byte[] Hash { get; set; }

        public byte[] Salt { get; set; }
    }
}
