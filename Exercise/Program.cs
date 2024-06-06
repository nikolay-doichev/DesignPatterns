namespace Exercise
{
    public class Program
    {
        public class Point
        {
            public int X, Y;

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public class Line
        {
            public Point Start, End;

            public Line()
            {
                
            }

            public Line(Point pointStart, Point pointEnd)
            {
                Start = pointStart;
                End = pointEnd;
            }

            public Line DeepCopy()
            {
                return (Line)MemberwiseClone();
            }

            public override string ToString()
            {
                return $"{nameof(Start)}: x:{Start.X}, y:{Start.Y}; {nameof(End)}: x:{End.X}, y:{End.Y}";
            }
        }
        static void Main(string[] args)
        {
            var startPoint = new Point(1,2);
            var enPoint = new Point(9,8);
            var line1 = new Line(startPoint, enPoint);
            var line2 = line1.DeepCopy();
            line2.Start = new Point(3, 4);
            Console.WriteLine("Line 1: " + line1);
            Console.WriteLine("Line 2: " + line2);
        }
    }
}
