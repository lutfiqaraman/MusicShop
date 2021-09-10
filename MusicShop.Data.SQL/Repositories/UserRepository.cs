using Microsoft.EntityFrameworkCore;
using MusicShop.Core.Models;
using MusicShop.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await DbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int Id)
        {
            return await DbContext.Users
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();
        }

        public async Task<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            var user = await DbContext.Users.SingleOrDefaultAsync(x => x.Username == username);

            if (user == null)
                return null;

            if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public async Task<User> Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            var resultUser = await DbContext.Users.AnyAsync(x => x.Username == user.Username);

            if (resultUser)
                throw new Exception("Username is already taken");

            CreatePasswordHash(password, out byte[] passwordHash, out byte[]  passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await DbContext.Users.AddAsync(user);

            return user;
        }

        public void Update(User user, string password = null)
        {
            User updateUser = DbContext.Users.Find(user.Id);

            if (updateUser == null)
                throw new Exception("User cannot be found");

            if (user.Username != updateUser.Username)
            {
                if (DbContext.Users.Any(x => x.Username == user.Username))
                    throw new Exception("User is already exist");
            }

            updateUser.FirstName = user.FirstName;
            updateUser.LastName  = user.LastName;
            updateUser.Username  = user.Username;

            if (string.IsNullOrWhiteSpace(password))
            {
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                updateUser.PasswordHash = passwordHash;
                updateUser.PasswordSalt = passwordSalt;
            }

            DbContext.Users.Update(updateUser);
        }

        public void Delete(int id)
        {
            var user = DbContext.Users.Find(id);

            if (user != null)
                DbContext.Users.Remove(user);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null)
                throw new ArgumentNullException(password);

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace", password);

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPassword(string password, byte[] storedPasswordHash, byte[] storedPasswordSalt)
        {
            if (password == null)
                throw new ArgumentNullException(password);

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace", password);

            if (storedPasswordHash.Length != 64)
                throw new ArgumentNullException("Invalid passwordhash", "Stored PasswordHash");

            if (storedPasswordSalt.Length != 128)
                throw new ArgumentException("Invalid passwordSalt", "Stored PasswordSalt");

            using (var hmac = new HMACSHA512(storedPasswordSalt))
            {
                byte[] computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != storedPasswordHash[i])
                        return false;
                }
            }

            return true;
        }
    }
}
