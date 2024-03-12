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

        public async Task<bool> CheckEmailExists(string email)
        {
            var checkExists = await _context.Invitations.FirstOrDefaultAsync(x => x.Email == email) != null;

            return checkExists;
        }

        public async Task<GetAllInvitationsOutputDTO> GetAllInvitations(int skip, int take)
        {
            var query = _context.Invitations.AsNoTracking().OrderBy(i => i.Id);

            var invitations = await query
                .Skip(skip)
                .Take(take)
                .Select(i => new Invitations
                {
                    Id = i.Id,
                    Email = i.Email,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    SongRequest = i.SongRequest,
                    IsAccepted = i.IsAccepted
                })
                .ToListAsync();

            var totalRecords = await query.CountAsync();

            return new GetAllInvitationsOutputDTO
            {
                Invitations = invitations,
                TotalRecords = totalRecords
            };
        }


        public async Task SubmitRSVP(SubmitRSVPInputDTO input)
        {
            var objectToSave = new InvitationModel()
            {
                Email = input.Email,
                FirstName = input.FirstName,
                LastName = input.LastName,
                SongRequest = input.SongRequest,
                IsAccepted = input.IsAccepted,
            };

            await _context.Invitations.AddAsync(objectToSave);
            await _context.SaveChangesAsync();
        }
    }
}
