using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class Simulate
    {
        //private latency values for all the ops, could possible be removed if we added a config file and passed
        //in all of our latency values in the config file
        private static int lw = 1, flw = 1, sw = 1, fsw = 1, add = 1, sub = 1, bne = 1, beq = 1, fadd = 1,
            fsub = 1, fmul = 4, fdiv = 9, write = 1, read = 1, commit = 1, issue = 1;
        public static void Sim(List<string> instructions)
        {
            //used for print formatting in the switch cases
            string strIssue, strExecute, strRead, strWrite, strCommit;
            int count = 0;
            Decode decode = new Decode();
            //loop through all the instructions
            for(int i = 0; i < instructions.Count(); i++)
            {
                //use decode to grab the operation we will be performing
                List<string> decodedInstruction = decode.DecodeInstruction(instructions[i]);

                //first position of the decodedInstruction list will have the instruction
                switch(decodedInstruction[0])
                {
                    #region lwCase
                    case "lw":
                        count++;
                        strIssue = count.ToString();
                        count += issue;
                        strExecute = count.ToString() + " - ";
                        count += lw;
                        strExecute += count.ToString();
                        count += read;
                        strRead = count.ToString();
                        count += write;
                        strWrite = count.ToString();
                        count += commit;
                        strCommit = count.ToString();
                        Console.WriteLine(String.Format("{0,-22}{1,6}{2,9}{3,6}{4,6}{5,8}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                    #endregion
                    #region flwCase
                    case "flw":
                        count++;
                        strIssue = count.ToString();
                        count += issue;
                        strExecute = count.ToString() + " - ";
                        count += flw;
                        strExecute += count.ToString();
                        count += read;
                        strRead = count.ToString();
                        count += write;
                        strWrite = count.ToString();
                        count += commit;
                        strCommit = count.ToString();
                        Console.WriteLine(String.Format("{0,-22}{1,6}{2,9}{3,6}{4,6}{5,8}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                    #endregion
                    #region swCase
                    case "sw":
                        count++;
                        strIssue = count.ToString();
                        count += issue;
                        strExecute = count.ToString() + " - ";
                        count += sw;
                        strExecute += count.ToString();
                        count += read;
                        strRead = count.ToString();
                        count += write;
                        strWrite = count.ToString();
                        count += commit;
                        strCommit = count.ToString();
                        Console.WriteLine(String.Format("{0,-22}{1,6}{2,9}{3,6}{4,6}{5,8}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                    #endregion
                    #region fswCase
                    case "fsw":
                        count++;
                        strIssue = count.ToString();
                        count += issue;
                        strExecute = count.ToString() + " - ";
                        count += fsw;
                        strExecute += count.ToString();
                        count += read;
                        strRead = count.ToString();
                        count += write;
                        strWrite = count.ToString();
                        count += commit;
                        strCommit = count.ToString();
                        Console.WriteLine(String.Format("{0,-22}{1,6}{2,9}{3,6}{4,6}{5,8}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                    #endregion
                    #region addCase
                    case "add":
                        count++;
                        strIssue = count.ToString();
                        count += issue;
                        strExecute = count.ToString() + " - ";
                        count+= add;
                        strExecute += count.ToString();
                        strRead = "";
                        count += write;
                        strWrite = count.ToString();
                        count += commit;
                        strCommit = count.ToString();
                        Console.WriteLine(String.Format("{0,-22}{1,6}{2,9}{3,6}{4,6}{5,8}" ,
                            instructions[i], strIssue, strExecute, strRead, strWrite, strCommit));
                        break;
                    #endregion
                    #region subCase
                    case "sub":
                        count++;
                        strIssue = count.ToString();
                        count += issue;
                        strExecute = count.ToString() + " - ";
                        count += sub;
                        strExecute += count.ToString();
                        strRead = "";
                        count += write;
                        strWrite = count.ToString();
                        count += commit;
                        strCommit = count.ToString();
                        Console.WriteLine(String.Format("{0,-22}{1,6}{2,9}{3,6}{4,6}{5,8}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                    #endregion
                    #region beqCase
                    case "beq":
                        count++;
                        strIssue = count.ToString();
                        count += issue;
                        strExecute = count.ToString() + " - ";
                        count += beq;
                        strExecute += count.ToString();
                        strRead = "";
                        count += write;
                        strWrite = count.ToString();
                        count += commit;
                        strCommit = count.ToString();
                        Console.WriteLine(String.Format("{0,-22}{1,6}{2,9}{3,6}{4,6}{5,8}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                    #endregion
                    #region bneCase
                    case "bne":
                        count++;
                        strIssue = count.ToString();
                        count += issue;
                        strExecute = count.ToString() + " - ";
                        count += bne;
                        strExecute += count.ToString();
                        strRead = "";
                        count += write;
                        strWrite = count.ToString();
                        count += commit;
                        strCommit = count.ToString();
                        Console.WriteLine(String.Format("{0,-22}{1,6}{2,9}{3,6}{4,6}{5,8}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                    #endregion
                    #region faddCase
                    case "fadd.s":
                        count++;
                        strIssue = count.ToString();
                        count += issue;
                        strExecute = count.ToString() + " - ";
                        count += fadd;
                        strExecute += count.ToString();
                        strRead = "";
                        count += write;
                        strWrite = count.ToString();
                        count += commit;
                        strCommit = count.ToString();
                        Console.WriteLine(String.Format("{0,-22}{1,6}{2,9}{3,6}{4,6}{5,8}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                    #endregion
                    #region fsubCase
                    case "fsub.s":
                        count++;
                        strIssue = count.ToString();
                        count += issue;
                        strExecute = count.ToString() + " - ";
                        count += fsub;
                        strExecute += count.ToString();
                        strRead = "";
                        count += write;
                        strWrite = count.ToString();
                        count += commit;
                        strCommit = count.ToString();
                        Console.WriteLine(String.Format("{0,-22}{1,6}{2,9}{3,6}{4,6}{5,8}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                    #endregion
                    #region fmulCase
                    case "fmul.s":
                        count++;
                        strIssue = count.ToString();
                        count += issue;
                        strExecute = count.ToString() + " - ";
                        count += fmul;
                        strExecute += count.ToString();
                        strRead = "";
                        count += write;
                        strWrite = count.ToString();
                        count += commit;
                        strCommit = count.ToString();
                        Console.WriteLine(String.Format("{0,-22}{1,6}{2,9}{3,6}{4,6}{5,8}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                    #endregion
                    #region fdivCase
                    case "fdiv.s":
                        count++;
                        strIssue = count.ToString();
                        count += issue;
                        strExecute = count.ToString() + " - ";
                        count += fdiv;
                        strExecute += count.ToString();
                        strRead = "";
                        count += write;
                        strWrite = count.ToString();
                        count += commit;
                        strCommit = count.ToString();
                        Console.WriteLine(String.Format("{0,-22}{1,6}{2,9}{3,6}{4,6}{5,8}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                        #endregion
                }
            }
        }
    }
}
