using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SIQbic.API.Model;

namespace SIQbic.API.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);

        Task<User> Login(string username, string password);

        Task<bool> UserExists(string username);

        Task<bool> ChangePassword(string username, string password, string newPassword);        

        Task<List<Question>> GetQuestions();

        Task<User> GetUserById(int userId);

        void RequestInvitation(string sponsorEmail, int roleId);

        Task<bool> CreateInvitation(RegistrationCode code);

        Task<List<RegistrationCode>> GetInvitations();

        Task<bool> UpdateRegisterCodeRecord(string regCode, string status, int userId);
    }
}