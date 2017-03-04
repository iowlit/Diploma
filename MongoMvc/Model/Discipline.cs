using System;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;

namespace MongoMvc.Model
{
    public enum YearPart
    {
        [Display(Name = "Осінь")]
        Autumn = 0,
        [Display(Name = "Весна")]
        Spring = 1
    }

    public enum ModuleType
    {
        [Display(Name = "Обов'язковий")]
        Required = 0,
        [Display(Name = "Необов'язковий")]
        NotRequired = 1
    }

    public enum ControlType
    {
        [Display(Name = "Екзамен")]
        Exam = 0,
        [Display(Name = "Залік")]
        Zalik = 1
    }
       

    public class Discipline
    {
        public Discipline()
        {
            Lectors = new List<Lecturer>();
            Id = ObjectId.GenerateNewId().ToString();
        }

        public Discipline(Discipline dc)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Name = dc.Name;
            Course = dc.Course;
            YearPart = dc.YearPart;
            ModuleType = dc.ModuleType;
            ModuleDescr = dc.ModuleDescr;
            Lectors = dc.Lectors;
            ControlType = dc.ControlType;
            UpdatedOn = dc.UpdatedOn;
        }
                
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }

        [Display(Name = "Назва")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Курс")]
        public int Course { get; set; } = 1;

        [Display(Name = "Семестр")]
        public YearPart YearPart { get; set; } = YearPart.Autumn;

        [Display(Name = "Тип модуля")]
        public ModuleType ModuleType { get; set; } = ModuleType.Required;

        [Display(Name = "Обсяг модуля")]
        public string ModuleDescr { get; set; }

        [Display(Name = "Лектори")]
        public List<Lecturer> Lectors { get; set; }

        [Display(Name = "Семестровий контроль")]
        public ControlType ControlType { get; set; } = ControlType.Exam;        
        public DateTime UpdatedOn { get; set; } = DateTime.Now.Date;  
    }        
}
