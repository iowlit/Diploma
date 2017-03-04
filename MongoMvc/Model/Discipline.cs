using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
