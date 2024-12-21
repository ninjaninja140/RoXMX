using System;
using System.Threading;

namespace RoXMX.Utilities
{
    public class Process
    {
        public static void Exit(int exitCode)
        {
            Console.WriteLine();
            Logger.Info("Press ENTER to Close RoXMX.");
            Console.ReadLine();
            Logger.Info("Exiting...");
            Logger.Info("Thank you for using RoXMX!");
            Thread.Sleep(5000);
            Environment.Exit(exitCode);
        }
    }
}
