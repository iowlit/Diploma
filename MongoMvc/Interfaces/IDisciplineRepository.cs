using MongoMvc.Model;

namespace MongoMvc.Interfaces
{
    public interface IDisciplineRepository: IRepository<Discipline>
    {
        void RemoveLecturerAsync(string id, Lecturer lc);
    }
}
