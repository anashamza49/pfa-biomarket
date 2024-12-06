using pfaproject.Models;

namespace pfaproject.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<string>> GetClientsAsync();
        Task<List<ClientDTO>> GetClientsWithDetailsAsync(); // Nouvelle méthode pour récupérer les clients avec leurs données
        Task<Cooperative> GetCooperativeByUserIdAsync(string userId);
        Task<List<Cooperative>> GetUnvalidatedCooperativesAsync();
        Task<bool> ValidateCooperativeAsync(string cooperativeId);
    }
}
