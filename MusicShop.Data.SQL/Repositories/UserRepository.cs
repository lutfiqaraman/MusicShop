using MusicShop.Core.Models;
using MusicShop.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Data.SQL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private MusicStoreDbContext DbContext
        {
            get { return Context as MusicStoreDbContext; }
        }

        public UserRepository(MusicStoreDbContext context) : base(context)
        {

        }

        public Task<User> Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User> Create(User user, string password)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(User user, string password = null)
        {
            throw new NotImplementedException();
        }
    }
}
