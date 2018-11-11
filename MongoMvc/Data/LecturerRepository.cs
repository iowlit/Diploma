using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoMvc.Repository;
using MongoMvc.Model;
using System.Collections;

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
                var lcs = _context.Lecturers.Find(_ => true).ToList();
                lcs.Sort();
                return lcs; 
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
                var lcs = await _context.Lecturers.Find(_ => true).ToListAsync();
                lcs.Sort();
                return lcs;
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
                return _context.Lecturers
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
        
        public List<Lecturer> GetLectorsByArray(string[] lcs)
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
    }
}
