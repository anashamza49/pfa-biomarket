namespace pfaproject.Models
{
    public class CooperativeDTO
    {
        public string NomCooperative { get; set; }
        public string SecteurCooperative { get; set; }
        public string Province { get; set; }
        public string SiegeCooperative { get; set; }
        public IFormFile Certificat { get; set; }
        public string CertificatPath { get; set; }
    }
}
