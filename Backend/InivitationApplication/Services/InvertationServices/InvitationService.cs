using InivitationApplication.DTOs;
using InivitationApplication.Models;
using InivitationApplication.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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

        public async Task<GetAllInvitationsOutputDTO> GetAllInvitations(int skip, int take, string? firstName, string? lastName = null, bool? isAccepted = null)
        {
            // Build the query with filters applied as needed
            var query = _context.Invitations.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query = query.Where(i => EF.Functions.Like(i.FirstName, $"%{firstName}%"));
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(i => EF.Functions.Like(i.LastName, $"%{lastName}%"));
            }

            if (isAccepted.HasValue)
            {
                query = query.Where(i => i.IsAccepted == isAccepted.Value);
            }

            // Order the query
            query = query.OrderBy(i => i.Id);

            // Execute the query with pagination and project the results
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

            // Calculate the total number of records that match the filters
            var totalRecords = await query.CountAsync();

            // Construct and return the DTO
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
