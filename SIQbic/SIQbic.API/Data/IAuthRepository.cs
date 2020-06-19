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

        Task<string> RequestInvitation(string invetedEmail, string sponsorEmail, int roleId);

        Task<string> CreateInvitation(RegistrationCode code);

        Task<List<RegistrationCode>> GetInvitations();

        Task<RegistrationCode> GetInvitationById(int id);

        Task<bool> UpdateRegisterCodeRecord(string regCode, string status, int userId);

        Task<bool> SaveAll();
    }
}