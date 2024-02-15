namespace InivitationApplication.Models
{
    public class InvitationModel
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public int NumberOfPeople { get; set; }
        public string? Notes { get; set; }
    }
}
