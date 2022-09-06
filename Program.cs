using System;

namespace designs
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyScanner scanner = new AssemblyScanner(typeof(Program).Assembly);
            TestCaseRunner<ITest>.Run(scanner.Matches());

            Console.WriteLine("Tests complete");
        }
    }
}
