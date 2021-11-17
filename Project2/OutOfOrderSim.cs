using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class OutOfOrderSim
    {
        /// <summary>
        /// Simulates out-of-order execution
        /// </summary>
        /// <param name="instructionEntries"></param>
        /// <param name="state"></param>
        public static void OutOfOrder(List<InstructionEntry> instructionEntries, State state)
        {
            int globalCount = 0;
            int count = 1;
            int instCounter = 0;
            Boolean done = false;
            //CalculateBranches();
            //this is for checking the rob and making sure that fetches
            //are set accordingly if the rob is full
            List<int> robCount = new List<int>();

            //loop through
            while(!done)
            {
                #region fetch/decode
                for (int j = 0; j < Config.rob; j++)
                {
                    //if rob is not full
                    if (state.rob.rob[j] == null)
                    {
                        if (robCount.Count >= Config.rob && robCount[globalCount - Config.rob] >= count)
                            count = robCount[globalCount - Config.rob] + 1;


                            //put instruction into rob and set fetch and decode cycle counts
                            state.rob.rob[j] = instructionEntries[globalCount].instruction;
                        instructionEntries[globalCount].fetch = count.ToString();
                        count++;
                        instructionEntries[globalCount].decode = count++.ToString();
                        globalCount++;
                        break;
                    }
                    else continue;
                }
                #endregion

                #region execute
                int exeTime = 0;
                int hazardPointer = 0;
                Boolean hazardExists = false;
                //loop through and work on the next instruction that has not been committed yet
                for (int j = 0; j < instructionEntries.Count - 1; j++)
                    if(instructionEntries[j].commit == null)
                    {
                        instCounter = j;
                        break;
                    }

                //loop through previous entries looking for data hazards
                for (int j = instCounter - 1; j >= 0; j--)
                {
                    //if there is a data hazard
                    if(instructionEntries[instCounter].op1 == instructionEntries[j].dest ||
                        instructionEntries[instCounter].op2 == instructionEntries[j].dest ||
                        instructionEntries[instCounter].dest == instructionEntries[j].dest)
                    {
                        //if the data hazard has not finished executing
                        if (instructionEntries[j].execute == null)
                        {
                            hazardExists = true;
                            hazardPointer = j;
                            break;
                        }
                        else
                        {
                            count = Int32.Parse(instructionEntries[j].write) + 1;
                            exeTime = GetExecutionTime(instructionEntries[instCounter].opcode);
                            //instructionEntries[instCounter].execute = (count).ToString() + " - " +
                            //(count + exeTime).ToString();
                            hazardExists = true;
                            hazardPointer = j;
                            break;
                        }
                    }
                }
                //method call to get the execution time
                exeTime = GetExecutionTime(instructionEntries[instCounter].opcode);
                //if execute time is longer than one cycle add a '-' in the middle and get the end time
                if (exeTime != 0)
                    instructionEntries[instCounter].execute = (count).ToString() + " - " +
                        (count + exeTime).ToString();
                else
                    instructionEntries[instCounter].execute = (count).ToString();

                //this checks to see if data hazards have been detected and will
                //check to see if there are any other data hazards with a higher cycle
                //completion time than the first hazard it found
                if (hazardExists == true)
                {
                    for(int j = hazardPointer; j >= 0; j--)
                    {
                        //if data hazard exists
                        if (instructionEntries[instCounter].op1 == instructionEntries[j].dest ||
                        instructionEntries[instCounter].op2 == instructionEntries[j].dest)
                            //if new hazard found is higher than current
                            if (count < (Int32.Parse(instructionEntries[j].write) + 1))
                            {
                                //set the execute for printing later
                                count = Int32.Parse(instructionEntries[j].write) + 1;
                                instructionEntries[instCounter].execute = (count).ToString() + " - " +
                                (count + exeTime).ToString();
                            }
                    }
                }

                count = count + exeTime + 1;
                #endregion

                #region Read
                //if the instructions is a load that will need a read step
                if (instructionEntries[instCounter].execute != null && instructionEntries[instCounter].opcode == "lw" ||
                    instructionEntries[instCounter].opcode == "flw")
                {
                    instructionEntries[instCounter].read = count.ToString();
                    count++;
                }
                else
                    instructionEntries[instCounter].read = "";
                #endregion

                #region Write
                //loop through previous instructions to ensure that two writes are not happening
                //at the same time
                for(int j = instCounter - 1; j >= 0; j--)
                {
                    //if current write cycle is equal to another write cycle
                    if(instructionEntries[j].write == count.ToString())
                    {
                        count++;
                        j = instCounter - 1;
                    }
                }
                //set write cycle to be printed later
                instructionEntries[instCounter].write = count.ToString();
                count++;
                #endregion

                #region Commit
                //if it's the first time through
                if (instCounter == 0)
                {
                    instructionEntries[instCounter].commit = count.ToString();
                    state.rob.rob[state.rob.robPointer] = null;
                    state.rob.robPointer++;
                    if (state.rob.robPointer >= state.rob.rob.Length)
                        state.rob.robPointer = 0;
                    robCount.Add(count);
                }
                //if previous commit is greater than or equal to current count value
                else if(count <= Int32.Parse(instructionEntries[instCounter - 1].commit) + 1)
                {
                    instructionEntries[instCounter].commit = (Int32.Parse(instructionEntries[instCounter - 1].commit) + 1).ToString();
                    state.rob.rob[state.rob.robPointer] = null;
                    state.rob.robPointer++;
                    if (state.rob.robPointer >= state.rob.rob.Length)
                        state.rob.robPointer = 0;
                    robCount.Add(count);
                }
                //otherwise good to go and add the commit value
                else
                {
                    instructionEntries[instCounter].commit = (Int32.Parse(instructionEntries[instCounter].write) + 1).ToString();
                    state.rob.rob[state.rob.robPointer] = null;
                    state.rob.robPointer++;
                    if (state.rob.robPointer >= state.rob.rob.Length)
                        state.rob.robPointer = 0;
                    robCount.Add(count);
                }
                #endregion

                //print the entry
                PrintEntry(instructionEntries[instCounter]);

                //reset the count value
                count = globalCount + 1;
                instCounter++;
                //if the final instruction has been committed
                if (instructionEntries[instructionEntries.Count - 1].commit != null)
                    done = true;
            }
        }

        /// <summary>
        /// Print out the entry
        /// </summary>
        /// <param name="instructionEntry"></param>
        private static void PrintEntry(InstructionEntry instructionEntry)
        {
            Console.WriteLine(String.Format("{0,-22}{1,7}{2,9}{3,10}{4,7}{5,8}{6,9}" ,
                            instructionEntry.instruction, instructionEntry.fetch, instructionEntry.decode,
                            instructionEntry.execute, instructionEntry.read, instructionEntry.write,
                            instructionEntry.commit));
        }

        /// <summary>
        /// Switch case to determine the execution time set in the config file
        /// </summary>
        /// <param name="opcode"></param>
        /// <returns>integer value of execution time</returns>
        private static int GetExecutionTime(string opcode)
        {
            switch (opcode)
            {
                case "lw":
                    return Config.lw;
                case "flw":
                    return Config.flw;
                case "sw":
                    return Config.sw;
                case "fsw":
                    return Config.fsw;
                case "add":
                    return Config.add;
                case "sub":
                    return Config.sub;
                case "bne":
                    return Config.bne;
                case "beq":
                    return Config.beq;
                case "fadd":
                    return Config.fadd;
                case "fsub":
                    return Config.fsub;
                case "fmul":
                    return Config.fmul;
                case "fdiv":
                    return Config.fdiv;
                default:
                    return -1;
            }
        }

    }
}
