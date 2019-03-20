using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_class_project
{
    public static class GetFullPath
    {
        public static string ReturnPath(string fileName)
        {
            string currentDir = Directory.GetCurrentDirectory();
            DirectoryInfo dir = new DirectoryInfo(currentDir);
            var fullName = Path.Combine(dir.FullName, fileName);
            return fullName;
        }
    }
}
