using gamerszone.Data;

namespace gamerszone.Iservice
{
    public interface IAppUserService
    {
        void SaveOrUpdate(AppUser user);
        AppUser AppUserGetById(string id);

        List<AppUser> AppUserList();

        string DeleteAppUser(string id);

        void Authenticate(string username, string password);
    }
}
