using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MongoMvc.Model
{
    public class User
    {        
        public User()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
        [BsonId]
        public string Id { get; set; }
        [Required(ErrorMessage = "Вкажіть Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Вкажіть пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
