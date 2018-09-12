using System.Collections.Generic;
using System.Threading.Tasks;
using trialApps.API.Models;

namespace trialApps.API.Repo.Interface
{
    public interface IDatingRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T:class;
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);
         Task<Photo> GetPhoto(int id); 

         Task<Photo> GetMainPhoto(int userId);
    }
}