namespace school_api.Model
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int TeacherId { get; set; }
        public int Email { get; set; } 
        public string PhoneNumber { get; set; }  
        public int Address { get; set; }
    }
}
