using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    /// <summary>
    /// Contains all operations that are handled at the Decode stage. 
    /// </summary>
    class Decode
    {
        private string instruction;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Decode()
        {
            SetInstruction(string.Empty);
        }

        /// <summary>
        /// Sets the object's instruction attribute.
        /// </summary>
        /// <param name="instruction">RISC-V instruction to set.</param>
        private void SetInstruction(string instruction)
        {
            this.instruction = instruction;
        }

        /// <summary>
        /// Splits instruction into its opcode (index 0) and operands (index 1 to n). 
        /// </summary>
        /// <returns>List of opcode (index 0) and operands (index 1 to n) as strings.</returns>
        public List<string> DecodeInstruction(string fetchedInstruction)
        {
            SetInstruction(fetchedInstruction);
            string[] instruction = this.instruction.Split(' ');
            string opcode = instruction[0];
            string[] operands = { };

            try
            {
                operands = instruction[1].Split(',');
            }
            catch (Exception e)
            { }
            List<string> decodedInstruction = new List<string>();
            decodedInstruction.Add(opcode);
            for (int i = 0; i < operands.Length; i++)
                decodedInstruction.Add(operands[i].Trim());
            return decodedInstruction;
        }

    }
}
