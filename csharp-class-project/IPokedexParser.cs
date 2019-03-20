using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_class_project
{
    interface IPokedexParser
    {
        void CreateFile(string fileName);

        List<MyPokemon> ReadFromFile(string fileName);

        void WriteToFile(string fileName, List<MyPokemon> myPokemons);

        void DeleteFile(string fileName);
    }
}
