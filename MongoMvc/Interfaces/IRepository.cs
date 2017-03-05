using System.Collections.Generic;
using System.Threading.Tasks;
using MongoMvc.Model;
using MongoDB.Driver;

namespace MongoMvc.Interfaces
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

        // demo interface - full document update
        //Task<ReplaceOneResult> UpdateNoteDocument(string id, string body);

        //// should be used with high cautious, only in relation with demo setup
        //Task<DeleteResult> RemoveAllNotes();
    }
}
