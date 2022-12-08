using System.ComponentModel.DataAnnotations.Schema;

namespace school_api.Model
{
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string StreetName { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public int Code { get; set; }
    }
}
