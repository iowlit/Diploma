using MongoMvc.Model;
using System.Collections.Generic;

namespace MongoMvc.Repository
{
    public interface ILecturerRepository: IRepository<Lecturer>
    {
        List<Lecturer> GetLectorsByArray(string[] lcs);
    }
}
