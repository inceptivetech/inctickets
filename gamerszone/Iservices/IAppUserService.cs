using gamerszone.Data;

namespace gamerszone.Iservices
{
    public interface IAppUserService
    {
        void SaveOrUpdate(AppUser user);
        AppUser AppUserGetById(string id);

        List<AppUser> AppUserList();

        string DeleteAppUser(string id);

        
    }
}
