#pragma warning disable CA1305
#pragma warning disable IDE0058

using System.Text;
internal sealed class CompositeAndChainOfResponsibility
{
    internal abstract class Shape
    {
        internal string? Fill { get; set; }
        internal string? Stroke { get; set; }
        internal int StrokeWidth { get; set; }

        public abstract string Draw ();

        public virtual void HandleRequest (int x, int y, RequestType requestType) { }
    }

    internal sealed class Circle : Shape
    {
        internal required int CX { get; set; }
        internal required int CY { get; set; }
        internal required int R { get; set; }

        public override string Draw ()
        {
            return $"\t\t<circle cx=\"{CX}\" cy=\"{CY}\" r=\"{R}\" fill=\"{Fill}\" stroke-width=\"{StrokeWidth}\" stroke=\"{Stroke}\"/>\n";
        }

        public override void HandleRequest (int x, int y, RequestType requestType)
        {
            if (IsPointInside(x, y))
            {
                HandleRequestCore(requestType);
            }
            else
            {
                base.HandleRequest(x, y, requestType);
            }
        }

        private bool IsPointInside (int x, int y)
        {
            int distance = ((x - CX) * (x - CX)) + ((y - CY) * (y - CY));

            return distance <= R * R;
        }

        private void HandleRequestCore (RequestType requestType)
        {
            switch (requestType)
            {
                case RequestType.Print:
                Console.WriteLine($"Printing circle with coordinates ({CX}, {CY}) and radius {R}");
                break;
                case RequestType.Help:
                Console.WriteLine($"Help for circle with coordinates ({CX}, {CY}) and radius {R}");
                break;
                default:
                throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null);
            }
        }
    }

    internal sealed class Rectangle : Shape
    {
        internal required int X { get; set; }
        internal required int Y { get; set; }
        internal required int Width { get; set; }
        internal required int Height { get; set; }

        public override string Draw ()
        {
            return $"\t\t<rect x=\"{X}\" y=\"{Y}\" width=\"{Width}\" height=\"{Height}\" fill=\"{Fill}\" stroke-width=\"{StrokeWidth}\" stroke=\"{Stroke}\"/>\n";
        }

        public override void HandleRequest (int x, int y, RequestType requestType)
        {
            if (IsPointInside(x, y))
            {
                HandleRequestCore(requestType);
            }
            else
            {
                base.HandleRequest(x, y, requestType);
            }
        }

        private bool IsPointInside (int x, int y)
        {
            return x >= X && x <= X + Width && y >= Y && y <= Y + Height;
        }

        private void HandleRequestCore (RequestType requestType)
        {
            switch (requestType)
            {
                case RequestType.Print:
                Console.WriteLine($"Printing rectangle with coordinates ({X}, {Y}), width {Width}, and height {Height}");
                break;
                case RequestType.Help:
                Console.WriteLine($"Help for rectangle with coordinates ({X}, {Y}), width {Width}, and height {Height}");
                break;
                default:
                throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null);
            }
        }
    }

    internal sealed class Polygon : Shape
    {
        internal required List<Point> Points { get; set; }

        public override string Draw ()
        {
            StringBuilder sb = new();

            sb.Append("\t\t<polygon points=\"");

            foreach (Point point in Points)
            {
                sb.Append($"{point.X},{point.Y} ");
            }

            sb.Append($"\" fill=\"{Fill}\" stroke-width=\"{StrokeWidth}\" stroke=\"{Stroke}\"/>\n");

            return sb.ToString();
        }

        public override void HandleRequest (int x, int y, RequestType requestType)
        {
            if (IsPointInside(x, y))
            {
                HandleRequestCore(requestType);
            }
            else
            {
                base.HandleRequest(x, y, requestType);
            }
        }

        private bool IsPointInside (int x, int y)
        {
            bool inside = false;
            int j = Points.Count - 1;

            for (int i = 0; i < Points.Count; i++)
            {
                if (Points [i].Y > y)
                {
                    if (Points [j].Y <= y)
                    {
                        if (Points [i].X < x && Points [j].X >= x)
                        {
                            inside = !inside;
                        }
                    }
                }
                else if (Points [j].Y > y)
                {
                    if (Points [i].X >= x)
                    {
                        inside = !inside;
                    }
                }

                j = i;
            }

            return inside;
        }

        private void HandleRequestCore (RequestType requestType)
        {
            switch (requestType)
            {
                case RequestType.Print:
                Console.WriteLine($"Printing polygon with points {string.Join(", ", Points)}");
                break;
                case RequestType.Help:
                Console.WriteLine($"Help for polygon with points {string.Join(", ", Points)}");
                break;
                default:
                throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null);
            }
        }
    }

    internal sealed class Point
    {
        internal int X { get; set; }
        internal int Y { get; set; }
    }

    internal abstract class Container : Shape
    {
        private protected List<Shape> Shapes { get; } = [];

        internal void Add (Shape shape)
        {
            Shapes.Add(shape);
        }

        internal void Remove (Shape shape)
        {
            Shapes.Remove(shape);
        }
    }

    internal sealed class CompositeShape : Container
    {
        public override string Draw ()
        {
            StringBuilder sb = new();

            foreach (Shape shape in Shapes)
            {
                sb.Append(shape.Draw());
            }

            return sb.ToString();
        }

        public override void HandleRequest (int x, int y, RequestType requestType)
        {
            for (int i = Shapes.Count - 1; i >= 0; i--)
            {
                Shape shape = Shapes [i];
                shape.HandleRequest(x, y, requestType);
            }
        }
    }

    internal enum RequestType
    {
        Print,
        Help
    }
    internal static void Execute ()
    {
        // Создание объектов геометрических фигур
        Circle circle1 = new()
        {
            CX = 205,
            CY = 205,
            R = 200,
            Fill = "green",
            StrokeWidth = 5,
            Stroke = "black"
        };

        Rectangle rectangle1 = new()
        {
            X = 55,
            Y = 70,
            Width = 300,
            Height = 200,
            Fill = "orange",
            StrokeWidth = 2,
            Stroke = "black"
        };

        Polygon polygon1 = new()
        {
            Points = [new Point() { X = 100, Y = 100 }, new Point() { X = 200, Y = 50 }, new Point() { X = 300, Y = 150 }],
            Fill = "yellow",
            StrokeWidth = 2,
            Stroke = "black"
        };

        Circle circle2 = new()
        {
            CX = 115,
            CY = 50,
            R = 100,
            Fill = "black",
            StrokeWidth = 5,
            Stroke = "black"
        };

        Rectangle rectangle2 = new()
        {
            X = 70,
            Y = 110,
            Width = 160,
            Height = 150,
            Fill = "grey",
            StrokeWidth = 2,
            Stroke = "black"
        };

        Polygon polygon2 = new()
        {
            Points = [new Point() { X = 100, Y = 100 }, new Point() { X = 200, Y = 50 }, new Point() { X = 300, Y = 150 }],
            Fill = "blue",
            StrokeWidth = 2,
            Stroke = "black"
        };

        // Создание составных контейнеров
        CompositeShape composite1 = new();
        composite1.Add(circle1);
        composite1.Add(rectangle1);
        composite1.Add(polygon1);

        CompositeShape composite2 = new();
        composite2.Add(circle2);
        composite2.Add(rectangle2);
        composite2.Add(polygon2);

        composite1.Add(composite2);

        StringBuilder htmlContent = new();
        htmlContent.AppendLine(@"<!DOCTYPE html>");
        htmlContent.AppendLine(@"<html>");
        htmlContent.AppendLine(@"<head>");
        htmlContent.AppendLine(@"    <title>SVG-графика</title>");
        htmlContent.AppendLine(@"</head>");
        htmlContent.AppendLine(@"<body>");
        htmlContent.AppendLine(@"    <svg width=""500"" height=""500"" version=""1.1"" id=""Layer_1"" xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" enable-background=""new 0 0 386 388"" xml:space=""preserve"">");
        htmlContent.AppendLine(composite1.Draw());
        htmlContent.AppendLine(@"    </svg>");
        htmlContent.AppendLine(@"</body>");
        htmlContent.AppendLine(@"</html>");

        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "output.html");

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        File.WriteAllText(filePath, htmlContent.ToString());

        Console.WriteLine($"Файл успешно создан: {filePath}");

        // Обработка запросов
        Console.WriteLine();
        composite1.HandleRequest(0, 0, RequestType.Print);
        Console.WriteLine();
        composite1.HandleRequest(200, 300, RequestType.Help);
        Console.WriteLine();
        composite1.HandleRequest(20, 20, RequestType.Print);
        Console.WriteLine();
        composite1.HandleRequest(10, 15, RequestType.Help);

        Console.ReadLine();
    }
}