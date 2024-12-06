using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using pfaproject.Hubs;
using pfaproject.Models;
using pfaproject.Services.Interfaces;
using System.Security.Claims;

namespace pfaproject.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/controller")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("clients")]
        public async Task<IEnumerable<string>> GetClientsAsync()
        {
            return await _adminService.GetClientsAsync();
        }
        [HttpGet("clients/details")]
        public async Task<ActionResult<List<ClientDTO>>> GetClientsWithDetailsAsync()
        {
            var clients = await _adminService.GetClientsWithDetailsAsync();
            return Ok(clients);
        }
        [HttpGet("unvalidated-cooperatives")]
        public async Task<ActionResult<List<Cooperative>>> GetUnvalidatedCooperatives()
        {
            var cooperatives = await _adminService.GetUnvalidatedCooperativesAsync();
            return Ok(cooperatives);
        }

        // validation coopérative par son nom

        [HttpPost("validate-cooperative/{cooperativeName}")]
        public async Task<IActionResult> ValidateCooperative([FromRoute] string cooperativeName)
        {
            if (string.IsNullOrWhiteSpace(cooperativeName))
            {
                return BadRequest("Invalid cooperative name.");
            }

            var result = await _adminService.ValidateCooperativeAsync(cooperativeName);
            if (!result)
            {
                return NotFound("Cooperative not found");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User ID not found.");
            }

            var cooperative = await _adminService.GetCooperativeByUserIdAsync(userId);
            if (cooperative != null)
            {
                await _hubContext.Clients.User(userId).SendAsync("ReceiveMessage", $"Votre coopérative '{cooperative.NomCooperative}' a été validée.");
            }

            return Ok("Cooperative validated successfully");
        }


    }
}
