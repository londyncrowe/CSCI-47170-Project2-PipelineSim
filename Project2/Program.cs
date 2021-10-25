using System;
using System.Collections.Generic;

namespace Project2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Example Fetch --> Decode 
            Fetch fetch = new Fetch();
            string instruction = fetch.GetInstruction();

            Decode decode = new Decode();
            List<string> decodedInstruction = decode.DecodeInstruction(instruction);

            for (int i = 0; i < decodedInstruction.Count; i++)
                Console.WriteLine(decodedInstruction[i]);

            instruction = fetch.GetInstruction();
            decodedInstruction = decode.DecodeInstruction(instruction);

            for (int i = 0; i < decodedInstruction.Count; i++)
                Console.WriteLine(decodedInstruction[i]);

            // End Fetch --> Decode
        }
    }
}
