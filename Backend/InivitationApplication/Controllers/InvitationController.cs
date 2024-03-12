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
        private readonly ILogger<InvitationController> _logger;

        public InvitationController(IInvitationService invitationService, ILogger<InvitationController> logger)
        {
            _invitationService = invitationService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllInvitations(int skip = 0, int take = 10, string? firstName = null, string? lastName = null, bool? isAccepted = null)
        {
            try
            {
                var invitations = await _invitationService.GetAllInvitations(skip, take, firstName, lastName, isAccepted);
                return Ok(invitations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all invitations.");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("CheckEmailExists/{email}")]
        public async Task<IActionResult> CheckEmailExists(string email)
        {
            try
            {
                var exists = await _invitationService.CheckEmailExists(email.Trim());
                return Ok(exists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking if email exists: {Email}", email);
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
                _logger.LogError(ex, "An error occurred while submitting RSVP for {Email}", input.Email);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
