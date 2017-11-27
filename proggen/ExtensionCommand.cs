using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proggen
{
    internal class ExtensionCommand
    {
        /// <summary>
        /// Package guid for a visual studio extension - empty Guid means don't check for this extension
        /// </summary>
        public Guid ExtensionGuid { get; set; }

        /// <summary>
        /// Command to run from the devenv command line:
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Command first parameter for command line
        /// </summary>
        public string Param1 { get; set; }

        /// <summary>
        /// Command second parameter for command line
        /// </summary>
        public string Param2 { get; set; }

        /// <summary>
        /// Command third parameter for command line
        /// </summary>
        public string Param3 { get; set; }

        /// <summary>
        /// Command fourth parameter for command line
        /// </summary>
        public string Param4 { get; set; }
    }
}
