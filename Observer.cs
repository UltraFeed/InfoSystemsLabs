#pragma warning disable CS8618
#pragma warning disable IDE0058
#pragma warning disable IDE0059
#pragma warning disable CA1303
#pragma warning disable CA1305
#pragma warning disable CA5394

using System.Text;
internal sealed class Observer
{
	internal sealed class TableView
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
			sb.AppendLine("            borderWidth: 1");
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
			sb.AppendLine("            borderWidth: 1");
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

		private readonly TableModel model;
		private readonly string filePath;

		// Конструктор класса TableView
		public TableView (TableModel model, string filePath)
		{
			this.model = model;
			this.filePath = filePath;
			model.DataChanged += UpdateView; // Подписываемся на событие изменения данных
		}

		// Метод для обновления представления
		public void UpdateView (object? sender, EventArgs args)
		{
			Dictionary<int, char> data = model.GetData(); // Получаем текущие данные модели
			string markupHTML = GenerateHTML(data); // Генерация HTML-разметки и запись в файл
			File.WriteAllText(filePath, markupHTML); // Запись HTML-разметки в файл
			Console.WriteLine($"Файл {filePath} обновлен.");
		}
	}

	// Модель (Model)
	internal sealed class TableModel
	{
		private Dictionary<int, char> data;

		// Событие, которое будет вызываться при изменении данных
		public event EventHandler DataChanged;

		// Метод для установки новых данных модели
		public void SetData (Dictionary<int, char> newData)
		{
			data = newData;
			NotifyObservers(); // Оповещение наблюдателей
		}

		// Метод для получения текущих данных модели
		public Dictionary<int, char> GetData ()
		{
			return data;
		}

		// Оповещение наблюдателей о изменении данных
		private void NotifyObservers ()
		{
			DataChanged?.Invoke(this, EventArgs.Empty);
		}
	}

	// Контроллер (Controller)
	internal sealed class TableController (TableModel model)
	{
		private readonly TableModel model = model;

		// Метод для изменения данных модели
		public void ChangeData (Dictionary<int, char> newData)
		{
			model.SetData(newData); // Устанавливаем новые данные модели
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

			// Пауза в 5 секунд перед следующим изменением данных
			Thread.Sleep(5000);
		}
	}

	private static Dictionary<int, char> GenerateRandomData ()
	{
		char [] alphabet = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];

		Random random = new();
		int count = random.Next(1, alphabet.Length);

		Dictionary<int, char> data = [];

		HashSet<int> usedKeys = []; // Хранит уже использованные ключи

		for (int i = 0; i < count; i++)
		{
			int percentage;
			do
			{
				percentage = random.Next(0, 101); // Генерируем уникальное значение ключа
			} while (!usedKeys.Add(percentage)); // Проверяем уникальность ключа

			char letter = alphabet [i % alphabet.Length]; // Используем буквы по порядку, повторяя алфавит при необходимости
			data.Add(percentage, letter);
		}

		return data;
	}
}