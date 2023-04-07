// bigdegen 2023

using MyApp.Util;

namespace MyApp; // Note: actual namespace depends on the project name.



internal class Program
{
    private static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("The output will be in the root folder of CaptainKey by default");

        var logger = new Capture(@"./log.txt");
        Console.ReadLine();
        logger.StopLogging();
    }
}