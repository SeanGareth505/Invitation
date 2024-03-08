using System;
using Microsoft.Extensions.Logging;

namespace InivitationApplication.Models
{
    public class DbLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly Func<LogEntry, bool> _filter;
        private readonly ApplicationDbContext _context;

        public DbLogger(string categoryName, Func<LogEntry, bool> filter, ApplicationDbContext context)
        {
            _categoryName = categoryName;
            _filter = filter;
            _context = context;
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => (_filter == null || _filter(new LogEntry { LogLevel = logLevel.ToString() }));

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var logEntry = new LogEntry
            {
                LogLevel = logLevel.ToString(),
                Category = _categoryName,
                Message = formatter(state, exception),
                StackTrace = exception?.StackTrace ?? string.Empty,
            };

            // Save log to database
            _context.LogEntries.Add(logEntry);
            _context.SaveChanges();
        }
    }

    public class DbLoggerProvider : ILoggerProvider
    {
        private readonly Func<LogEntry, bool> _filter;
        private readonly ApplicationDbContext _context;

        public DbLoggerProvider(Func<LogEntry, bool> filter, ApplicationDbContext context)
        {
            _filter = filter;
            _context = context;
        }

        public ILogger CreateLogger(string categoryName) => new DbLogger(categoryName, _filter, _context);

        public void Dispose() { }
    }
}
