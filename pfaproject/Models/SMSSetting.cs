namespace pfaproject.Models
{
    public class SMSSetting
    {
        public int Id { get; set; }
        public string AccountSID { get; set; }
        public string AuthToken { get; set; }
        public string PhoneNumber { get; set; }
    }
}
