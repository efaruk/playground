using System;
using System.Threading;
using System.Threading.Tasks;

namespace WhoSummonedMe.Konsole
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Argument Count {0}", args.Length);
            Parallel.For(0, 1000, i =>
            {
                using (var caller = new Caller())
                {
                    caller.Call();
                }
                Console.WriteLine("Parallel Caller Count {0} at Operation {1}", Summoner.LiveCallerCount(), i);
            });
            for (var i = 0; i < 10; i++)
            {
                using (var caller = new Caller())
                {
                    caller.Call();
                }
                Thread.Sleep(100);
                Console.WriteLine("Caller Count {0} at Operation {1}", Summoner.LiveCallerCount(), i);
            }
            Thread.Sleep(5000);
            Console.WriteLine("Caller Count {0}", Summoner.LiveCallerCount());
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
