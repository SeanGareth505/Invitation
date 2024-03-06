namespace InivitationApplication.DTOs
{
    public class SubmitRSVPInputDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? SongRequest { get; set; }
        public bool IsAccepted { get; set; }
    }
}
