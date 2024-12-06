using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pfaproject.DTOs;
using pfaproject.Services.Interfaces;

namespace pfaproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase
    {
        private readonly ISMSService _SMSService;

        public SMSController(ISMSService sMSService)
        {
            this._SMSService = sMSService;
        }
        [HttpPost]
        [ActionName("Send")]
        public async Task<IActionResult> SendAsync(MessageRessourceDTO model)
        {
            try
            {
                var result = await _SMSService.SendAsynch(model.Message, model.To);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
