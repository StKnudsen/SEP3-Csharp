namespace SharedLibrary.Models
{
    public class GuestUser : User
    {
        public GuestUser()
        {
            Role = "Guest";
        }
    }
}