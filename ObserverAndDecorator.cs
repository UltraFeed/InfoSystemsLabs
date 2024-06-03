#pragma warning disable CA1303
#pragma warning disable CS8604

using System.Reflection;
using System.Security.Cryptography;
using RazorLight;

internal sealed class ObserverAndDecorator
{
    internal interface IView
    {
        void UpdateView (object? sender, EventArgs args);
    }

    private sealed class TableView : IView
    {
        private readonly TableModel model;
        internal readonly string FilePath;

        internal TableView (TableModel model, string filePath)
        {
            this.model = model;
            FilePath = filePath;
            model.DataChanged += UpdateView; // Подписываемся на событие изменения данных
        }

        public void UpdateView (object? sender, EventArgs args)
        {
            Dictionary<int, char> data = model.GetData();
            string markupHTML = RenderRazorViewToString("TableView", data);
            File.WriteAllText(FilePath, markupHTML);
            Console.WriteLine($"Файл {FilePath} обновлен.");
        }

        private static string RenderRazorViewToString (string viewName, object model)
        {
            RazorLightEngine razorEngine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(Assembly.GetExecutingAssembly())
                .UseMemoryCachingProvider()
                .Build();

            string templatePath = "InfoSystemsLabs.resources.ObserverAndDecorator.cshtml";
            string template;

            using (StreamReader reader = new(Assembly.GetExecutingAssembly().GetManifestResourceStream(templatePath)))
            {
                template = razorEngine.CompileRenderStringAsync(viewName, reader.ReadToEnd(), model).Result;
            }

            return template;
        }
    }

    internal sealed class TableModel
    {
        private Dictionary<int, char> data = [];

        internal event EventHandler? DataChanged;

        internal void SetData (Dictionary<int, char> newData)
        {
            data = newData;
            NotifyObservers();
        }

        internal Dictionary<int, char> GetData ()
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

        internal TableController (TableModel model)
        {
            this.model = model;
        }

        public void ChangeData (Dictionary<int, char> newData)
        {
            model.SetData(newData);
        }
    }

    internal sealed class BlueBorderDecorator (IView decoratedView) : IView
    {
        private readonly IView decoratedView = decoratedView;

        public void UpdateView (object? sender, EventArgs args)
        {
            decoratedView.UpdateView(sender, args);

            // Получаем текущий HTML-код из декорированного представления
            string tableHTML = File.ReadAllText(((TableView) decoratedView).FilePath);

            // Заменяем стиль с синей рамкой перед началом таблицы
            string newStyle = "border: 3px solid blue;";
            string oldStyle = "border: 3px solid black;";
            tableHTML = tableHTML.Replace(oldStyle, newStyle, StringComparison.OrdinalIgnoreCase);

            // Записываем обновленный HTML-код обратно в файл
            File.WriteAllText(((TableView) decoratedView).FilePath, tableHTML);
            Console.WriteLine($"Файл {((TableView) decoratedView).FilePath} обновлен с синей рамкой.");
        }
    }
    internal static void Execute ()
    {
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "output.html");

        File.Delete(filePath);

        TableModel model = new();
        TableView view = new(model, filePath);
        TableController controller = new(model);

        Console.WriteLine($"Программа запущена. Изменения данных будут сохраняться в файле {filePath}");
        Console.WriteLine($"{filePath}");

        BlueBorderDecorator blueBorderView = new(view);

        while (true)
        {
            Dictionary<int, char> data = GenerateRandomData();
            controller.ChangeData(data);

            Console.WriteLine($"Установлены новые данные:");

            foreach (KeyValuePair<int, char> pair in data)
            {
                Console.WriteLine($"{pair.Key}% - {pair.Value}");
            }

            Console.WriteLine();

            blueBorderView.UpdateView(null, EventArgs.Empty);

            Thread.Sleep(5000);
        }
    }

    private static Dictionary<int, char> GenerateRandomData ()
    {
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        int count = GetRandomNumber(rng, 1, 27); // Генерируем случайное количество символов от 1 до 26, так как в алфавите 26 букв
        Dictionary<int, char> data = [];
        HashSet<int> usedKeys = [];

        for (int i = 0; i < count; i++)
        {
            int keyValue;
            do
            {
                keyValue = GetRandomNumber(rng, 65, 91); // Генерируем случайный ASCII код для больших букв английского алфавита (65-90)
            } while (!usedKeys.Add(keyValue)); // Проверяем уникальность ключа

            char letter = (char) keyValue; // Получаем символ по ASCII коду
            data.Add(keyValue, letter);
        }

        return data;
    }

    private static int GetRandomNumber (RandomNumberGenerator rng, int minValue, int maxValue)
    {
        byte [] uint32Buffer = new byte [4];
        rng.GetBytes(uint32Buffer);
        uint randomValue = BitConverter.ToUInt32(uint32Buffer, 0);
        return (int) (minValue + (randomValue % (maxValue - minValue)));
    }
}