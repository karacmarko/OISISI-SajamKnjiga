using System;
using System.Text;

namespace ConsoleClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            ConsoleView view = new ConsoleView();
            view.Run();
        }
    }
}