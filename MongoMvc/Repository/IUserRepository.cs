using MongoMvc.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoMvc.Repository
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User> GetUserAsync(string Email, string Password);

        Task<User> GetUserAsync(string Email);
    }
}
