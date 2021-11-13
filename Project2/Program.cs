using System;
using System.Collections.Generic;

namespace Project2
{
    class Program
    {
        static void Main(string[] args)
       {
            Config.InitConfigurations();    // Reads config.txt file and sets latency values. 

            Console.WriteLine(String.Format("{0,16}{1,12}{2,9}{3,10}{4,7}{5,8}{6,9}" , "Instruction", "Fetch", 
                "Decode", "Execute", "Read", "Write", "Commit"));
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
            Fetch fetch = new Fetch();
            Decode decode = new Decode();
            //state is an object that contains the reservation station and the reorder buffer
            State state = new State();
            //create a list for all the instruction entries
            List<InstructionEntry> instructionEntry = new List<InstructionEntry>();
            for (int i = 0; i < fetch.lineCount; i++)
            {
                //add a new "spot" in the list for the next for loop
                instructionEntry.Add(new InstructionEntry());
            }

            //list of all instructions for sending to the simulation class
            List<string> instructions = new List<string>();
            List<string> decodedInst = new List<string>();

            //loop through the instructions and add them to the instruction list and the instruction Entry list
            for (int i = 0; i < fetch.lineCount; i++)
            {
                string instruction = fetch.GetInstruction();
                decodedInst = decode.DecodeInstruction(instruction);
                instructions.Add(instruction);
                instructionEntry[i].instruction = instruction;
                instructionEntry[i].opcode = decodedInst[0];
                instructionEntry[i].dest = decodedInst[1];
                instructionEntry[i].op1 = decodedInst[2];
                if(decodedInst.Count == 4)
                    instructionEntry[i].op2 = decodedInst[3];
            }

            //this is the static version
            //Simulate.Sim(instructions);

            //this is the Tomasulo version with out of order exe and register renaming
            Tomasulo.TomasuloSim(instructionEntry, state);
            

            Console.ReadKey();
        }
    }
}
