using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using trialApps.API.Models;

namespace trialApps.API.Data
{
    public class Seeder
    {
        private readonly DataContext context;
        public Seeder(DataContext context)
        {
            this.context = context;

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()){
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public void SeedUser() {
            var UserData = System.IO.File.ReadAllText("Data/UserSeedData.json");
           // var Users = JsonConvert.DeserializeObject<List<User>>(UserData);
           JsonConvert.DeserializeObject<List<User>>(UserData)
           .ForEach(user => {
               byte[] pwHash, pwSalt;
               CreatePasswordHash("password", out pwHash, out pwSalt);

               user.PassHash = pwHash;
               user.PassSalt = pwSalt;
               user.Username = user.Username.ToLower();
               user.LastActive = user.CreatedDate;
               user.Photos.ToList()
                .ForEach(photo => {
                   photo.CreatedDate = user.CreatedDate;
                   photo.LastModified = user.CreatedDate;
                   photo.LastModifiedBy = user.Username;
                   photo.CreatedBy = user.Username;
               });
               context.Users.Add(user);
           });
           context.SaveChanges();



        }
    }
}