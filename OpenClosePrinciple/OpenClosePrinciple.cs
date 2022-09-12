using System;
using System.Collections.Generic;
using System.Text;

namespace designs
{
    public enum Color
    {
        Red, Green, Blue
    }
    public enum Size
    {
        Small, Medium, Large, Yuge
    }
    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;
        public Product(string name, Color color, Size size)
        {
            Name = name;
            Color = color;
            Size = size;
        }
    }


    /// <summary>
    /// This should be open for extension and not modification.
    /// </summary>
    public class ProductFilter
    {
        public IEnumerable<Product> FilterByColor
        (IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
                if (p.Color == color) // && p.Size == size
                    yield return p;
        }
    }

    public interface ISpecification<T>
    {
        bool IsSatisfied(T item);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
                if (spec.IsSatisfied(i))
                    yield return i;
        }
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private readonly Color color;
        public ColorSpecification(Color color)
        {
            this.color = color;
        }
        public bool IsSatisfied(Product p)
        {
            return p.Color == color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size Size { get; }

        public SizeSpecification(Size size)
        {
            Size = size;
        }

        public bool IsSatisfied(Product item) => Size == item.Size;
    }
    public class AndSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> first, second;
        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            this.first = first;
            this.second = second;
        }
        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

    [TestCase]
    public class OpenClosePrinciple : ITest
    {
        public void Run()
        {
            List<Product> products = new List<Product>(){ new Product("Product A", Color.Green, Size.Large),
            new Product("Product B", Color.Red, Size.Small),
            new Product("Product C", Color.Green, Size.Large)
        };
            BetterFilter filter = new BetterFilter();
            foreach (var p in filter.Filter(products, new AndSpecification<Product>(new ColorSpecification(Color.Green), new SizeSpecification(Size.Large))))
            {
                Console.WriteLine($"{p.Name} is large and green");
            }
        }
    }
}
