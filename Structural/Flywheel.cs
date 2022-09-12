using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace designs.Structural
{
    [TestCase]
    public class Flywheel : ITest
    {
        public void Run()
        {
            //var summary = BenchmarkRunner.Run<Flywheel>();
        }

        [Benchmark]
        public void TestReguarUser()
        {
            var user = new RegularUser();
            user.SetName(new Faker().Random.Words(2));
            var fn = user.FullName;
        }
        [Benchmark]
        public void TestLiteUser()
        {
            var user = new LiteUser();
            user.SetName(new Faker().Random.Words(2));
            var fn = user.FullName;
        }
        public class RegularUser
        {
            public string FullName { get; set; }
            public void SetName(string userName)
            {
                FullName = userName;
            }
        }
        public class LiteUser
        {
            private static List<string> strings = new List<string>();
            private int[] names;
            public void SetName(string fullName)
            { 
                int getOrAdd(string s)
                {
                    int idx = strings.IndexOf(s);
                    if (idx != -1) return idx;
                    else
                    {
                        strings.Add(s);
                        return strings.Count - 1;
                    }
                }
                names = fullName.Split(' ').Select(getOrAdd).ToArray();
            }
            public string FullName => string.Join(" ", names.Select(i => strings[i]));
        }
    }

}
