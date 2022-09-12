using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace designs.Structural
{
    public class Neuron : IEnumerable<Neuron>
    {
        public Neuron(string name)
        {
            this.name = name;
        }
        private string name;
        public List<Neuron> In { get; } = new List<Neuron>();
        public List<Neuron> Out { get; } = new List<Neuron>();
        public IEnumerator<Neuron> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    [TestCase]
    public class NeuronTest : ITest
    {
        public void Run()
        {
            Neuron a = new Neuron("a");
            a.In.Add(new Neuron("b"));
            a.In.Add(new Neuron("c"));
            foreach (var item in a)
            {

            }
        }
    }
}
