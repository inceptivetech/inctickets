

using gamerszone.Data;
using gamerszone.Utilities;

namespace gamerszone.Iservices
{
    public interface IAuthService
    {
       Task<LoggedInUser> Authenticate(string username, string password);

        void Authorize(string username);
    }

}