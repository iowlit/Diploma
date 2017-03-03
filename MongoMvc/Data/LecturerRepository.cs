using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoMvc.Interfaces;
using MongoMvc.Model;
using MongoDB.Bson;

namespace MongoMvc.Data
{
    public class LecturerRepository : IRepository<Lecturer>
    {
        private readonly Context _context = null;

        public LecturerRepository(IOptions<Settings> settings)
        {
            _context = new Context(settings);
        }

        public IEnumerable<Lecturer> GetAllNotes()
        {
            try
            {
                return _context.Lecturers.Find(_ => true).ToList();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<Lecturer>> GetAllNotesAsync()
        {
            try
            {
                return await _context.Lecturers.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Lecturer> GetNote(string id)
        {
            var filter = Builders<Lecturer>.Filter.Eq("Id", id);

            try
            {
                return await _context.Lecturers
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task AddNote(Lecturer item)
        {
            try
            {
                await _context.Lecturers.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<DeleteResult> RemoveNote(string id)
        {
            try
            {
                return await _context.Lecturers.DeleteOneAsync(
                     Builders<Lecturer>.Filter.Eq("Id", id));
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<UpdateResult> UpdateNote(string id, string Name)
        {
            var filter = Builders<Lecturer>.Filter.Eq(s => s.Id, id);
            var update = Builders<Lecturer>.Update
                            .Set(s => s.Name, Name);
                            //.CurrentDate(s => s.UpdatedOn);

            try
            {
                return await _context.Lecturers.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<ReplaceOneResult> UpdateNote(string id, Lecturer item)
        {
            try
            {
                return await _context.Lecturers
                            .ReplaceOneAsync(n => n.Id.Equals(id)
                                            , item
                                            , new UpdateOptions { IsUpsert = true });
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // Demo function - full document update
        public async Task<ReplaceOneResult> UpdateNoteDocument(string id, string body)
        {
            var item = await GetNote(id) ?? new Lecturer();
            item.Name = body;
            //item.UpdatedOn = DateTime.Now;

            return await UpdateNote(id, item);
        }

        public async Task<DeleteResult> RemoveAllNotes()
        {
            try
            {
                return await _context.Lecturers.DeleteManyAsync(new BsonDocument());
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
