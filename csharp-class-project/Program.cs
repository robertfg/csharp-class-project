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
            List<Pokemon> pokemons = pokey.ReadFromFile(pokemonFileName);

            // Open pokedex file:
            IPokedexParser pokedex = new PokedexParser();
            List<MyPokemon> myPokemons = pokedex.ReadFromFile(pokedexFileName);

            int pokedexCount;
            if (myPokemons == null)
            {
                pokedexCount = 0;
                myPokemons = new List<MyPokemon>();
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
                        viewList(pokemons, myPokemons, ref pokedexCount, pokedex, pokedexFileName);
                        break;
                    case 2:
                        if (pokedexCount == 0)
                        {
                            Console.WriteLine("Add some Pokemon first!");
                        }
                        else
                        {
                            modifyList(pokedex, myPokemons, pokedexCount, pokedexFileName);
                        }
                        break;
                    case 3:
                        // Delete the file and re-initialize the Pokedex.
                        pokedex.DeleteFile(pokedexFileName);
                        pokedexCount = 0;
                        myPokemons = new List<MyPokemon>();
                        break;
                }
            }
        }

        public static void modifyList(IPokedexParser pokedex,
                                      List<MyPokemon> myPokemons,
                                      int pokedexCount,
                                      string fileName)
        {
            string input;
            int selection = 0;
            int pageSize = 20;
            int pageCount = 1;

            // Get pokemons
            var pageOfPokemons = myPokemons.Take(pageSize);

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
                input = CLI.Prompt($"Enter a number to edit/delete that Pokemon, " +
                                   $"or 'M' to return to the Main Menu:  ");

                if (input.ToUpper().Equals("C"))
                {
                    pageOfPokemons = myPokemons.Skip(pageSize * pageCount).Take(pageSize);
                    pageCount++;
                }
                else if (int.TryParse(input, out selection))
                {
                    Console.Write("Enter your Comment or 'D' to Delete:  ");
                    input = Console.ReadLine();
                    Console.WriteLine("");

                    // Evaluate response:
                    if (input.ToUpper().Equals("D"))
                    {
                        myPokemons.RemoveAt(selection-1);
                    }
                    else
                    {
                        myPokemons[selection - 1].Comment = input;
                    }
                }

            } while (!input.ToUpper().Equals("M"));
        }

        public static void viewList(List<Pokemon> pokemons,
                                    List<MyPokemon> myPokemons,
                                    ref int pokedexCount,
                                    IPokedexParser pokedex,
                                    string fileName)
        {
            string input;
            int selection = 0;
            int pageSize = 20;
            int pageCount = 1;

            // Here's where I'll store my list of pokemon to add to my pokedex
            //List<MyPokemon> myPokemons = new List<MyPokemon>();

            var pageOfPokemons = pokemons.Take(pageSize);

            do
            {
                Console.WriteLine(string.Format("\t{0,-25}\t{1}", "Name", "URL"));
                Console.WriteLine(string.Format("\t{0,-25}\t{1}", "----", "---"));
                foreach (var pok in pageOfPokemons)
                {
                    Console.WriteLine($"{pok.Counter+1,3})\t{pok.Name,-25}\t{pok.Url}");
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
                    // Check for duplicates
                    if ( myPokemons.Any(pok => pok.Name == pokemons[selection - 1].Name))
                    {
                        Console.WriteLine("This Pokemon is already in the Pokedex.  Please select another.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                    }
                    else
                    {
                        // Add the selected pokemon to the list

                        pokedexCount++;
                        MyPokemon pokey = new MyPokemon
                        {
                            Counter = pokedexCount,
                            Name = pokemons[selection - 1].Name,
                            Url = pokemons[selection - 1].Url,
                            Comment = ""
                        };

                        myPokemons.Add(pokey);
                    }
                }

            } while (!input.ToUpper().Equals("M"));

            // Write entire list to file.
            pokedex.WriteToFile(fileName, myPokemons);
        }
    }
}
