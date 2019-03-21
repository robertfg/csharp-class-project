using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_class_project
{
    class Menu
    {
        static string[] _options = new string[]
        {
            "View list of Pokemons",
            "View/Edit Pokedex",
            "Delete Pokedex",
            "Quit"
        };

        static void Display()
        {
            Console.WriteLine();
            for (int i = 0; i < _options.Length; i++)
            {
                Console.WriteLine($"  {i + 1}) {_options[i]}");
            }
            Console.WriteLine();
        }

        internal static int Prompt()
        {
            bool valid = false;
            int parsedOption = 0;
            string option = string.Empty;

            Display();

            do
            {
                option = CLI.Prompt($"Please select an option (1-{_options.Length}): ");
                bool canParse = int.TryParse(option, out parsedOption);
                valid = canParse && parsedOption > 0 && parsedOption <= 4;

                if (!valid)
                {
                    Console.WriteLine("'" + option + "' is not a valid option. Please provide a number from 1 - 4.");
                    Console.WriteLine();
                }
            }
            while (!valid);

            return parsedOption;
        }
    }
}
