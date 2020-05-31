using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SIQbic.API.Model;
using SIQbic.API.Model.Enums;

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

        public async Task<List<Question>> GetQuestions()
        {
            return await this._context.Questions.ToListAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            var user = await this._context.Users.Include(rc => rc.RegistrationCodes).FirstOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        public async void RequestInvitation(string sponsorEmail) 
        {
            int EXPIRATION_DATE_VIGENCY = 5;

            string rcode = GenerateInvitationCode();

            this._context.Invitations.Add(new RegistrationCode{
                Code = rcode,
                DateCreated = DateTime.Now,
                DueDate = DateTime.Now.AddDays(EXPIRATION_DATE_VIGENCY),
                SponsorEmail = sponsorEmail,
                Status = RegistrationCodeStatusType.Requested.ToString()
            });

            await this._context.SaveChangesAsync();
        }

        public async Task<List<RegistrationCode>> GetInvitations()
        {
            return await this._context.Invitations.ToListAsync();
        }

        public async Task<bool> UpdateRegisterCodeRecord(string regCode, string status, int userId) 
        {
            var reg = await this._context.Invitations.FirstOrDefaultAsync(i => i.Code == regCode);
            var user = await this._context.Users.FirstOrDefaultAsync();


            if (reg != null)
            {
                reg.Status = status;
                if (user.RegistrationCodes == null) user.RegistrationCodes = new List<RegistrationCode>();
                user.RegistrationCodes.Add(reg);
                await this._context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }

        }

        private string GenerateInvitationCode() 
        {
            string result = string.Empty;
            DateTime rDate =  DateTime.Now;

            Random rnd = new Random(DateTime.Now.Second);
            int fr1 = rnd.Next(10, 99);
            int fr2 = rnd.Next(10, 99);
            int fr3 = rnd.Next(10, 99);
            int fr4 = rnd.Next(10, 99);
            int fr5 = rnd.Next(10, 99);
            int fr6 = rnd.Next(10, 99);
            int fr7 = rnd.Next(10, 99);

            result = "r" + fr1.ToString() + rDate.Year.ToString()
                + fr2.ToString() + rDate.Month.ToString("00")
                + fr3.ToString() + rDate.Day.ToString("00")
                + fr4.ToString() + rDate.Hour.ToString("00")
                + fr5.ToString() + rDate.Minute.ToString("00")
                + fr6.ToString() + rDate.Second.ToString("00")
                + fr7.ToString();

            return result;
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