using InivitationApplication.DTOs;
using InivitationApplication.Models;

namespace InivitationApplication.Services.Interfaces
{
    public interface IInvitationService
    {
        Task<bool> CheckEmailExists(string email);
        Task SubmitRSVP(SubmitRSVPInputDTO input);
        Task<GetAllInvitationsOutputDTO> GetAllInvitations(int skip, int take);
    }
}
