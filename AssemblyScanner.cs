using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace designs
{
    public class AssemblyScanner
    {
        private Assembly Assembly { get; }

        public AssemblyScanner(Assembly assembly)
        {
            Assembly = assembly;
        }

        public IEnumerable<ITest> Matches()
        {
            foreach (var type in Assembly.GetTypes())
            {
                if (IsATestCase(type))
                {
                    var instance = Activator.CreateInstance(type);
                    yield return instance as ITest;
                }
            }
        }

        private bool IsATestCase(Type type) => type.IsDefined(typeof(TestCaseAttribute));
    }
}
