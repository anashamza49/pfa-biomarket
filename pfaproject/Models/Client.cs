using System.ComponentModel.DataAnnotations;

namespace pfaproject.Models
{
    public class Client
    {
        [Key]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Nom { get; set; }
        public string CIN { get; set; }
        public string DateNaissance { get; set; }
        public string Province { get; set; }
        public string Genre { get; set; }
        public string Address { get; set; }

    }
}
