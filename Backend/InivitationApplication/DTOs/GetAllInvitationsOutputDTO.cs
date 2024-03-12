using System.ComponentModel.DataAnnotations;

namespace InivitationApplication.DTOs
{
    public class Invitations
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? SongRequest { get; set; }
        public bool IsAccepted { get; set; }
    }

    public class GetAllInvitationsOutputDTO
    {
        public List<Invitations>? Invitations { get; set; }
        public int TotalRecords { get; set; }
    }
}
