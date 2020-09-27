using System.Threading.Tasks;
using ZawajApp.API.Models;

namespace ZawajApp.API.Date.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> RegisterAsync(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> IsUserExists(string username);
    }
}