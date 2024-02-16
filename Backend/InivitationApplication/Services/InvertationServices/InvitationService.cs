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
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    SongRequest = i.SongRequest,
                    IsAccepted = i.IsAccepted,
                });

            return await allInvitations.ToListAsync();
        }

        public async Task AddRandomText(string text)
        {
            var inv = new InvitationModel()
            {
                Id = Guid.NewGuid(),
                Email = text,
                FirstName = text,
                LastName = text,
                SongRequest = text,
                IsAccepted = true,
            };

            _context.Invitations.Add(inv);
            _context.SaveChanges();
        }
    }
}
