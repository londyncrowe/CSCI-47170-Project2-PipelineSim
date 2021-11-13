using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class Simulate
    {
        public static void Sim(List<string> instructions)
        {
            //used for print formatting in the switch cases
            string strInst, strIssue, strExecute, strRead, strWrite, strCommit;
            List<int[]> counts = new List<int[]>();
            counts.Add(new[] { 0, 1, 1, 3, 4, 5});
            Decode decode = new Decode();
            //loop through all the instructions
            while(instructions.Count != 0)
            {
                //fill a buffer with the instructions to populate the pipeline
                List<string> decodedInst = decode.DecodeInstruction(instructions[0]);
                strInst = instructions[0];
                instructions.RemoveAt(0);
                counts.Add(new int[6]);

                //Count issue stage for each instruction
                    counts[counts.Count - 1][0] = counts[counts.Count - 2][1];  //last instruction leaves issue stage

                //when entering the execute stage
                    counts[counts.Count - 1][1] = counts[counts.Count - 2][2] + 1;

                //add counts for execute end
                switch (decodedInst[0])
                {
                    #region lwCase
                    case "lw":
                        counts[counts.Count - 1][2] = counts[counts.Count - 1][1] + Config.lw;
                        /*
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
                        */
                        break;
                    #endregion
                    #region flwCase
                    case "flw":
                        counts[counts.Count - 1][2] = counts[counts.Count - 1][1] + Config.flw;
                        /*
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
                        */
                        break;
                    #endregion
                    #region swCase
                    case "sw":
                        counts[counts.Count - 1][2] = counts[counts.Count - 1][1] + Config.sw;
                        break;
                    #endregion
                    #region fswCase
                    case "fsw":
                        counts[counts.Count - 1][2] = counts[counts.Count - 1][1] + Config.fsw;
                        break;
                    #endregion
                    #region addCase
                    case "add":
                        counts[counts.Count - 1][2] = counts[counts.Count - 1][1] + Config.add;
                        break;
                    #endregion
                    #region subCase
                    case "sub":
                        counts[counts.Count - 1][2] = counts[counts.Count - 1][1] + Config.sub;
                        break;
                    #endregion
                    #region beqCase
                    case "beq":
                        counts[counts.Count - 1][2] = counts[counts.Count - 1][1] + Config.beq;
                        break;
                    #endregion
                    #region bneCase
                    case "bne":
                        counts[counts.Count - 1][2] = counts[counts.Count - 1][1] + Config.bne;
                        break;
                    #endregion
                    #region faddCase
                    case "fadd.s":
                        counts[counts.Count - 1][2] = counts[counts.Count - 1][1] + Config.fadd;
                        break;
                    #endregion
                    #region fsubCase
                    case "fsub.s":
                        counts[counts.Count - 1][2] = counts[counts.Count - 1][1] + Config.fsub;
                        break;
                    #endregion
                    #region fmulCase
                    case "fmul.s":
                        counts[counts.Count - 1][2] = counts[counts.Count - 1][1] + Config.fmul;
                        break;
                    #endregion
                    #region fdivCase
                    case "fdiv.s":
                        counts[counts.Count - 1][2] = counts[counts.Count - 1][1] + Config.fdiv;
                        break;
                        #endregion
                }

                //read counts
                if (decodedInst[0] == "lw" || decodedInst[0] == "flw")
                {
                    counts[counts.Count - 1][3] = counts[counts.Count - 1][2] + 1;
                    strRead = counts[counts.Count - 1][3].ToString();
                }
                else
                {
                    counts[counts.Count - 1][3] = counts[counts.Count - 1][2];
                    strRead = "";
                }
                
                //write counts
                counts[counts.Count - 1][4] = counts[counts.Count - 1][3] + 1;
                //commit counts
                counts[counts.Count - 1][5] = counts[counts.Count - 1][4] + 1;




                strIssue = counts[counts.Count - 1][0].ToString();
                strExecute = counts[counts.Count - 1][1].ToString() + " - "
                    + counts[counts.Count - 1][2].ToString();
                strWrite = counts[counts.Count - 1][4].ToString();
                strCommit = counts[counts.Count - 1][5].ToString();

                Console.WriteLine(String.Format("{0,-22}{1,7}{2,9}{3,10}{4,7}{5,8}{6,9}" ,
                            strInst, strIssue, strIssue, strExecute, strRead, strWrite, strCommit));
            }
        }
    }
}
