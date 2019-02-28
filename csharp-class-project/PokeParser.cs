using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_class_project
{
    class PokeParser : IPokeParser
    {
        public void ReadFromFile()
        {
            string currentDir = Directory.GetCurrentDirectory();

            DirectoryInfo dir = new DirectoryInfo(currentDir);

            var fileName = Path.Combine(dir.FullName, "Pokemon.json");
            var pokemons = DeserializePokemon(fileName);

            foreach (var pokemon in pokemons)
            {
                Console.WriteLine(pokemon.Name + " " + pokemon.Url);
            }
        }

        public static List<Pokemon> DeserializePokemon(string fileName)
        {
            var rootObject = new Rootobject();
            var serializer = new JsonSerializer();
            using (var reader = new StreamReader(fileName))
            using (var jsonReader = new JsonTextReader(reader))
            {
                rootObject = serializer.Deserialize<Rootobject>(jsonReader);
            }

            return rootObject.Pokemons.ToList<Pokemon>();
        }
    }
}
