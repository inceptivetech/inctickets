namespace gamerszone.Utilities
{
    public class UserSession
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
    }

    public class LoggedInUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
