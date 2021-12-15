namespace SharedLibrary.Models.Restaurateur
{
    public class Dish
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}