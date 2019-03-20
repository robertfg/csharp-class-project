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
        List<Pokemon> IPokeParser.ReadFromFile(string fileName)
        {
            var pokemons = DeserializePokemon(GetFullPath.ReturnPath(fileName));
            return pokemons;
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

            // Add the Counter:
            for (var i = 0; i < rootObject.Pokemons.Length; i++)
            {
                rootObject.Pokemons[i].Counter = i + 1;
            }

            return rootObject.Pokemons.ToList<Pokemon>();
        }
    }
}
