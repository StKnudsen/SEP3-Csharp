namespace SharedLibrary.Models
{
    public class RegisteredUser : User
    {
        public string Password { get; set; }
        public int UserId { get; set; }

        public RegisteredUser(string role)
        {
            Role = role;
        }

        public RegisteredUser()
        {
        }
    }
}