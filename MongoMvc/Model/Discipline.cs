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
        [Display(Name = "Для вибору")]
        NotRequired = 1,
        [Display(Name = "За вибором студента")]
        StudentChoice = 2
    }

    public enum ControlType
    {
        [Display(Name = "Екзамен")]
        Exam = 0,
        [Display(Name = "Залік")]
        Zalik = 1
    }

    public class Discipline : IEquatable<Discipline>, IComparable<Discipline>
    {
        public Discipline()
        {
            Lectors = new List<Lecturer>();
            Id = ObjectId.GenerateNewId().ToString();
        }

        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public String Id { get; set; }

        [Required(ErrorMessage = "Поле назви є обов'язковим")]
        [Display(Name = "Назва")]
        public String Name { get; set; } = string.Empty;

        [Display(Name = "Курс")]
        public int Course { get; set; } = 1;

        [Display(Name = "Семестр")]
        public YearPart YearPart { get; set; } = YearPart.Autumn;

        [Display(Name = "Тип модуля")]
        public ModuleType ModuleType { get; set; } = ModuleType.Required;

        [Display(Name = "Обсяг модуля")]
        public String ModuleDescr { get; set; }

        [Display(Name = "Лектори")]
        public List<Lecturer> Lectors { get; set; }

        [Display(Name = "Посібники, підручники, тексти лекцій")]
        public String Books { get; set; } = string.Empty;

        [Display(Name = "Методичні вказівки")]
        public List<String> InstructionFiles { get; set; } = new List<String>();

        public String Instructions { get; set; } = string.Empty;

        [Display(Name = "НМЕК")]
        public String HMEK { get; set; } = string.Empty;

        [Display(Name = "ВНС кафедри, мережевий диск")]
        public String VNS { get; set; } = string.Empty;

        [Display(Name = "Семестровий контроль")]
        public ControlType ControlType { get; set; } = ControlType.Exam;
        public DateTime UpdatedOn { get; set; } = DateTime.Now.Date;

        [Display(Name = "Робоча програма")]
        public List<String> ProgramFiles { get; set; } = new List<String>();

        //for remove and sort in list
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Discipline objAsPart = obj as Discipline;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return Convert.ToInt32(Id);
        }
        public bool Equals(Discipline other)
        {
            if (other == null) return false;
            return (this.Id.Equals(other.Id));
        }

        public static int Compare(Discipline x, Discipline y)
        {
            return x.Name.CompareTo(y.Name);
        }

        public int CompareTo(Discipline other)
        {
            return -this.UpdatedOn.CompareTo(other.UpdatedOn);
        }
    }        
}
