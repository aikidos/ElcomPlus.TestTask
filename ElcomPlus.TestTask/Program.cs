using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElcomPlus.TestTask.Providers;
using ElcomPlus.TestTask.Readers;

namespace ElcomPlus.TestTask
{
    class Program
    {
        static async Task Main()
        {
            Console.Write("Path: ");
            var path = Console.ReadLine();

            var provider = new DirectoryValueProvider();
            provider.RegisterReader(".xml", new XmlValueReader());
            provider.RegisterReader(".json", new JsonValueReader());

            var countByValue = new Dictionary<string, int>();

            await foreach (var value in provider.GetValuesAsync(path))
            {
                if (countByValue.TryGetValue(value, out var count))
                {
                    countByValue[value] = count + 1;
                }
                else
                {
                    countByValue.Add(value, 1);
                }
            }

            Console.Write("Result: ");

            if (countByValue.Count == 0)
            {
                Console.WriteLine("Нет значений.");
                return;
            }

            var (maxCountValue, maxCount) = countByValue
                .OrderByDescending(pair => pair.Value)
                .First();

            Console.WriteLine($"{maxCountValue} ({maxCount})");
        }
    }
}