using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using trialApps.API.Data;
using trialApps.API.Models;
using trialApps.API.Repo.Interface;

namespace trialApps.API.Repo.Implement
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext ctx;
        public DatingRepository(DataContext ctx)
        {
            this.ctx = ctx;

        }
        public void Add<T>(T entity) where T : class
        {
            ctx.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            ctx.Remove(entity);
        }

        public async Task<Photo> GetMainPhoto(int userId)
        {
            return await ctx.Photos.Where(Data => Data.UserId == userId)
                            .FirstOrDefaultAsync(item => item.isMain);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await ctx.Photos.FirstOrDefaultAsync(data => data.Id == id);

            return photo;
        }

        public async Task<User> GetUser(int id)
        {
            return await ctx.Users.Include(ent => ent.Photos).FirstOrDefaultAsync(data => data.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await ctx.Users.Include(ent => ent.Photos).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await ctx.SaveChangesAsync() > 0;
        }
    }
}