#pragma warning disable CS8618
#pragma warning disable CA1305
#pragma warning disable IDE0058

using System.Text;
internal sealed class Composite
{
	internal abstract class Shape
	{
		public string Fill
		{
			get; set;
		}
		public string Stroke
		{
			get; set;
		}
		public int StrokeWidth
		{
			get; set;
		}

		public abstract string Draw ();
	}

	internal sealed class Circle : Shape
	{
		public required int CX
		{
			get; set;
		}
		public required int CY
		{
			get; set;
		}
		public required int R
		{
			get; set;
		}

		public override string Draw ()
		{
			return $"\t\t<circle cx=\"{CX}\" cy=\"{CY}\" r=\"{R}\" fill=\"{Fill}\" stroke-width=\"{StrokeWidth}\" stroke=\"{Stroke}\"/>\n";
		}
	}

	internal sealed class Rectangle : Shape
	{
		public required int X
		{
			get; set;
		}
		public required int Y
		{
			get; set;
		}
		public required int Width
		{
			get; set;
		}
		public required int Height
		{
			get; set;
		}

		public override string Draw ()
		{
			return $"\t\t<rect x=\"{X}\" y=\"{Y}\" width=\"{Width}\" height=\"{Height}\" fill=\"{Fill}\" stroke-width=\"{StrokeWidth}\" stroke=\"{Stroke}\"/>\n";
		}
	}

	internal sealed class Polygon : Shape
	{
		public required List<Point> Points
		{
			get; set;
		}

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
	}

	internal sealed class Point
	{
		public int X
		{
			get; set;
		}
		public int Y
		{
			get; set;
		}
	}

	internal abstract class Container : Shape
	{
		protected List<Shape> Shapes { get; } = [];

		public void Add (Shape shape)
		{
			Shapes.Add(shape);
		}

		public void Remove (Shape shape)
		{
			Shapes.Remove(shape);
		}
	}

	internal sealed class CompositeShape : Container
	{
		// Класс для представления составных контейнеров фигур

		public override string Draw ()
		{
			StringBuilder sb = new();

			foreach (Shape shape in Shapes)
			{
				sb.Append(shape.Draw());
			}

			return sb.ToString();
		}
	}
	public static void Execute ()
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

		Circle circle3 = new()
		{
			CX = 115,
			CY = 50,
			R = 70,
			Fill = "white",
			StrokeWidth = 5,
			Stroke = "black"
		};

		Rectangle rectangle3 = new()
		{
			X = 40,
			Y = 70,
			Width = 100,
			Height = 120,
			Fill = "white",
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

		CompositeShape composite3 = new();
		composite3.Add(circle3);
		composite3.Add(rectangle3);

		composite1.Add(composite2);
		composite2.Add(composite3);

		// Создание SVG-файла
		string svgContent = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<!DOCTYPE svg PUBLIC ""-//W3C//DTD SVG 1.1//EN"" ""http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd"">
<html>
<head>
    <title>SVG-графика</title>
</head>
<body>
    <svg width=""500"" height=""500"" version=""1.1"" id=""Layer_1"" xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" enable-background=""new 0 0 386 388"" xml:space=""preserve"">
{composite1.Draw()}    </svg>
</body>
</html>";

		// Сохранение SVG-файла
		string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "output.svg");

		if (File.Exists(filePath))
		{
			File.Delete(filePath);
		}

		File.WriteAllText(filePath, svgContent);

		Console.WriteLine($"SVG-файл успешно создан: {filePath}");
		Console.ReadLine();
	}
}