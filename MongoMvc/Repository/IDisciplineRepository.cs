using MongoMvc.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoMvc.Repository
{
    public interface IDisciplineRepository: IRepository<Discipline>
    {
        void RemoveLecturerAsync(string id, Lecturer lc);
        Task<IEnumerable<Discipline>> GetByCourseAsync(int CourseId);
    }
}
