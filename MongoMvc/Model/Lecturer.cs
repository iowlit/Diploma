using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.ComponentModel.DataAnnotations;

namespace MongoMvc.Model
{
    public class Lecturer: IEquatable<Lecturer>
    {
        public Lecturer()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }

        [Display(Name = "ПІБ")]
        public string Name { get; set; }

        [Display(Name = "Деталі")]
        public string Descr { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Lecturer objAsPart = obj as Lecturer;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return Convert.ToInt32(Id);
        }
        public bool Equals(Lecturer other)
        {
            if (other == null) return false;
            return (this.Id.Equals(other.Id));
        }
    }
}
