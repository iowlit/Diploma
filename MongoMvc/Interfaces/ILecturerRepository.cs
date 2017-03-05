using MongoMvc.Model;
using System.Collections.Generic;

namespace MongoMvc.Interfaces
{
    public interface ILecturerRepository: IRepository<Lecturer>
    {
        List<Lecturer> GetLectorsByArray(string[] lcs);
    }
}
