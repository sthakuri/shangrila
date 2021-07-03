namespace shangrila.Models
{
    public class ServiceHour
    {
        public int ID { get; set; }
        public string WeekDay { get; set; }
        public string ServiceHours { get; set; }
        public bool IsOpen { get; set; }
    }
}