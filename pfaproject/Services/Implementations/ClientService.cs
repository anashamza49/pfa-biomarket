using Microsoft.EntityFrameworkCore;
using pfaproject.Data;
using pfaproject.Models;
using System.Threading.Tasks;

namespace pfaproject.Services
{
    public class ClientService : IClientService
    {
        private readonly MyContext _context;

        public ClientService(MyContext context)
        {
            _context = context;
        }

        public async Task<bool> UpdateProfileAsync(ClientDTO clientProfileDto, string userId)
        {
            var existingClient = await _context.Clients.FirstOrDefaultAsync(c => c.UserId == userId);
            if (existingClient != null)
            {
                // Un profil client existe déjà pour cet utilisateur, renvoyer false
                return false;
            }

            // Aucun profil client trouvé, continuer avec l'ajout du profil
            var client = new Client { UserId = userId };
            client.Nom = clientProfileDto.Nom;
            client.CIN = clientProfileDto.CIN;
            client.DateNaissance = clientProfileDto.DateNaissance;
            client.Province = clientProfileDto.Province;
            client.Genre = clientProfileDto.Genre;
            client.Address = clientProfileDto.Address;

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
