namespace SharedLibrary.Models.User
{
    public class RegisteredUser : User
    {
        public string Password { get; set; }
        public int UserId { get; set; }

        public RegisteredUser() { }

        public RegisteredUser(string role)
        {
            Role = role;
        }
    }
}