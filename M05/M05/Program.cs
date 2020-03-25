using System;
using NLog;
using StringConverterLib;

namespace M05
{
    class Program
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                Log.Info("Application started");
                Console.WriteLine("Print a string to convert to int32:");
                var str = Console.ReadLine();
                Console.WriteLine(str.ToInt32());
                Log.Info("Response was returned.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }
    }
}
