using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_class_project
{
    class PokedexParser : IPokedexParser
    {
        List<MyPokemon> IPokedexParser.ReadFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                var pokemon = new List<MyPokemon>();
                var serializer = new JsonSerializer();
                using (var reader = new StreamReader(fileName))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    pokemon = serializer.Deserialize<List<MyPokemon>>(jsonReader);
                }

                return pokemon;
            }

            return null;
        }

        public void WriteToFile(string fileName, List<MyPokemon> pokemon)
        {
            //bool appendFile = false;
            //if (File.Exists(fileName))
            //{
            //    appendFile = true;
            //}

            var serializer = new JsonSerializer();
            using (var writer = new StreamWriter(fileName))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jsonWriter, pokemon);
            }
        }

        public void CreateFile(string fileName)
        {
            File.Create(GetFullPath.ReturnPath(fileName));
        }

        public void DeleteFile(string fileName)
        {
            File.Delete(GetFullPath.ReturnPath(fileName));
        }
    }
}
