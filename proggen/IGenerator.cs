using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proggen
{
    interface IGenerator
    {
        string Name { get; set; }

        string VSVersion { get; set; }
        string Command { get; set; }
        void CreateFiles();
    }
}
