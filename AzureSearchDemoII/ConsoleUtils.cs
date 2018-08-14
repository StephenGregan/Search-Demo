using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSearchDemoII
{
    internal static class ConsoleUtils
    {
        public static void WriteInfo(string format, params object[] args)
        {
            WriteColour(ConsoleColor.DarkGreen, "\t" + format, args);
        }

        public static void WriteColour(ConsoleColor colour, string format, params object[] args)
        {
            ConsoleColor oldColour = Console.ForegroundColor;
            Console.ForegroundColor = colour;
            Console.WriteLine(format, args);
            Console.ForegroundColor = oldColour;
        }

        public static int? ReadIntegerInput(string prompt, bool allowNull)
        {
            while (true)
            {
                Console.WriteLine(prompt);

                string line = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(line) && allowNull)
                {
                    return null;
                }

                int inputValue;

                if (int.TryParse(line, out inputValue))
                {
                    return inputValue;
                }
            }
        }

        public static int ReadIntegerInput(string prompt, int defaultValue, Func<int, bool> validator)
        {
            while (true)
            {
                int? input = ReadIntegerInput(prompt, allowNull: true);

                if (!input.HasValue)
                {
                    return defaultValue;
                }
                else
                {
                    if (validator(input.Value))
                    {
                        return input.Value;
                    }
                }
            }
        }

        public static int ReadIntegerinput(string prompt)
        {
            return ReadIntegerInput(prompt, allowNull: false).Value;
        }
    }
}
