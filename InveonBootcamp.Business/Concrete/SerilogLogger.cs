using InveonBootcamp.Business.Abstract;
using Serilog;

namespace InveonBootcamp.Business.Concrete
{
    public class SerilogLogger : ILoggerService
    {
        private readonly ILogger _logger;

        public SerilogLogger()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void Write(string message)
        {
            _logger.Information(message);
        }
    }
}
