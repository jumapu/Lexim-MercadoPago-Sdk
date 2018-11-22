using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MercadoPagoSDK.Samples
{
    class Program
    {
        private static readonly Dictionary<int, ISample> Samples =
            Assembly.GetEntryAssembly()
                    .GetTypes()
                    .Where(x => typeof(ISample).IsAssignableFrom(x))
                    .Where(x => x.IsClass)
                    .Select(Activator.CreateInstance)
                    .Cast<ISample>()
                    .OrderBy(x => x.Category)
                    .ThenBy(x => x.Name)
                    .Select((s, i) => (i + 1, s))
                    .ToDictionary(x => x.Item1, x => x.Item2);

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                PrintMenu();

                Console.WriteLine();
                Console.Write("Please select an option: ");

                switch (Console.ReadLine().ToLower())
                {
                    case "q":
                        return;
                    case string x when int.TryParse(x, out var option) && Samples.ContainsKey(option):
                        var sample = Samples[option];
                        RunSample(sample);
                        break;
                    default:
                        Console.WriteLine($"Invalid Option.");
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("Press Enter to return to Menu");
                Console.ReadLine();
            }
        }

        private static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an Option:");

            var categories =
                Samples.GroupBy(x => x.Value.Category)
                       .ToList();

            foreach (var c in categories)
            {
                Console.WriteLine();
                Console.WriteLine(c.Key);
                Console.WriteLine(new string('-', c.Key.Length));

                foreach (var (index, sample) in c)
                    Console.WriteLine($"  • {index:D2} - {sample.Name}");
            }

            Console.WriteLine();
            Console.WriteLine($"• Q - Exit");
        }

        private static void RunSample(ISample sample)
        {
            Console.Clear();
            Console.WriteLine(sample.Name);
            Console.WriteLine();

            switch (sample)
            {
                case IRequiresAccessToken _:
                    Utils.LoadOrPromptAccessToken();
                    break;
                case IRequiresClientCredentials _:
                    Utils.LoadOrPromptClientCredentials();
                    break;
            }

            Console.WriteLine();
            Console.WriteLine($"Running Sample: {sample.Name}");

            sample.Run();
        }
    }
}
