using System.Collections.Generic;
namespace shangrila.Models
{
    public class DishMenu
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public bool Visible { get; set; }
        public IEnumerable<Dish> Dishes {get; set;}
    }
}