#pragma warning disable CA1303
#pragma warning disable CA1305
#pragma warning disable CA1310
#pragma warning disable CA5394
#pragma warning disable CS8618
#pragma warning disable IDE0290
#pragma warning disable IDE0058
#pragma warning disable IDE0059

using System.Text;

internal sealed class Decorator
{
	internal interface IView
	{
		void UpdateView (object? sender, EventArgs args);
	}

	internal sealed class TableView : IView
	{
		private static string GenerateHTML (Dictionary<int, char> data)
		{
			StringBuilder sb = new();
			sb.AppendLine("<!DOCTYPE html>");
			sb.AppendLine("<html lang=\"ru\">");
			sb.AppendLine("<head>");
			sb.AppendLine("<meta charset=\"UTF-8\">");
			sb.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
			sb.AppendLine("<style>");
			sb.AppendLine("table {");
			sb.AppendLine("    border-collapse: collapse;");
			sb.AppendLine("    width: 300px; /* Ширина таблицы */");
			sb.AppendLine("}");
			sb.AppendLine("td {");
			sb.AppendLine("    border: 1px solid black; /* Граница ячеек */");
			sb.AppendLine("    padding: 8px; /* Внутренний отступ ячеек */");
			sb.AppendLine("    text-align: center; /* Выравнивание текста по центру в ячейках */");
			sb.AppendLine("}");
			sb.AppendLine("canvas {");
			sb.AppendLine("    border: 1px solid black; /* Граница для элемента canvas */");
			sb.AppendLine("    margin-top: 10px; /* Отступ сверху для гистограммы */");
			sb.AppendLine("}");
			sb.AppendLine("</style>");
			sb.AppendLine("</head>");
			sb.AppendLine("<body>");
			sb.AppendLine("<table id=\"Table\">");

			// Добавляем первую строку с названиями
			sb.AppendLine("<tr>");
			foreach (KeyValuePair<int, char> pair in data)
			{
				sb.AppendLine($"<td>{pair.Value}</td>");
			}

			sb.AppendLine("</tr>");

			// Добавляем вторую строку с данными
			sb.AppendLine("<tr>");
			foreach (KeyValuePair<int, char> pair in data)
			{
				sb.AppendLine($"<td>{pair.Key}</td>");
			}

			sb.AppendLine("</tr>");

			sb.AppendLine("</table>");

			// Генерация гистограммы
			sb.AppendLine("<script src=\"https://cdn.jsdelivr.net/npm/chart.js\"></script>");
			sb.AppendLine("<canvas id=\"myChart\" width=\"400\" height=\"200\"></canvas>");
			sb.AppendLine("<script>");
			sb.AppendLine("var ctx = document.getElementById('myChart').getContext('2d');");
			sb.AppendLine("var myChart = new Chart(ctx, {");
			sb.AppendLine("    type: 'bar',");
			sb.AppendLine("    data: {");
			sb.AppendLine("        labels: [" + string.Join(",", data.Select(pair => $"'{pair.Value}'")) + "],");
			sb.AppendLine("        datasets: [{");
			sb.AppendLine("            label: 'Values',");
			sb.AppendLine("            data: [" + string.Join(",", data.Select(pair => pair.Key)) + "],");
			sb.AppendLine("            backgroundColor: [");

			// Генерация цветовой палитры для каждого символа
			for (int i = 0; i < data.Count; i++)
			{
				int red = i * 70 % 255; // Генерация значения красного цвета
				int green = i * 130 % 255; // Генерация значения зеленого цвета
				int blue = i * 200 % 255; // Генерация значения синего цвета
				sb.AppendLine($"                'rgba({red}, {green}, {blue}, 0.2)',");
			}

			sb.AppendLine("            ],");
			sb.AppendLine("            borderColor: [");

			// Генерация цветов границ для каждого символа
			for (int i = 0; i < data.Count; i++)
			{
				int red = i * 70 % 255; // Генерация значения красного цвета
				int green = i * 130 % 255; // Генерация значения зеленого цвета
				int blue = i * 200 % 255; // Генерация значения синего цвета
				sb.AppendLine($"                'rgba({red}, {green}, {blue}, 1)',");
			}

			sb.AppendLine("            ],");
			sb.AppendLine("            borderWidth: 2");
			sb.AppendLine("        }]");
			sb.AppendLine("    },");
			sb.AppendLine("    options: {");
			sb.AppendLine("        scales: {");
			sb.AppendLine("            y: {");
			sb.AppendLine("                beginAtZero: true");
			sb.AppendLine("            }");
			sb.AppendLine("        }");
			sb.AppendLine("    }");
			sb.AppendLine("});");
			sb.AppendLine("</script>");

			// Код для создания круговой диаграммы
			sb.AppendLine("<canvas id=\"pieChart\" width=\"400\" height=\"400\"></canvas>");
			sb.AppendLine("<script>");
			sb.AppendLine("var ctx_pie = document.getElementById('pieChart').getContext('2d');");
			sb.AppendLine("var myPieChart = new Chart(ctx_pie, {");
			sb.AppendLine("    type: 'pie',");
			sb.AppendLine("    data: {");
			sb.AppendLine("        labels: [" + string.Join(",", data.Select(pair => $"'{pair.Value}'")) + "],");
			sb.AppendLine("        datasets: [{");
			sb.AppendLine("            label: 'Values',");
			sb.AppendLine("            data: [" + string.Join(",", data.Select(pair => pair.Key)) + "],");
			sb.AppendLine("            backgroundColor: [");

			// Генерация цветовой палитры для каждого символа для круговой диаграммы
			for (int i = 0; i < data.Count; i++)
			{
				int red = i * 70 % 255; // Генерация значения красного цвета
				int green = i * 130 % 255; // Генерация значения зеленого цвета
				int blue = i * 200 % 255; // Генерация значения синего цвета
				sb.AppendLine($"                'rgba({red}, {green}, {blue}, 0.2)',");
			}

			sb.AppendLine("            ],");
			sb.AppendLine("            borderColor: [");

			// Генерация цветов границ для каждого символа для круговой диаграммы
			for (int i = 0; i < data.Count; i++)
			{
				int red = i * 70 % 255; // Генерация значения красного цвета
				int green = i * 130 % 255; // Генерация значения зеленого цвета
				int blue = i * 200 % 255; // Генерация значения синего цвета
				sb.AppendLine($"                'rgba({red}, {green}, {blue}, 1)',");
			}

			sb.AppendLine("            ],");
			sb.AppendLine("            borderWidth: 2");
			sb.AppendLine("        }]");
			sb.AppendLine("    },");
			sb.AppendLine("    options: {");
			sb.AppendLine("        scales: {");
			sb.AppendLine("            y: {");
			sb.AppendLine("                beginAtZero: true");
			sb.AppendLine("            }");
			sb.AppendLine("        }");
			sb.AppendLine("    }");
			sb.AppendLine("});");
			sb.AppendLine("</script>");
			sb.AppendLine("</body>");
			sb.AppendLine("</html>");

			return sb.ToString();
		}

		public void UpdateView (object? sender, EventArgs args)
		{
			Dictionary<int, char> data = model.GetData(); // Получаем текущие данные модели
			string markupHTML = GenerateHTML(data); // Генерация HTML-разметки и запись в файл
			File.WriteAllText(FilePath, markupHTML); // Запись HTML-разметки в файл
			Console.WriteLine($"Файл {FilePath} обновлен.");
		}

		private readonly TableModel model;
		public readonly string FilePath;

		public TableView (TableModel model, string filePath)
		{
			this.model = model;
			FilePath = filePath;
			model.DataChanged += UpdateView; // Подписываемся на событие изменения данных
		}
	}

	internal sealed class TableModel
	{
		private Dictionary<int, char> data = [];

		public event EventHandler DataChanged;

		public void SetData (Dictionary<int, char> newData)
		{
			data = newData;
			NotifyObservers();
		}

		public Dictionary<int, char> GetData ()
		{
			return data;
		}

		private void NotifyObservers ()
		{
			DataChanged?.Invoke(this, EventArgs.Empty);
		}
	}

	internal sealed class TableController
	{
		private readonly TableModel model;

		public TableController (TableModel model)
		{
			this.model = model;
		}

		public void ChangeData (Dictionary<int, char> newData)
		{
			model.SetData(newData);
		}
	}

	internal sealed class BlueBorderDecorator : IView
	{
		private readonly IView decoratedView;

		public BlueBorderDecorator (IView decoratedView)
		{
			this.decoratedView = decoratedView;
		}

		public void UpdateView (object? sender, EventArgs args)
		{
			decoratedView.UpdateView(sender, args);

			// Получаем текущий HTML-код из декорированного представления
			string tableHTML = File.ReadAllText(((TableView) decoratedView).FilePath);

			// Найдем индекс начала таблицы в HTML
			int tableStartIndex = tableHTML.IndexOf("<table");
			if (tableStartIndex != -1)
			{
				// Вставляем стиль с синей рамкой перед началом таблицы
				string style = "<style>table { border: 2px solid blue; }</style>";
				tableHTML = tableHTML.Insert(tableStartIndex, style);
			}

			// Найдем индекс начала скрипта гистограммы в HTML
			int chartScriptStartIndex = tableHTML.IndexOf("type: 'bar'");
			if (chartScriptStartIndex != -1)
			{
				// Заменяем цвет рамок и фона для гистограммы на темно-синий
				int datasetsIndex = tableHTML.IndexOf("datasets: [{", chartScriptStartIndex);
				int closingBracketIndex = tableHTML.IndexOf('}', datasetsIndex);
				if (datasetsIndex != -1 && closingBracketIndex != -1)
				{
					tableHTML = tableHTML.Insert(closingBracketIndex, ",\nbackgroundColor: 'rgba(0, 0, 255, 0.5)',\nborderColor: 'rgba(0, 0, 255, 1)'");
				}
			}

			// Найдем индекс начала скрипта круговой диаграммы в HTML
			int pieChartScriptStartIndex = tableHTML.IndexOf("type: 'pie'");
			if (pieChartScriptStartIndex != -1)
			{
				// Заменяем цвет рамок и фона для круговой диаграммы на темно-синий
				int datasetsIndex = tableHTML.IndexOf("datasets: [{", pieChartScriptStartIndex);
				int closingBracketIndex = tableHTML.IndexOf('}', datasetsIndex);
				if (datasetsIndex != -1 && closingBracketIndex != -1)
				{
					tableHTML = tableHTML.Insert(closingBracketIndex, ",\nbackgroundColor: 'rgba(0, 0, 255, 0.5)',\nborderColor: 'rgba(0, 0, 255, 1)'");
				}
			}

			// Перезаписываем файл с обновленным HTML-кодом
			File.WriteAllText(((TableView) decoratedView).FilePath, tableHTML);

			Console.WriteLine("Добавлена синяя рамка к представлению.");
		}
	}
	public static void Execute ()
	{
		string fileName = "output.html";
		string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", fileName);

		if (File.Exists(filePath))
		{
			File.Delete(filePath);
		}

		TableModel model = new();
		TableView view = new(model, filePath);
		TableController controller = new(model);

		Console.WriteLine($"Программа запущена. Изменения данных будут сохраняться в файле {fileName}");
		Console.WriteLine($"{filePath}");

		// Декорируем представление синей рамкой
		BlueBorderDecorator blueBorderView = new(view);

		while (true)
		{
			Dictionary<int, char> data = GenerateRandomData();
			controller.ChangeData(data); // Изменяем данные модели

			Console.WriteLine($"Установлены новые данные:");

			foreach (KeyValuePair<int, char> pair in data)
			{
				Console.WriteLine($"{pair.Key}% - {pair.Value}");
			}

			Console.WriteLine();

			// Вызываем метод UpdateView у декорированного объекта
			//blueBorderView.UpdateView(null, EventArgs.Empty);

			// Пауза в 5 секунд перед следующим изменением данных
			Thread.Sleep(5000);
		}
	}

	private static Dictionary<int, char> GenerateRandomData ()
	{
		Random random = new();
		int count = random.Next(1, 27); // Генерируем случайное количество символов от 1 до 26, так как в алфавите 26 букв

		Dictionary<int, char> data = [];
		HashSet<int> usedKeys = []; // Хранит уже использованные ключи

		for (int i = 0; i < count; i++)
		{
			int keyValue;
			do
			{
				keyValue = random.Next(65, 91); // Генерируем случайный ASCII код для больших букв английского алфавита (65-90)
			} while (!usedKeys.Add(keyValue)); // Проверяем уникальность ключа

			char letter = (char) keyValue; // Получаем символ по ASCII коду
			data.Add(keyValue, letter);
		}

		return data;
	}
}