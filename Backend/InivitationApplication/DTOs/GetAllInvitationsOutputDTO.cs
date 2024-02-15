using System.ComponentModel.DataAnnotations;

namespace InivitationApplication.DTOs
{
    public class GetAllInvitationsOutputDTO
    {
        public Guid Id { get; set; }
        [MinLength(1)]
        public string? Email { get; set; }
        public int NumberOfPeople { get; set; }
        public string? Notes { get; set; }
    }
}
