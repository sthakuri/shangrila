using System.Collections.Generic;

namespace shangrila.Models
{
    public class HomeViewModel
    {
        public Restaurant Restaurant { get; set; }
        public List<DishMenu> Menus { get; set; }
        public List<ServiceHour> ServiceHours { get; set; }

    }
}