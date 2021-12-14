namespace SharedLibrary.Models.User
{
    public class GuestUser : User
    {
        public GuestUser()
        {
            Role = "Guest";
        }
    }
}