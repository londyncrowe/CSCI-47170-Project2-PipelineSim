using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project2
{
    /// <summary>
    /// Contains all operations that are handled at the Fetch stage. 
    /// </summary>
    class Fetch
    {
        private StreamReader sr;
        public int lineCount = File.ReadLines("../../../Instructions.txt").Count();

        /// <summary>
        /// Default constructor
        /// </summary>
        public Fetch()
        {
            sr = new StreamReader("../../../Instructions.txt");
            
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="filepath">File path to read from.</param>
        public Fetch(string filepath)
        {
            sr = new StreamReader("../../../" + filepath);
        }

        /// <summary>
        /// Gets the next instruction to send down the pipeline. 
        /// </summary>
        public string GetInstruction()
        {
            // Does it read binary or plaintext (BinaryReader or StreamReader)? 
            return sr.ReadLine();
        }
    }
}
