namespace school_api.Model
{
    public class ScheduleTable
    {
        public int Id { get; set; }
        public int Range { get; set; }
        public int Grade { get; set; } 
        public int Teacher { get; set; }
        public string Subject { get; set; } 
        public string DayTable { get; set; }
    }
}
