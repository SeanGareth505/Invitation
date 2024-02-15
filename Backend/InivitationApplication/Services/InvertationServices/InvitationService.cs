using InivitationApplication.DTOs;
using InivitationApplication.Models;
using InivitationApplication.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InivitationApplication.Services.InvertationServices
{
    public class InvitationService : IInvitationService
    {
        private readonly ApplicationDbContext _context;

        public InvitationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetAllInvitationsOutputDTO>> GetAllInvitations()
        {
            var allInvitations = _context.Invitations
                .Select(i => new GetAllInvitationsOutputDTO()
                {
                    Id = i.Id,
                    Email = i.Email,
                    Notes = i.Notes,
                    NumberOfPeople = i.NumberOfPeople,
                });

            return await allInvitations.ToListAsync();
        }
    }
}
