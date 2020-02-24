using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        public AuthRepository(DataContext context)
        {
            _Context = context;
        }

        private  DataContext _Context { get; }

        public async  Task<User> Login(string username, string password)
        {
            var user = await _Context.Users.FirstOrDefaultAsync(x=>x.Username==username);
            if(user==null)
            return null;
            if(!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt)){
                return null;
            }

            return user;
            
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
              using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
          {
             
              var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0; i<computedHash.Length;i++){
                    if(computedHash[i]!=passwordHash[i] ) return false;
                }
          } 
          return true;
        }

        public async Task<User> Register(User user, string password)
        {
           byte[] passowrdHash,passwordSalt;
           CreatePasswordHash(password,out passowrdHash,out passwordSalt);
           user.PasswordHash = passowrdHash;
           user.PasswordSalt = passwordSalt;
           await _Context.Users.AddAsync(user);
           await _Context.SaveChangesAsync();
           return user;

        }

        private void CreatePasswordHash(string password, out byte[] passowrdHash, out byte[] passwordSalt)
        {
           

          using (var hmac = new System.Security.Cryptography.HMACSHA512())
          {
              passwordSalt = hmac.Key;
              passowrdHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

          } 
        }

        public async Task<bool> UserExists(string username)
        {
           if(await _Context.Users.AnyAsync(x=>x.Username==username))
           return true;

           return false;
        }
    }
}