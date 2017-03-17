using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MongoMvc.Model
{
    public class UserView
    {        
        public UserView()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
        [BsonId]
        public string Id { get; set; }
        [Required(ErrorMessage = "Вкажіть Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Вкажіть пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
