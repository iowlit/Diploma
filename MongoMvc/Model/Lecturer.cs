using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MongoMvc.Model
{
    public class Lecturer: IEquatable<Lecturer>, IComparable<Lecturer>
    {
        public Lecturer()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле назви є обов'язковим")]
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

        //for remove and sort in list
        public override int GetHashCode()
        {
            return Convert.ToInt32(Id);
        }
        public bool Equals(Lecturer other)
        {
            if (other == null) return false;
            return (this.Id.Equals(other.Id));
        }

        public static int Compare(Lecturer x, Lecturer y)
        {
            return x.Name.CompareTo(y.Name);
        }

        public int CompareTo(Lecturer other)
        {
            if(Name != null && other.Name != null)
            {
                return Name.CompareTo(other.Name);
            }
            else
            {
                return Id.CompareTo(other.Id);
            }            
        }
    }
}
