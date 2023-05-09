
using gamerszone.Data;
using gamerszone.Iservices;
using gamerszone.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace gamerszone.Services
{
    public class AuthService : IAuthService
    {
        private MongoClient? _mongoClient = null;
        private IMongoDatabase? _mongoDatabase = null;
        private IMongoCollection<AppUser>? _appUserTable = null;

        
        

        public AuthService()
        {
            _mongoClient = new MongoClient("mongodb://127.0.0.1:27017");
            _mongoDatabase = _mongoClient.GetDatabase("trdb");
            _appUserTable = _mongoDatabase.GetCollection<AppUser>("appusers");
        }
        public async Task<LoggedInUser> Authenticate(string username, string password)
        
        {
            EncyptTool encyptTool = new EncyptTool();
            LoggedInUser loggedInUser = new();
            try
            {
                var encData = await encyptTool.EncryptAsync(password, username);
                string encString = BitConverter.ToString(encData);
              
                if (username != null || password != null)
                {
                    var appUser = _appUserTable.Find(x=>x.Email ==  username && x.Password == encString && x.active).FirstOrDefault();
                    if (appUser != null)
                    {
                        
                        loggedInUser.Email = appUser.Email;
                        loggedInUser.Id = appUser.Id;
                        loggedInUser.Role = appUser.Role;
                       
                        return loggedInUser;
                       // Console.WriteLine("Success");
                    }
                    else
                    {
                        return loggedInUser;
                      //  Console.WriteLine("failed");
                    }
                }
                else
                {
                    Console.WriteLine("failed");
                    return loggedInUser;
                }
            }
            catch (SystemException ex)
            {
                return loggedInUser;
                throw ex;

            }
        }

        public void Authorize(string username)
        {
            throw new NotImplementedException();
        }
    }
}