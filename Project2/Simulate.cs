using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class Simulate
    {
        private static int lw = 1, flw = 1, sw = 1, fsw = 1, add = 1, sub = 1, bne = 1, beq = 1, fadd = 1,
            fsub = 1, fmul = 4, fdiv = 9, write = 1, read = 1, commit = 1, issue = 1;
        public static void Sim(List<string> instructions)
        {
            string strIssue, strExecute, strRead, strWrite, strCommit;
            int count = 1;
            Decode decode = new Decode();
            for(int i = 0; i < instructions.Count(); i++)
            {
                List<string> decodedInstruction = decode.DecodeInstruction(instructions[i]);

                switch(decodedInstruction[0])
                {
                    #region lwCase
                    case "lw":
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
                        Console.WriteLine(String.Format("{0,-23}{1,-5}{2,-10}{3,-6}{4,-7}{5,-1}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                    #endregion
                    #region flwCase
                    case "flw":
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
                        Console.WriteLine(String.Format("{0,-23}{1,-5}{2,-10}{3,-6}{4,-7}{5,-1}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                    #endregion
                    #region swCase
                    case "sw":
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
                        Console.WriteLine(String.Format("{0,-23}{1,-5}{2,-10}{3,-6}{4,-7}{5,-1}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                    #endregion
                    #region fswCase
                    case "fsw":
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
                        Console.WriteLine(String.Format("{0,-23}{1,-5}{2,-10}{3,-6}{4,-7}{5,-1}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                    #endregion
                    #region addCase
                    case "add":
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
                        Console.WriteLine(String.Format("{0,-23}{1,-5}{2,-10}{3,-6}{4,-7}{5,-1}",
                            instructions[i], strIssue, strExecute, strRead, strWrite, strCommit));
                        break;
                    #endregion
                    #region subCase
                    case "sub":
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
                        Console.WriteLine(String.Format("{0,-23}{1,-5}{2,-10}{3,-6}{4,-7}{5,-1}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                    #endregion
                    #region beqCase
                    case "beq":
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
                        Console.WriteLine(String.Format("{0,-23}{1,-5}{2,-10}{3,-6}{4,-7}{5,-1}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                    #endregion
                    #region bneCase
                    case "bne":
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
                        Console.WriteLine(String.Format("{0,-23}{1,-5}{2,-10}{3,-6}{4,-7}{5,-1}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                    #endregion
                    #region faddCase
                    case "fadd.s":
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
                        Console.WriteLine(String.Format("{0,-23}{1,-5}{2,-10}{3,-6}{4,-7}{5,-1}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                    #endregion
                    #region fsubCase
                    case "fsub.s":
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
                        Console.WriteLine(String.Format("{0,-23}{1,-5}{2,-10}{3,-6}{4,-7}{5,-1}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                    #endregion
                    #region fmulCase
                    case "fmul.s":
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
                        Console.WriteLine(String.Format("{0,-23}{1,-5}{2,-10}{3,-6}{4,-7}{5,-1}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                    #endregion
                    #region fdivCase
                    case "fdiv.s":
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
                        Console.WriteLine(String.Format("{0,-23}{1,-5}{2,-10}{3,-6}{4,-7}{5,-1}" ,
                            instructions[i] , strIssue , strExecute , strRead , strWrite , strCommit));
                        break;
                        #endregion
                }
            }
        }
    }
}
