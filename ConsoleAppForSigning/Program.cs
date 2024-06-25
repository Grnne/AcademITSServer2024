using System;
using DllForSigning;

namespace ConsoleAppForSigning
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var test = new APlusB();

            Console.WriteLine(test.GetSum());
            Console.ReadKey();
        }
    }
}
