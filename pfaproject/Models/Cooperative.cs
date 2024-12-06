using pfaproject.Controllers;
using pfaproject.Models;
using System.ComponentModel.DataAnnotations;

public class Cooperative
{
    [Key]
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public string NomCooperative { get; set; }
    public string SecteurCooperative { get; set; }
    public string Province { get; set; }
    public string SiegeCooperative { get; set; }
    public string CertificatPath { get; set; }
    public bool IsValidated { get; set; }
    public IList<Magasin> Magasins { get; set; }
}
