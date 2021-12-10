namespace SharedLibrary.Models.Restaurateur
{
    public class Restaurant
    {
        public int Id { get; }
        public int CVR { get; set; }
        public string Name { get; set; }
        public string Theme { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
        
        public Restaurant()
        {
        }
    }
}