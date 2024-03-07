using InivitationApplication.DTOs;
using InivitationApplication.Models;

namespace InivitationApplication.Services.Interfaces
{
    public interface IInvitationService
    {
        Task<bool> CheckEmailExists(string email);
        Task<IEnumerable<GetAllInvitationsOutputDTO>> GetAllInvitations();
        Task SubmitRSVP(SubmitRSVPInputDTO input);
    }
}
