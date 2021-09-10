using MusicShop.Core;
using MusicShop.Core.Models;
using MusicShop.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShop.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork UnitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            return await UnitOfWork.Users.Authenticate(username, password);
        }

        public async Task<User> Create(User user, string password)
        {
            await UnitOfWork.Users.Create(user, password);
            await UnitOfWork.CommitAsync();

            return user;
        }

        public void Delete(int id)
        {
            UnitOfWork.Users.Delete(id);
            UnitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await UnitOfWork.Users.GetAllUsersAsync();
        }

        public async Task<User> GetUserById(int Id)
        {
            return await UnitOfWork.Users.GetUserByIdAsync(Id);
        }

        public void Update(User user, string password = null)
        {
            UnitOfWork.Users.Update(user, password);
            UnitOfWork.CommitAsync();
        }
    }
}
