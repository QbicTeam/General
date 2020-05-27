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

    }
}