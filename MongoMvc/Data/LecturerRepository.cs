using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoMvc.Interfaces;
using MongoMvc.Model;
using MongoDB.Bson;
using MongoMvc.Data;

namespace MongoMvc.Data
{
    public class LecturerRepository : ILecturerRepository
    {
        private readonly Context _context = null;        

        public LecturerRepository(IOptions<Settings> settings)
        {
            _context = new Context(settings);            
        }

        public IEnumerable<Lecturer> GetAll()
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

        public async Task<IEnumerable<Lecturer>> GetAllAsync()
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

        public Lecturer GetById(string id)
        {
            var filter = Builders<Lecturer>.Filter.Eq("Id", id);

            try
            {
                return  _context.Lecturers
                                .Find(filter)
                                .FirstOrDefault();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Lecturer> GetByIdAsync(string id)
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

        public async Task AddAsync(Lecturer item)
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

        public void Add(Lecturer item)
        {
            try
            {
                _context.Lecturers.InsertOne(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<DeleteResult> RemoveByIdAsync(string id)
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
        

        public async Task<UpdateResult> UpdateAsync(string id, Lecturer item)
        {
            var filter = Builders<Lecturer>.Filter.Eq(s => s.Id, id);
            var update = Builders<Lecturer>.Update
                            .Set(s => s.Name, item.Name);                            

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

        
        List<Lecturer> ILecturerRepository.GetLectorsByArray(string[] lcs)
        {
            var list = new List<Lecturer>();
            foreach (var tmp in lcs)
            {
                if(GetById(tmp) != null)
                {
                    list.Add(GetById(tmp));
                }                
            }
            return list;
        }

        //    public async Task<ReplaceOneResult> UpdateNote(string id, Lecturer item)
        //    {
        //        try
        //        {
        //            return await _context.Lecturers
        //                        .ReplaceOneAsync(n => n.Id.Equals(id)
        //                                        , item
        //                                        , new UpdateOptions { IsUpsert = true });
        //        }
        //        catch (Exception ex)
        //        {
        //            // log or manage the exception
        //            throw ex;
        //        }
        //    }

        //    // Demo function - full document update
        //    public async Task<ReplaceOneResult> UpdateNoteDocument(string id, string body)
        //    {
        //        var item = GetById(id) ?? new Lecturer();
        //        item.Name = body;
        //        //item.UpdatedOn = DateTime.Now;

        //        return await UpdateNote(id, item);
        //    }

        //    public async Task<DeleteResult> RemoveAllNotes()
        //    {
        //        try
        //        {
        //            return await _context.Lecturers.DeleteManyAsync(new BsonDocument());
        //        }
        //        catch (Exception ex)
        //        {
        //            // log or manage the exception
        //            throw ex;
        //        }
        //    }
    }
}
