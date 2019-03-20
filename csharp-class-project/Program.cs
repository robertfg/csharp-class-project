using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_class_project
{
    class Program
    {
        static void Main(string[] args)
        {
            // Names of the files:
            string pokemonFileName = "Pokemon.json";
            string pokedexFileName = "Pokedex.json";

            // Get pokemon info:
            IPokeParser pokey = new PokeParser();
            IList<Pokemon> pokemons = pokey.ReadFromFile(pokemonFileName);

            // Open pokedex file:
            IPokedexParser pokedex = new PokedexParser();
            IEnumerable<MyPokemon> myPokemons = pokedex.ReadFromFile(pokedexFileName);

            int pokedexCount;
            if (myPokemons == null)
            {
                pokedexCount = 0;
            }
            else
            {
                pokedexCount = myPokemons.Count();
            }

            // Display the welcome screen:
            CLI.DisplayWelcome();

            // Put up the menu:
            int option = 0;
            while ((option = Menu.Prompt()) != 4)
            {
                switch (option)
                {
                    case 1:
                        viewList(pokemons, pokedex, pokedexFileName);
                        break;
                    case 2:
                        modifyList(pokedex, pokedexFileName);
                        break;
                    case 3:
                        pokedex.DeleteFile(pokedexFileName);
                        break;
                }
            }
        }

        public static void modifyList(IPokedexParser pokedex, string fileName)
        {
            string input;
            int selection = 0;
            int pageSize = 20;
            int pageCount = 1;

            // Get pokemons
            IEnumerable<MyPokemon> pokemons = pokedex.ReadFromFile(fileName);

            int pokedexCount = pokemons.Count();

            var pageOfPokemons = pokemons.Take(pageSize);

            do
            {
                Console.WriteLine(string.Format("\t{0,-40}\t{1}", "Name/URL", "Comment"));
                Console.WriteLine(string.Format("\t{0,-40}\t{1}", "--------", "-------"));
                foreach (var pok in pageOfPokemons)
                {
                    Console.WriteLine($"  {pok.Counter,3})\t{pok.Name,-40}\t{pok.Comment}");
                    Console.WriteLine($"  \t{pok.Url}");
                }
                Console.WriteLine();
                input = CLI.Prompt($"Enter a number to edit/delete that Pokemon, \n" +
                                   $"or 'M' to return to the Main Menu:  ");

                if (input.ToUpper().Equals("C"))
                {
                    pageOfPokemons = pokemons.Skip(pageSize * pageCount).Take(pageSize);
                    pageCount++;
                }
                else if (int.TryParse(input, out selection))
                {
                    Console.WriteLine("");
                }

            } while (!input.ToUpper().Equals("M"));
        }

        public static void viewList(IList<Pokemon> pokemons,
                                    IPokedexParser pokedex,
                                    string fileName)
        {
            string input;
            int selection = 0;
            int count = 1;
            int pageSize = 20;
            int pageCount = 1;

            // Here's where I'll store my list of pokemon to add to my pokedex
            List<MyPokemon> myPokemons = new List<MyPokemon>();

            var pageOfPokemons = pokemons.Take(pageSize);

            do
            {
                Console.WriteLine(string.Format("\t{0,-25}\t{1}", "Name", "URL"));
                Console.WriteLine(string.Format("\t{0,-25}\t{1}", "----", "---"));
                foreach (var pok in pageOfPokemons)
                {
                    Console.WriteLine($"{pok.Counter,3})\t{pok.Name,-25}\t{pok.Url}");
                }
                Console.WriteLine();
                input = CLI.Prompt($"Enter a number to add the Pokemon to your Pokedex,\n" +
                                   $"'C' to Continue, or 'M' to return to the Main Menu:  ");

                if (input.ToUpper().Equals("C"))
                {
                    pageOfPokemons = pokemons.Skip(pageSize * pageCount).Take(pageSize);
                    pageCount++;
                }
                else if (int.TryParse(input, out selection))
                {
                    // Add the selected pokemon to the list
                    MyPokemon pokey = new MyPokemon();

                    pokey.Counter = count;
                    pokey.Name = pokemons[selection - 1].Name;
                    pokey.Url = pokemons[selection - 1].Url;
                    pokey.Comment = "";

                    myPokemons.Add(pokey);

                    count++;
                }

            } while (!input.ToUpper().Equals("M"));

            // Write entire list to file.
            pokedex.WriteToFile(fileName, myPokemons);
        }
    }
}
