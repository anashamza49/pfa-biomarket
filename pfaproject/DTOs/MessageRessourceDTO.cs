using System.ComponentModel.DataAnnotations;

namespace pfaproject.DTOs
{
    public class MessageRessourceDTO
    {
        [MaxLength(200)]
        [Required]
        public string Message { get; set; }
        [MaxLength]
        public string To { get; set; }
    }
}
