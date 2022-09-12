using System;
using System.Collections.Generic;
using System.Text;

namespace designs.Creation.Builder
{
    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int indentSize = 2;
        public HtmlElement() { }
        public HtmlElement(string name, string text)
        {
            Name = name;
            Text = text;
        }

        public static HtmlBuilder Create(string name, string text) => new HtmlBuilder(name);
    }
    public class HtmlBuilder
    {
        protected readonly string rootName;

        public HtmlBuilder(string rootName)
        {
            this.rootName = rootName;
            root = new HtmlElement(rootName, "");
        }

        protected HtmlElement root;


        public static implicit operator HtmlElement(HtmlBuilder builder)
        {
            return builder.root;
        }

        public override string ToString() => root.ToString();

        public HtmlElement Build() => root;

        public HtmlBuilder AddChild(string name, string text)
        {
            root.Elements.Add(HtmlElement.Create(name, text));
            return this;
        }
    }

    [TestCase]
    public class BuilderPattern : ITest
    {
        public void Run()
        {
            var builder = new HtmlBuilder("ul");
            var result = builder
                .AddChild("li", "hello")
                .AddChild("li", "world")
                .Build();
            Console.WriteLine(result.ToString());
        }
    }
}
