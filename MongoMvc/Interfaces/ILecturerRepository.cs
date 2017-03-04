using MongoMvc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoMvc.Interfaces
{
    public interface ILecturerRepository: IRepository<Lecturer>
    {
        List<Lecturer> GetLectorsByArray(string[] lcs);
    }
}
