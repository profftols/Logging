using System;
using System.IO;

namespace Logging
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Pathfinder logFile = new Pathfinder(new FileLogWritter());
            Pathfinder logConsole = new Pathfinder(new ConsoleLogWritter());
            Pathfinder logFileFriday = new Pathfinder(new SecureConsoleLogWritter(new FileLogWritter()));
            Pathfinder logConsoleFriday = new Pathfinder(new SecureConsoleLogWritter(new ConsoleLogWritter()));
            Pathfinder logAll = new Pathfinder(new ConsoleLogWritter(new SecureConsoleLogWritter(new FileLogWritter())));

            logFile.Find("Hello");
            logConsole.Find("Hello");
            logFileFriday.Find("Hello");
            logConsoleFriday.Find("Hello");
            logAll.Find("Hello");
        }
    }
    
    public class ConsoleLogWritter : ILogger
    {
        private readonly ILogger _logger;

        public ConsoleLogWritter()
        {
            
        }
        
        public ConsoleLogWritter(ILogger logger)
        {
            _logger = logger;
        }

        public void Find(string message)
        {
            Console.WriteLine(message);

            if (_logger != null)
            {
                _logger.Find(message);
            }
        }
    }
 
    public class FileLogWritter : ILogger
    {
        public void Find(string message)
        {
            File.WriteAllText("log.txt", message);
        }
    }
 
    public class SecureConsoleLogWritter : ILogger
    {
        private readonly ILogger _logger;
        
        public SecureConsoleLogWritter(ILogger logger)
        {
            _logger = logger;
        }

        public void Find(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                _logger.Find(message);
                Console.WriteLine("Sec");
            }
        }
    }

    public class Pathfinder : ILogger
    {
        private readonly ILogger _logger;
        
        public Pathfinder(ILogger logger)
        {
            _logger = logger;
        }

        public void Find(string message)
        {
            _logger.Find(message);
        }
    }

    public interface ILogger
    {
        void Find(string message);
    }
}