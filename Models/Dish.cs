namespace shangrila.Models
{
    public class Dish
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Visible { get; set; }
    }
}