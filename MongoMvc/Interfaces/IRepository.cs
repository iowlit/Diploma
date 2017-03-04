using System.Collections.Generic;
using System.Threading.Tasks;
using MongoMvc.Model;
using MongoDB.Driver;

namespace MongoMvc.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllNotesAsync();
        IEnumerable<T> GetAllNotes();
        T GetNote(string id);
        Task AddNoteAsync(T item);
        void AddNote(T item);
        Task<DeleteResult> RemoveNote(string id);

        Task<UpdateResult> UpdateNote(string id, string body);

        // demo interface - full document update
        Task<ReplaceOneResult> UpdateNoteDocument(string id, string body);

        // should be used with high cautious, only in relation with demo setup
        Task<DeleteResult> RemoveAllNotes();
    }
}
