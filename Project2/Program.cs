using System;
using System.Collections.Generic;

namespace Project2
{
    class Program
    {
        static void Main(string[] args)
       {
            Config.InitConfigurations();    // Reads config.txt file and sets latency values. 

            Console.Write("Enter file name (leave blank for default): ");
            string filename = Console.ReadLine();
            Fetch fetch = new Fetch();    // Use default constructor
            if (!string.IsNullOrEmpty(filename))
            {
                try
                {
                    fetch = new Fetch(filename);
                }
                catch(System.IO.FileNotFoundException e)
                {
                    Console.WriteLine("Error: Couldn't find " + filename + ". Exiting...");
                    Environment.Exit(0);
                }
            }
            Console.WriteLine("\n");    // Formatting
            Console.WriteLine(String.Format("{0,16}{1,12}{2,9}{3,10}{4,7}{5,8}{6,9}" , "Instruction" , "Fetch" ,
               "Decode" , "Execute" , "Read" , "Write" , "Commit"));
            Console.WriteLine("--------------------- ------- -------- --------- ------ ------- --------");



            #region branchPredictor
            BranchPredictor predictor = new BranchPredictor(2, 3);

            predictor.PredictBranchTournament(4);
            predictor.UpdatePredictor(false);
            predictor.PredictBranchTournament(4);
            predictor.UpdatePredictor(false);
            predictor.PredictBranchTournament(4);
            predictor.UpdatePredictor(false);
            predictor.PredictBranchTournament(4);
            predictor.UpdatePredictor(true);
            predictor.PredictBranchTournament(4);
            predictor.UpdatePredictor(true);
            predictor.PredictBranchTournament(4);
            predictor.UpdatePredictor(false);
            predictor.PredictBranchTournament(0);
            predictor.UpdatePredictor(true);
            predictor.PredictBranchTournament(0);
            predictor.UpdatePredictor(false);
            #endregion

            //fetch gets the file with all the instructions
            Decode decode = new Decode();
            //state is an object that contains the reservation station and the reorder buffer
            State state = new State();
            //create a list for all the instruction entries
            List<InstructionEntry> instructionEntry = new List<InstructionEntry>();

            //list of all instructions for sending to the simulation class
            List<string> instructions = new List<string>();
            List<List<string>> decodedInsts = new List<List<string>>();

            for (int i = 0; i < fetch.lineCount; i++)
            {
                instructions.Add(fetch.GetInstruction().Trim().Replace(".s", ""));
                decodedInsts.Add(decode.DecodeInstruction(instructions[i]));
            }

            Dictionary<string, int> labels = FindLabels(ref decodedInsts, ref instructions);

            //loop through the instructions and add them to the instruction list and the instruction Entry list
            for (int i = 0; i < fetch.lineCount - labels.Count; i++)
            {
                instructionEntry.Add(new InstructionEntry());
                instructionEntry[i].instruction = instructions[i].Trim();
                instructionEntry[i].opcode = decodedInsts[i][0];
                instructionEntry[i].dest = decodedInsts[i][1];
                instructionEntry[i].op1 = decodedInsts[i][2];
                if(decodedInsts[i].Count == 4)
                    instructionEntry[i].op2 = decodedInsts[i][3];
            }

            //this is the static version
            //Simulate.Sim(instructions);

            //this is the out of order version with out of order exe
            OutOfOrderSim.OutOfOrder(instructionEntry, state);
            

            Console.ReadKey();
        }

        private static int[] registers = new int[32];

        /// <summary>
        /// Finds where the labels are in the instructions
        /// </summary>
        /// <param name="instructions"></param>
        /// <returns></returns>
        private static Dictionary<string, int> FindLabels(ref List<List<string>> decodedInstructions, ref List<string> instructions)
        {
            Dictionary<string, int> labels = new Dictionary<string, int>();
            string mneumonics = ("lw flw sw fsw add sub beq bne fadd.s fsub.s fmul.s fdiv.s");
            List<string> curInst;
            for (int i = 0; i < decodedInstructions.Count; i++)
            {
                curInst = decodedInstructions[i];
                if (!mneumonics.Contains(curInst[0].Trim()))
                {
                    labels.Add(curInst[0].Trim(':'), i);
                    decodedInstructions.RemoveAt(i);
                    instructions.RemoveAt(i);
                    i--;
                }
            }
            return labels;
        }

        /// <summary>
        /// Pre-Calculates the results of each branch
        /// </summary>
        /// <param name="instructions"></param>
        /// <returns></returns>
        private static List<bool> CalculateBranches(List<List<string>> instructions, Dictionary<string, int> labels)
        {
            List<string> curInst;
            List<bool> branchesTaken = new List<bool>();
            int dest, source1, source2;
            for (int i = 0; i < instructions.Count; i++)
            {
                curInst = instructions[i];
                //add counts for execute end
                switch (curInst[0].Trim())
                {
                    #region lwCase
                    case "lw":
                        Int32.TryParse(curInst[1].Substring(1), out dest);
                        registers[dest] = dest;
                        break;
                    #endregion
                    #region flwCase
                    case "flw":
                        Int32.TryParse(curInst[1].Substring(1), out dest);
                        registers[dest] = dest;
                        break;
                    #endregion
                    #region swCase
                    case "sw":
                        break;
                    #endregion
                    #region fswCase
                    case "fsw":
                        break;
                    #endregion
                    #region addCase
                    case "add":
                        Int32.TryParse(curInst[1].Substring(1), out dest);
                        Int32.TryParse(curInst[2].Substring(1), out source1);
                        Int32.TryParse(curInst[3].Substring(1), out source2);
                        registers[dest] = registers[source1] + registers[source2];
                        break;
                    #endregion
                    #region subCase
                    case "sub":
                        Int32.TryParse(curInst[1].Substring(1), out dest);
                        Int32.TryParse(curInst[2].Substring(1), out source1);
                        Int32.TryParse(curInst[3].Substring(1), out source2);
                        registers[dest] = registers[source1] - registers[source2];
                        break;
                    #endregion
                    #region beqCase
                    case "beq":
                        Int32.TryParse(curInst[1].Substring(1), out source1);
                        Int32.TryParse(curInst[2].Substring(1), out source2);
                        if (registers[source1] == registers[source2])
                        {
                            branchesTaken.Add(true);
                            labels.TryGetValue(curInst[3], out i);
                        }
                        else
                            branchesTaken.Add(false);
                        break;
                    #endregion
                    #region bneCase
                    case "bne":
                        Int32.TryParse(curInst[1].Substring(1), out source1);
                        Int32.TryParse(curInst[2].Substring(1), out source2);
                        if (registers[source1] == registers[source2])
                            branchesTaken.Add(false);
                        else
                        {
                            branchesTaken.Add(true);
                            labels.TryGetValue(curInst[3], out i);
                        }
                        break;
                    #endregion
                    #region faddCase
                    case "fadd.s":
                        Int32.TryParse(curInst[1].Substring(1), out dest);
                        Int32.TryParse(curInst[2].Substring(1), out source1);
                        Int32.TryParse(curInst[3].Substring(1), out source2);
                        registers[dest] = registers[source1] + registers[source2];
                        break;
                    #endregion
                    #region fsubCase
                    case "fsub.s":
                        Int32.TryParse(curInst[1].Substring(1), out dest);
                        Int32.TryParse(curInst[2].Substring(1), out source1);
                        Int32.TryParse(curInst[3].Substring(1), out source2);
                        registers[dest] = registers[source1] - registers[source2];
                        break;
                    #endregion
                    #region fmulCase
                    case "fmul.s":
                        Int32.TryParse(curInst[1].Substring(1), out dest);
                        Int32.TryParse(curInst[2].Substring(1), out source1);
                        Int32.TryParse(curInst[3].Substring(1), out source2);
                        registers[dest] = registers[source1] * registers[source2];
                        break;
                    #endregion
                    #region fdivCase
                    case "fdiv.s":
                        Int32.TryParse(curInst[1].Substring(1), out dest);
                        Int32.TryParse(curInst[2].Substring(1), out source1);
                        Int32.TryParse(curInst[3].Substring(1), out source2);
                        registers[dest] = registers[source1] / registers[source2];
                        break;
                        #endregion
                }
            }
            return branchesTaken;
        }
    }
}
