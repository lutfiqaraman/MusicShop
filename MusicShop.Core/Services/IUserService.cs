using MusicShop.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShop.Core.Services
{
    public interface IUserService
    {
        Task<IList<User>> GetAllUsers();
        Task<User> GetUserById(int Id);
        Task<User> Authenticate(string username, string password);
        Task<User> Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
    }
}
