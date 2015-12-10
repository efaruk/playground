using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log4net.Appender.SplunkAppenders.DemoConsole
{
    class Program
    {
        static ILog logger = LoggerContainer.Logger;

        static void Main(string[] args)
        {

            //var data =
            //    "{\"Application\":\"Demo\",\"Machine\":\"BEVPC09\",\"Message\":\"Error: Home/Index:\r\n System.InvalidOperationException: Index action is not allowed for anonymous users...\r\n\",\"StackTrace\":\"log4net.Appender.SplunkAppenders.Demo.Controllers.HomeController.Index\",\"LogLevel\":\"ERROR\",\"Variables\":[{\"Key\":\"StackTrace\",\"Value\":\"log4net.Appender.SplunkAppenders.Demo.Controllers.HomeController.Index\"},{\"Key\":\"Exception\",\"Value\":\"System.InvalidOperationException: Index action is not allowed for anonymous users...\r\n\"},{\"Key\":\"StackTrace\",\"Value\":\"log4net.Appender.SplunkAppenders.Demo.Controllers.HomeController.Index\"},{\"Key\":\"Exception\",\"Value\":\"System.InvalidOperationException: Index action is not allowed for anonymous users...\r\n\"}]}";
            //SplunkContainer.Log(data, "localhost", "log4net", "log4net", "log4netpass");

            logger.Error("Demo Console Error", new InvalidOperationException("Program Executing not allowed..."));
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
