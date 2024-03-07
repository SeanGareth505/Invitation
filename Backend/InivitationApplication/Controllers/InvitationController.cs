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

        [HttpGet("CheckEmailExists/{email}")]
        public async Task<IActionResult> CheckEmailExists(string email)
        {
            try
            {
                var invitations = await _invitationService.CheckEmailExists(email.Trim());
                return Ok(invitations);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an error response if necessary
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("SubmitRSVP")]
        public async Task<IActionResult> SubmitRSVP([FromBody] SubmitRSVPInputDTO input)
        {
            try
            {
                await _invitationService.SubmitRSVP(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
