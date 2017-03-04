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
    public class DisciplineRepository : IRepository<Discipline>
    {
        private readonly Context _context = null;

        public DisciplineRepository(IOptions<Settings> settings)
        {
            _context = new Context(settings);
        }

        public IEnumerable<Discipline> GetAll()
        {
            try
            {
                return _context.Disciplines.Find(_ => true).ToList();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }       

        public async Task<IEnumerable<Discipline>> GetAllAsync()
        {
            try
            {
                return await _context.Disciplines.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public Discipline GetById(string id)
        {
            var filter = Builders<Discipline>.Filter.Eq("Id", id);

            try
            {
                return  _context.Disciplines
                                .Find(filter)
                                .FirstOrDefault();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;                
            }
        }

        public async Task<Discipline> GetByIdAsync(string id)
        {
            var filter = Builders<Discipline>.Filter.Eq("Id", id);

            try
            {
                return await _context.Disciplines
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task AddAsync(Discipline item)
        {
            try
            {
                await _context.Disciplines.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public void Add(Discipline item)
        {
            try
            {
                _context.Disciplines.InsertOne(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<DeleteResult> RemoveById(string id)
        {
            try
            {
                return await _context.Disciplines.DeleteOneAsync(
                     Builders<Discipline>.Filter.Eq("Id", id));
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
        public async Task<UpdateResult> UpdateAsync(string id, Discipline item)
        {
            var filter = Builders<Discipline>.Filter.Eq(s => s.Id, id);
            var update = Builders<Discipline>.Update
                            .Set(s => s.Name, item.Name)
                            .CurrentDate(s => s.UpdatedOn);

            try
            {
                return await _context.Disciplines.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        //public async Task<ReplaceOneResult> UpdateAsync(string id, Discipline item)
        //{
        //    try
        //    {
        //        return await _context.Disciplines
        //                    .ReplaceOneAsync(n => n.Id.Equals(id)
        //                                    , item
        //                                    , new UpdateOptions { IsUpsert = true });
        //    }
        //    catch (Exception ex)
        //    {
        //        // log or manage the exception
        //        throw ex;
        //    }
        //}


        // Demo function - full document update
        //public async Task<ReplaceOneResult> UpdateNoteDocument(string id, string body)
        //{
        //    var item = GetById(id) ?? new Discipline();
        //    item.Name = body;
        //    item.UpdatedOn = DateTime.Now;

        //    return await UpdateNote(id, item);
        //}

        //public async Task<DeleteResult> RemoveAllNotes()
        //{
        //    try
        //    {
        //        return await _context.Disciplines.DeleteManyAsync(new BsonDocument());
        //    }
        //    catch (Exception ex)
        //    {
        //        // log or manage the exception
        //        throw ex;
        //    }
        //}
    }
}
