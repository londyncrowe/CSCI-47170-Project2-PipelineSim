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
            Console.WriteLine("Enter file name:");
            Fetch fetch = new Fetch();
            //Fetch fetch = new Fetch(Console.ReadLine());

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
