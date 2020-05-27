using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SIQbic.API.Model;

namespace SIQbic.API.Data
{
    public class AuthRepository: IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            this._context = context;
        }

        public Task<bool> ChangePassword(string username, string password, string newPassword)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await this._context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await this._context.Users.AddAsync(user);
            await this._context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await this._context.Users.AnyAsync(u => u.UserName == username))
            {
                return true;
            }

            return false;
        }

        private void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        private bool VerifyPasswordHash(string password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for(int idx = 0; idx < computedHash.Length; idx++)
                {
                    if (computedHash[idx] != PasswordHash[idx]) return false;
                }
            } 

            return true;      
        }
        
    }
}