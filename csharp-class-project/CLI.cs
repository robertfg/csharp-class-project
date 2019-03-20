using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_class_project
{
    class CLI
    {
        internal static void DisplayWelcome()
        {
            Console.WriteLine();
            Console.WriteLine("------------     POKEMON LIST     ------------");
            Console.WriteLine("  Pick a Pokemon and add it to your Pokedex.  ");
            Console.WriteLine("----------------------------------------------");
        }

        internal static string Prompt(string message)
        {
            Console.Write(message);
            string userInput = Console.ReadLine();
            Console.WriteLine();

            return userInput.Trim();
        }
    }
}
