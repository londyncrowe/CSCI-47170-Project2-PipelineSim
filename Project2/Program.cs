using System;
using System.Collections.Generic;

namespace Project2
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(String.Format("{0,16}{1,12}{2,9}{3,6}{4,6}{5,8}" , "Instruction", "Issues", 
                "Executes", "Reads", "Write", "Commits"));
            Console.WriteLine("--------------------- ------ -------- ----- ----- -------");


            #region Fetch/Decode
            Fetch fetch = new Fetch();
            
            List<string> instructions = new List<string>();
            for (int i = 0; i < fetch.lineCount; i++)
            {
                string instruction = fetch.GetInstruction();
                instructions.Add(instruction);
            }


            // End Fetch --> Decode
            #endregion

            Simulate.Sim(instructions);
            Console.ReadKey();
        }
    }
}
