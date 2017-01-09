using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proggen
{
    class Program
    {
        static void Main(string[] args)
        {   
            try
            {
                throw new Exception("hello");
            }
            catch (Exception ex)
            {
                string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                string progname = Path.GetFileNameWithoutExtension(codeBase);
                //string fullname = Path.GetFullPath(codeBase);
                string name = System.Reflection.Assembly.GetExecutingAssembly().Location;
                Console.Error.WriteLine(progname + ": Error: " + ex.Message);
            }
        }
    }
}
