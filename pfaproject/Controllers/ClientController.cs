using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pfaproject.Models;
using pfaproject.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace pfaproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Client")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] ClientDTO clientProfileDto)
        {
            // Obtenir l'ID utilisateur à partir des revendications du jeton JWT
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID not found in token claims");
            }

            var result = await _clientService.UpdateProfileAsync(clientProfileDto, userId);
            if (!result)
            {
                return BadRequest("Failed to update profile");
            }
            return Ok("Profile updated successfully");
        }
    }
}
