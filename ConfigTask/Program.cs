using System.Configuration;

namespace ConfigTask;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(ConfigurationManager.AppSettings["url"]);
    }
}
