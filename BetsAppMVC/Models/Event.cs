namespace BetsAppMVC.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsLive { get; set; }
        public DateTime StartDate { get; set; }
        public Odd[] Odds { get; set; }
    }
}
