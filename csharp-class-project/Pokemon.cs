using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace csharp_class_project
{
    public class Rootobject
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "next")]
        public object Next { get; set; }

        [JsonProperty(PropertyName = "previous")]
        public object Previous { get; set; }

        [JsonProperty(PropertyName = "results")]
        public Pokemon[] Pokemons { get; set; }
    }

    public class Pokemon
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}
