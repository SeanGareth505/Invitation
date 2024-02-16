namespace InivitationApplication.Models
{
    public class InvitationModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? SongRequest { get; set; }
        public bool IsAccepted { get; set; }
    }
}
