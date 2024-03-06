using InivitationApplication.DTOs;
using InivitationApplication.Models;

namespace InivitationApplication.Services.Interfaces
{
    public interface IInvitationService
    {
        Task<IEnumerable<GetAllInvitationsOutputDTO>> GetAllInvitations();
        Task SubmitRSVP(SubmitRSVPInputDTO input);
    }
}
