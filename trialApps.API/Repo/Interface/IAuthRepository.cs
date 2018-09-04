using System.Threading.Tasks;
using trialApps.API.Models;

namespace trialApps.API.Repo.Interface
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);
    }
}