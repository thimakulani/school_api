namespace school_api.Model
{
    public class Student
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string StudentNumber { get; set; }
        public string Identification { get; set; } 
        public Address Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Parent Parent { get; set; }  
        public Grade Grade { get; set; }

    }
}
