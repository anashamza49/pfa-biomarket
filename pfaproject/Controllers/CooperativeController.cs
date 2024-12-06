using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pfaproject.DTOs;
using pfaproject.Models;
using System.Security.Claims;

namespace pfaproject.Controllers
{
    [Authorize(Roles = "Cooperative")]
    [Route("api/[controller]")]
    [ApiController]
    public class CooperativeController : ControllerBase
    {
        private readonly ICooperativeService _cooperativeService;
        public CooperativeController(ICooperativeService cooperativeService)
        {
            _cooperativeService = cooperativeService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCooperative([FromForm] CooperativeDTO cooperativeDTO)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Utilisateur non autorisé.");
                }

                await _cooperativeService.AddCooperativeAsync(cooperativeDTO, userId);

                return Ok("Coopérative ajoutée avec succès !");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Une erreur s'est produite : {ex.Message}");
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCooperative([FromForm] CooperativeDTO cooperativeDTO)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Utilisateur non autorisé.");
                }

                await _cooperativeService.UpdateCooperativeAsync(cooperativeDTO, userId);

                return Ok("Coopérative mise à jour avec succès !");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Une erreur s'est produite : {ex.Message}");
            }
        }
        [HttpPost("add-magasin")]
        public async Task<IActionResult> AddMagasin([FromBody] MagasinDTO magasinDTO, string cooperativeUserId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Utilisateur non autorisé.");
                }

                var success = await _cooperativeService.AjouterMagasinAsync(magasinDTO,  cooperativeUserId); // Fournir l'UserId

                if (success)
                {
                    return Ok("Magasin ajouté avec succès !");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Erreur lors de l'ajout du magasin.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Une erreur s'est produite : {ex.Message}");
            }
        }


    }
}
