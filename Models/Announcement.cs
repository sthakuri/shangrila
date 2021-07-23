using System;

namespace shangrila.Models
{
    public class Announcement
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public bool Visible { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}