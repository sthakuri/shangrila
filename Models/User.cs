using System;
namespace shangrila.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public bool IsLocked { get; set; }
        public DateTime LastLoggedIn { get; set; }
    }
}