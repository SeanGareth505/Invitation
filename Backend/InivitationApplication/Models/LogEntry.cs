namespace InivitationApplication.Models
{
    public class LogEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string LogLevel { get; set; }
        public string Category { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        // Additional fields as needed
    }
}
