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
    public class UserRepository : IUserRepository
    {
        private readonly Context _context = null;        

        public UserRepository(IOptions<Settings> settings)
        {
            _context = new Context(settings);            
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public User GetById(string id)
        {
            throw new NotImplementedException();
        }                    

        public async Task<User> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }        

        public async Task AddAsync(User item)
        {
            try
            {
                await _context.Users.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }        

        public void Add(User item)
        {
            try
            {
                _context.Users.InsertOne(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }        

        public async Task<DeleteResult> RemoveByIdAsync(string id)
        {
            throw new NotImplementedException();
        }        

        public async Task<UpdateResult> UpdateAsync(string id, User item)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserAsync(string Email, string Password)
        {
            var filter = Builders<User>.Filter.Where(u => u.Email == Email && u.Password == Password);

            try
            {
                return await _context.Users
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<User> GetUserAsync(string Email)
        {
            var filter = Builders<User>.Filter.Eq("Email", Email);

            try
            {
                return await _context.Users
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }


    }
}
