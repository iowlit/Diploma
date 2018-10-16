using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MongoMvc.Model
{
    public class MongoFile
    {
        public String ID { get; private set; }
        public String Name { get; private set; }
        public String FilePath
        {
            get
            {
                return ID + Extension;
            }
        }
        public String Extension
        {
            get
            {
                return Path.GetExtension(Name);
            }
        }
        public String NameWithoutExtension
        {
            get
            {
                return Path.GetFileNameWithoutExtension(Name);
            }
        }


        public MongoFile(String ID, String Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }
}
