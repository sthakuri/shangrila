namespace shangrila.Models
{
    public class ServiceHour
    {
        public int ID { get; set; }
        public string WeekDay { get; set; }
        public string StartTime { get; set; }
        public string CloseTime { get; set; }
        public bool Visible { get; set; }
    }
}