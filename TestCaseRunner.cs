using System;
using System.Collections.Generic;
using System.Text;

namespace designs
{
    public class TestCaseRunner<T>
            where T : ITest
    {
        public static void Run(IEnumerable<T> types)
        {
            foreach (var type in types)
            {
                Console.WriteLine("---------------------------------\n");
                type.Run();
                Console.WriteLine("---------------------------------\n");
            }
        }
    }
}
