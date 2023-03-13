using gamerszone.Data;
using gamerszone.Iservice;
using gamerszone.Utilities;
using MongoDB.Driver;
using System.Text;
using ZstdSharp.Unsafe;

namespace gamerszone.Services
{
    public class AppUserService:IAppUserService
    {
        private MongoClient? _mongoClient = null;
        private IMongoDatabase? _mongoDatabase = null;
        private IMongoCollection<AppUser>? _appUserTable = null;

        public AppUserService() 
        {
            _mongoClient = new MongoClient("mongodb://127.0.0.1:27017");
            _mongoDatabase = _mongoClient.GetDatabase("trdb");
            _appUserTable = _mongoDatabase.GetCollection<AppUser>("appusers");
        }

        public AppUser AppUserGetById(string id)
        {
           return _appUserTable.Find(au=>au.Id == id && au.active).FirstOrDefault(); 
        }

        public List<AppUser> AppUserList()
        {
            return _appUserTable.Find(FilterDefinition<AppUser>.Empty).ToList();   
        }

        public string DeleteAppUser(string id)
        {
            try
            {
                if (_appUserTable != null)
                {
                    _appUserTable.DeleteOne(au => au.Id == id);
                    return "Deleted";
                }

            }
            catch (SystemException ex)
            {
                throw ex;
            }
            return "Failed";
            
        }
        public async void Authenticate(string username, string password)
        {
            EncyptTool encyptTool = new EncyptTool();

            try
            {
                var encData = await encyptTool.EncryptAsync(password, username);
                string encString = BitConverter.ToString(encData);
                if (username != null || password != null)
                {
                    var appUser = _appUserTable.Find(x=>x.Email ==  username && x.Password == encString && x.active).FirstOrDefault();
                    if (appUser != null)
                    {
                        Console.WriteLine("Success");
                    }
                    else
                    {
                        Console.WriteLine("failed");
                    }
                }
                else
                {
                    Console.WriteLine("failed");
                }
            }
            catch (SystemException ex)
            {
                throw ex;
            }
        }

        public async void SaveOrUpdate(AppUser user)
        {
            /*
             * Need to perform password encryption here
             */
            string strPassPhrase = user.Email;

            EncyptTool enc = new EncyptTool();
            var encData = await enc.EncryptAsync(user.Password, strPassPhrase);
            if (encData != null)
            {
                user.Password = BitConverter.ToString(encData);
            }
            
            var appUserObj = _appUserTable.Find(x=>x.Id== user.Id && x.active).FirstOrDefault();
            if (appUserObj != null)
            {
                if(_appUserTable!= null)
                {
                    _appUserTable.ReplaceOne(au => au.Id == user.Id, user);
                }
            }
            else
            {
                if (_appUserTable != null) _appUserTable.InsertOne(user);
            }
        }

    }
}
