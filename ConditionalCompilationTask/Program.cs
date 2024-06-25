namespace ConditionalCompilationTask;

internal class Program
{
    static void Main(string[] args)
    {
#if DEBUG

        #region DebugPart

        Console.WriteLine("Debug mode");

        #endregion
#else
        #region ReleasePart

        Console.WriteLine("Release mode");

        #endregion
#endif
        Console.ReadKey();
    }
}
