using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class BranchPredictor
    {
        private int GlobalPredictor { get; set; }    //11 and 10 are take, 01 and 00 are don't take
        private int[] LocalPredictor { get; set; }
        private bool Selector { get; set; }     //high for global, low for local
        private bool Miss { get; set; }         //if the last prediction was incorrect
        private int LastLocalPredictor { get; set; }

        public BranchPredictor() {
            GlobalPredictor = 3;
            LocalPredictor = new int[10];
            for (int i = 0; i < LocalPredictor.Length; i++)
                LocalPredictor[i] = 3;
            Selector = true;
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="localBrancheBuffer">Size of buffer for local branch prediction</param>
        /// <param name="bitStart">Where to start the bit histories</param>
        public BranchPredictor(int localBrancheBuffer, int bitStart)
        {
            GlobalPredictor = bitStart;
            LocalPredictor = new int[localBrancheBuffer];
            for (int i = 0; i < LocalPredictor.Length; i++)
                LocalPredictor[i] = bitStart;
            Selector = true;
        }

        /// <summary>
        /// Update the predictors
        /// </summary>
        /// <param name="branchTaken">If the branch was taken</param>
        public void UpdatePredictor(bool branchTaken) {
            bool correctPrediction = DetermineCorrectPrediction(branchTaken);

            //Upate global predictor
            if (branchTaken && GlobalPredictor < 3)
                GlobalPredictor++;
            else if (!branchTaken && GlobalPredictor > 0)
                GlobalPredictor--;

            //Update local predictor
            if (branchTaken && LocalPredictor[LastLocalPredictor] < 3)
                LocalPredictor[LastLocalPredictor]++;
            else if (!branchTaken && LocalPredictor[LastLocalPredictor] > 0)
                LocalPredictor[LastLocalPredictor]--;

            if (Selector)
            { 
                //set selector
                if (correctPrediction)
                    Miss = false;
                else if (Miss)
                {
                    Selector = false;
                    Miss = false;
                }
                else
                    Miss = true;
            }
            else
            {
                //set selector
                if (correctPrediction)
                    Miss = false;
                else if (Miss)
                {
                    Selector = true;
                    Miss = false;
                }
                else
                    Miss = true;
            }
        }

        /// <summary>
        /// Determines if branch prediction was correct
        /// </summary>
        /// <param name="branchTaken">If the branch was taken</param>
        /// <returns>If the prediction was correct</returns>
        private bool DetermineCorrectPrediction(bool branchTaken) {
            bool correctPrediction;
            if (Selector)
            {
                if (branchTaken && GlobalPredictor > 1)
                    correctPrediction = true;
                else if (!branchTaken && GlobalPredictor < 2)
                    correctPrediction = true;
                else
                    correctPrediction = false;
            }
            else
            {
                if (branchTaken && LocalPredictor[LastLocalPredictor] > 1)
                    correctPrediction = true;
                else if (!branchTaken && LocalPredictor[LastLocalPredictor] < 2)
                    correctPrediction = true;
                else
                    correctPrediction = false;
            }
            return correctPrediction;
        }

        /// <summary>
        /// Tournament branch prediction using 2 bit history
        /// for global and 2 bit history for local
        /// </summary>
        /// <param name="pc">Progarm counter</param>
        /// <returns>If branch is predicted to be taken or not</returns>
        public bool PredictBranchTournament(int pc) {
            bool takeBranch;
            bool globalResult, localResult;

            globalResult = PredictBranchGlobal();
            localResult = PredictBranchLocal(pc);

            if (Selector)
                takeBranch = globalResult;
            else
                takeBranch = localResult;

            return takeBranch;
        }

        /// <summary>
        /// 2 bit histroy branch prediction for particular branch
        /// </summary>
        /// <param name="pc">Program counter</param>
        /// <returns>If branch is predicted to be taken or not</returns>
        public bool PredictBranchLocal(int pc) {
            LastLocalPredictor = Hash(pc);  //hash to index in local predictors
            bool takeBranch;

            if (LocalPredictor[LastLocalPredictor] > 1)
                takeBranch = true;
            else
                takeBranch = false;
            return takeBranch;
        }

        /// <summary>
        /// 2 bit histroy global branch prediction
        /// </summary>
        /// <returns>If branch is predicted to be taken or not</returns>
        public bool PredictBranchGlobal() {
            bool takeBranch;

            if (GlobalPredictor > 1)
                takeBranch = true;
            else
                takeBranch = false;
            return takeBranch;
        }

        /// <summary>
        /// Simple hashing algorithm.
        /// Right shift 2 to simulate dividing by 4 since pc will be multiple of 4
        /// and gets index by mod with array length
        /// </summary>
        /// <param name="pc">Program Counter at branch</param>
        /// <returns>Hash of program counter at branch</returns>
        private int Hash(int pc) {
            int hash = pc;
            hash = hash >> 2;
            hash = hash % LocalPredictor.Length;
            return hash;
        }

    }
}
