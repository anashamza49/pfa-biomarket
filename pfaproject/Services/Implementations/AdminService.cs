using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using pfaproject.Data;
using pfaproject.Models;
using pfaproject.Services.Interfaces;
using System.Security.Claims;

namespace pfaproject.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MyContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminService(UserManager<ApplicationUser> userManager, MyContext context, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<string>> GetClientsAsync()
        {
            // Vérifier l'authentification et l'autorisation ici
            var user = _httpContextAccessor.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            if (!user.IsInRole("Admin"))
            {
                throw new UnauthorizedAccessException("User is not authorized to access this resource.");
            }

            // Récupérer les clients
            var clients = await _userManager.GetUsersInRoleAsync("Client");
            return clients.Select(u => u.UserName);
        }

        public async Task<Cooperative> GetCooperativeByUserIdAsync(string userId)
        {
            // Vérifier l'authentification et l'autorisation ici
            var user = _httpContextAccessor.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            // Vérifier si l'utilisateur peut accéder aux informations de cette coopérative
            if (!user.IsInRole("Admin") && user.FindFirstValue(ClaimTypes.NameIdentifier) != userId)
            {
                throw new UnauthorizedAccessException("User is not authorized to access this resource.");
            }

            // Récupérer la coopérative
            return await _context.Cooperatives.FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<List<Cooperative>> GetUnvalidatedCooperativesAsync()
        {
            // Vérifier l'authentification et l'autorisation ici
            var user = _httpContextAccessor.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            if (!user.IsInRole("Admin"))
            {
                throw new UnauthorizedAccessException("User is not authorized to access this resource.");
            }

            // Récupérer les coopératives non validées
            return await _context.Cooperatives
                .Where(c => !c.IsValidated)
                .ToListAsync();
        }

        public async Task<bool> ValidateCooperativeAsync(string cooperativeName)
        {
            // Vérifier l'authentification et l'autorisation ici
            var user = _httpContextAccessor.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            if (!user.IsInRole("Admin"))
            {
                throw new UnauthorizedAccessException("User is not authorized to access this resource.");
            }

            var cooperative = await _context.Cooperatives.FirstOrDefaultAsync(c => c.NomCooperative == cooperativeName);
            if (cooperative == null)
            {
                return false;
            }

            cooperative.IsValidated = true;
            _context.Cooperatives.Update(cooperative);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<List<ClientDTO>> GetClientsWithDetailsAsync()
        {
            // Vérifier l'authentification et l'autorisation ici
            var user = _httpContextAccessor.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            if (!user.IsInRole("Admin"))
            {
                throw new UnauthorizedAccessException("User is not authorized to access this resource.");
            }

            // Récupérer les clients avec leurs données
            var clients = await _context.Clients.ToListAsync();

            // Convertir les clients en DTO
            var clientDTOs = clients.Select(c => new ClientDTO
            {
                Nom = c.Nom,
                CIN = c.CIN,
                DateNaissance = c.DateNaissance,
                Province = c.Province,
                Genre = c.Genre,
                Address = c.Address
            }).ToList();

            return clientDTOs;
        }

    }
}
