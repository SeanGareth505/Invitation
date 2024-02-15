using Azure.Core;
using InivitationApplication.DTOs;
using InivitationApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InivitationApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationController : ControllerBase
    {
        private readonly IInvitationService _invitationService;

        public InvitationController(IInvitationService invitationService)
        {
            _invitationService = invitationService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllInvitations()
        {
            try
            {
                var invitations = await _invitationService.GetAllInvitations();
                return Ok(invitations);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an error response if necessary
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("test")]
        public async Task<IActionResult> test([FromBody] testDto randomText)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(randomText.RandomText))
                {
                    return BadRequest("Invalid request payload");
                }

                await _invitationService.AddRandomText(randomText.RandomText);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging
                // Consider returning a more specific error message depending on the exception
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
