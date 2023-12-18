namespace Open_ClosedPrinciple
{
    public class Program
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
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }
                Name = name;
                Color = color;
                Size = size;
            }
        }

        public class ProductFilter
        {
            public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size) 
            {
                foreach (Product p in products)
                {
                    if(p.Size == size) yield return p;
                }
            }

            public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
            {
                foreach (Product p in products)
                {
                    if (p.Color == color) yield return p;
                }
            }

            public IEnumerable<Product> FilterBySizeandColor(IEnumerable<Product> products, Color color, Size size)
            {
                foreach (Product p in products)
                {
                    if (p.Color == color && p.Size == size) yield return p;
                }
            }
        }

        public interface ISpecification<T>
        {
            bool IsSatisfied(T t);
        }

        public interface IFilter<T>
        {
            IEnumerable<T> Filter(IEnumerable<T> t, ISpecification<T> spec);
        }

        public class ColorSpecification : ISpecification<Product>
        {
            private Color Color;

            public ColorSpecification(Color color)
            {
                Color = color;
            }

            public bool IsSatisfied(Product t)
            {
                return t.Color == Color;
            }
        }

        public class SizeSpecification : ISpecification<Product>
        {
            private Size size;
            public SizeSpecification(Size size)
            {
                this.size = size;
            }
            public bool IsSatisfied(Product t)
            {
                return t.Size == size;
            }
        }

        public class AddSpecification<T> : ISpecification<T>
        {
            ISpecification<T> first, second;

            public AddSpecification(ISpecification<T> first, ISpecification<T> second)
            {
                this.first = first ?? throw new ArgumentNullException(nameof(first));
                this.second = second ?? throw new ArgumentNullException(nameof(second));
            }

            public bool IsSatisfied(T t)
            {
                return first.IsSatisfied(t) && second.IsSatisfied(t);
            }
        }

        public class BetterFilter : IFilter<Product>
        {
            public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
            {
                foreach (var item in items)
                {
                    if(spec.IsSatisfied(item)) yield return item;
                }
            }
        }

        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = new Product[] { apple, tree, house };

            var pf = new ProductFilter();
            Console.WriteLine("Green products (old).");
            foreach (var p in pf.FilterByColor(products, Color.Green))
            {
                Console.WriteLine($" - {p.Name} is green.");
            }

            var bf = new BetterFilter();
            Console.WriteLine("Green products (new)");
            foreach (var item in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($" - {item.Name} is green.");
            }

            Console.WriteLine("Large blue items");

            foreach (var p in bf.Filter(products,
                new AddSpecification<Product>(
                    new ColorSpecification(Color.Blue),
                    new SizeSpecification(Size.Large))))
            {
                Console.WriteLine($" - {p.Name} is big and blue.");
            }
        }
    }
}