using Microsoft.AspNetCore.Identity;

namespace school_api.Model
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Identification { get; set; } 
    }
}
