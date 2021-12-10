namespace SharedLibrary.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public int PostalCode { get; set; }
        public Address()
        {
        }
    }
}