using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class InstructionEntry
    {
        public string fetch { get; set; }
        public string decode { get; set; }
        public string execute { get; set; }
        public int exeEnd { get; set; }
        public string read { get; set; }
        public string write { get; set; }
        public string commit { get; set; }
        public string instruction { get; set; }
        public string opcode { get; set; }
        public string dest { get; set; }
        public string op1 { get; set; }
        public string op2 { get; set; }

        public InstructionEntry()
        {
            this.fetch   = fetch;
            this.decode  = decode;
            this.execute = execute;
            this.exeEnd = exeEnd;
            this.read    = read;
            this.write   = write;
            this.commit  = commit;
            this.opcode = opcode;
            this.dest    = dest;
            this.op1     = op1;
            this.op2     = op2;
            this.instruction = instruction;
        }

        public void printEntries()
        {
            Console.WriteLine(String.Format("{0,-22}{1,7}{2,9}{3,10}{4,7}{5,8}{6,9}" ,
                            instruction, fetch, decode, execute, read, write, commit));
        }
    }
}
