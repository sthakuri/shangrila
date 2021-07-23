using System.Collections.Generic;

namespace shangrila.Models
{
    public class AdminViewModel
    {
        public Restaurant Restaurant { get; set; }
        public ServiceHour Sunday { get; set; }
        public ServiceHour Monday { get; set; }
        public ServiceHour Tuesday { get; set; }
        public ServiceHour Wednesday { get; set; }
        public ServiceHour Thursday { get; set; }
        public ServiceHour Friday { get; set; }
        public ServiceHour Saturday { get; set; }

        public List<Announcement> Announcements { get; set; }
        public Announcement Announcement { get; set; }

    }
}