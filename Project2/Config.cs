using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project2
{
    static class Config
    {
        public static byte lw, flw, sw, fsw, add, sub, bne, beq, fadd, fsub, fmul, fdiv, write, read, commit, issue,
            adder, mul, load, store, rob;
        const string FILENAME = "Config.txt";

        /// <summary>
        /// Reads Config.txt file and sets latency values. 
        /// </summary>
        public static void InitConfigurations()
        {
            StreamReader sr = new StreamReader("../../../" + FILENAME);

            while(!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if(line == "latencies")
                {
                    sr.ReadLine();  // Throw away newline character

                    line = sr.ReadLine();
                    while (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] tokens = line.Split('=');
                        SetLatencyValue(tokens);
                        line = sr.ReadLine();
                    }
                }
                else if(line == "buffers")
                {
                    sr.ReadLine();  // Throw away newline character
                    // TODO: Logic to grab Buffer values
                }
                else
                {
                    Console.WriteLine("Could not read " + FILENAME + ". Exiting...");
                    Environment.Exit(0);
                }
            }
        }

        /// <summary>
        /// Sets latency value to given value from Config.txt file. 
        /// </summary>
        /// <param name="tokens">
        ///     tokens[0] - latency variable
        ///     tokens[1] - latency value
        /// </param>
        private static void SetLatencyValue(string[] tokens)
        {
            switch (tokens[0].Trim())
            {
                case "lw":
                    lw = byte.Parse(tokens[1]);
                    break;
                case "flw":
                    flw = byte.Parse(tokens[1]);
                    break;
                case "sw":
                    sw = byte.Parse(tokens[1]);
                    break;
                case "fsw":
                    fsw = byte.Parse(tokens[1]);
                    break;
                case "add":
                    add = byte.Parse(tokens[1]);
                    break;
                case "sub":
                    sub = byte.Parse(tokens[1]);
                    break;
                case "bne":
                    bne = byte.Parse(tokens[1]);
                    break;
                case "beq":
                    beq = byte.Parse(tokens[1]);
                    break;
                case "fadd":
                    fadd = byte.Parse(tokens[1]);
                    break;
                case "fsub":
                    fsub = byte.Parse(tokens[1]);
                    break;
                case "fmul":
                    fmul = byte.Parse(tokens[1]);
                    break;
                case "fdiv":
                    fdiv = byte.Parse(tokens[1]);
                    break;
                case "write":
                    write = byte.Parse(tokens[1]);
                    break;
                case "read":
                    read = byte.Parse(tokens[1]);
                    break;
                case "commit":
                    commit = byte.Parse(tokens[1]);
                    break;
                case "issue":
                    issue = byte.Parse(tokens[1]);
                    break;
                case "adder":
                    adder = byte.Parse(tokens[1]);
                    break;
                case "mul":
                    mul = byte.Parse(tokens[1]);
                    break;
                case "load":
                    load = byte.Parse(tokens[1]);
                    break;
                case "store":
                    store = byte.Parse(tokens[1]);
                    break;
                case "rob":
                    rob = byte.Parse(tokens[1]);
                    break;
            }
        }
        
    }
}
