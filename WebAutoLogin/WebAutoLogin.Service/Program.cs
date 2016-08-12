using System;
using Mono.Unix;
using Mono.Unix.Native;
using Nancy.Hosting.Self;
using WebAutoLogin.Client;
using WebAutoLogin.Configuration;

namespace WebAutoLogin.Service
{
    internal class Program
    {
        // Resource: https://github.com/NancyFx/Nancy/wiki/Hosting-Nancy-with-Nginx-on-Ubuntu
        // ReSharper disable once UnusedParameter.Local maybe later.
        private static void Main(string[] args)
        {
            var settingsProvider = new AppConfigSettingsProvider();
            var uri = settingsProvider.GetAppSetting(GlobalModule.SettingBaseAddress);
            Console.WriteLine("Starting Nancy Host on " + uri);
            var configuration = new HostConfiguration
            {
                UrlReservations = new UrlReservations {CreateAutomatically = true}
            };
            // initialize an instance of NancyHost
            var host = new NancyHost(new Uri(uri), new Bootstrapper(), configuration);
            host.Start();  // start hosting

            // check if we're running on mono
            if (Type.GetType("Mono.Runtime") != null)
            {
                Console.WriteLine("Running on Mono");
                var p = (int)Environment.OSVersion.Platform;
                if ((p == 4) || (p == 6) || (p == 128))
                {
                    Console.WriteLine("Running on Unix Like System");
                    WaitForTerminationOnUnix();
                }
                else
                {
                    Console.WriteLine("Running on Windows");
                    WaitForTerminationOnWindows();
                }
            }
            else
            {
                Console.WriteLine("Running on .Net");
                WaitForTerminationOnWindows();
            }

            Console.WriteLine("Stopping Nancy Host");
            host.Stop();  // stop hosting
        }

        private static void WaitForTerminationOnWindows()
        {
            // Little modification for process control
            var t = string.Empty;
            while (!t.Equals("q", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("To stop process press Q and accept.");
                t = Console.ReadLine() ?? string.Empty;
            }
        }

        private static void WaitForTerminationOnUnix()
        {
            Console.WriteLine("To stop process send a termination signal.");
            // on mono, processes will usually run as daemons - this allows you to listen
            // for termination signals (ctrl+c, shutdown, etc) and finalize correctly
            UnixSignal.WaitAny(new[] {
                    new UnixSignal(Signum.SIGINT),
                    new UnixSignal(Signum.SIGTERM),
                    new UnixSignal(Signum.SIGQUIT),
                    new UnixSignal(Signum.SIGHUP)
                });
        }
    }
}