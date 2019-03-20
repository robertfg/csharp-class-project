using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace csharp_class_project
{
    public class Pokedex
    {
        public List<MyPokemon> Pokemons { get; set; }
    }

    public class MyPokemon
    {
        [JsonProperty(PropertyName = "counter")]
        public int Counter { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }
    }
}
