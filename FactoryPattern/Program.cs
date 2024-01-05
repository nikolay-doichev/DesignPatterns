namespace FactoryPattern;

public class Point
{
    public static class Factory
    {
        //factory method
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }

    public static Point Origin => new Point(0, 0);

    public static Point Origin2 = new Point(0, 0); //better

    private double x, y;

    private Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        var point = Point.Factory.NewPolarPoint(1.0, Math.PI / 2);
        Console.WriteLine(point);
    }
}

