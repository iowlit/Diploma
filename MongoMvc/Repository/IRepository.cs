using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongoMvc.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAll();
        T GetById(string id);
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T item);
        void Add(T item);
        Task<DeleteResult> RemoveByIdAsync(string id);
        Task<UpdateResult> UpdateAsync(string id, T item);        
    }
}
