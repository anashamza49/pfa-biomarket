using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pfaproject.Models
{
    public class Magasin
    {
        public string Id { get; set; } 
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string UserId { get; set; }
        public Cooperative Cooperative { get; set; }
    }
}
