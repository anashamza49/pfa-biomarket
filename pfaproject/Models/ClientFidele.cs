using System.ComponentModel.DataAnnotations;

namespace pfaproject.Models
{
    public class ClientFidele
    {
        [Key]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Point { get; set; }
        public string NiveauFidelite { get; set; }
        public string DateCreation { get; set; }
        public string TypeOffre { get; set; }
    }
}
