using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Collections.Generic;

namespace MongoMvc.Model
{
    public enum YearPart
    {        
        Autumn = 0,        
        Spring = 1
    }

    public enum ModuleType
    {           
        Required = 0,        
        NotRequired = 1
    }

    public enum ControlType
    {
        Exam = 0,
        Zalik = 1
    }


    public class Discipline
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Course { get; set; } = 1;
        public YearPart YearPart { get; set; } = YearPart.Autumn;
        public ModuleType ModuleType { get; set; } = ModuleType.Required;
        public string ModuleDescr { get; set; }
        public List<Lecturer> Lectors { get; set; }
        public ControlType ControlType { get; set; } = ControlType.Exam;
        public DateTime CreatedOn { get; set; } = DateTime.Now.Date;
        public DateTime UpdatedOn { get; set; } = DateTime.Now.Date;       

    }
}
