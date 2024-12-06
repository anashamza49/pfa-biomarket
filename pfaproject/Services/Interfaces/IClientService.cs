using pfaproject.Models;
using System.Threading.Tasks;

namespace pfaproject.Services
{
    public interface IClientService
    {
        Task<bool> UpdateProfileAsync(ClientDTO clientProfileDto, string userId);
    }
}
