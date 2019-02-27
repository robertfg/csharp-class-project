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
        public int count { get; set; }
        public object next { get; set; }
        public object previous { get; set; }
        public PokeList[] pokemonList { get; set; }
    }

    public class PokeList
    {
        public string name { get; set; }
        public string url { get; set; }
    }

}
