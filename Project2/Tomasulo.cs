using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class Tomasulo
    {
        public static void TomasuloSim(List<InstructionEntry> instructionEntries, State state)
        {
            int globalCount = 0;
            int count = 1;
            int instCounter = 0;
            Boolean done = false;
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
                for (int j = instCounter; j >= 0; j--)
                {
                    //if there is a data hazard
                    if(instructionEntries[instCounter].op1 == instructionEntries[j].dest ||
                        instructionEntries[instCounter].op2 == instructionEntries[j].dest)
                    {
                        //if the data hazard has not finished executing
                        if (instructionEntries[j].execute == null)
                            break;
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
                    } else
                    {
                        exeTime = GetExecutionTime(instructionEntries[instCounter].opcode);
                        if (exeTime != 0)
                        {
                            instructionEntries[instCounter].execute = (count).ToString() + " - " +
                                (count + exeTime).ToString();

                        }
                        else
                        {
                            instructionEntries[instCounter].execute = (count).ToString();
                        }
                    }
                }
                if(hazardExists == true)
                {
                    for(int j = hazardPointer; j >= 0; j--)
                    {
                        if (instructionEntries[instCounter].op1 == instructionEntries[j].dest ||
                        instructionEntries[instCounter].op2 == instructionEntries[j].dest)
                            if (count < (Int32.Parse(instructionEntries[j].write) + 1))
                            {
                                count = Int32.Parse(instructionEntries[j].write) + 1;
                                instructionEntries[instCounter].execute = (count).ToString() + " - " +
                                (count + exeTime).ToString();
                            }
                    }
                }

                count = count + exeTime + 1;
                #endregion

                #region Read
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
                for(int j = instCounter - 1; j >= 0; j--)
                {
                    if(instructionEntries[j].write == count.ToString())
                    {
                        count++;
                        j = instCounter - 1;
                    }
                }
                instructionEntries[instCounter].write = count.ToString();
                count++;
                #endregion

                if(instCounter == 0)
                {
                    instructionEntries[instCounter].commit = count.ToString();
                    state.rob.rob[state.rob.robPointer] = null;
                    state.rob.robPointer++;
                    if (state.rob.robPointer >= state.rob.rob.Length)
                        state.rob.robPointer = 0;
                    robCount.Add(count);
                }
                else if(count <= Int32.Parse(instructionEntries[instCounter - 1].commit) + 1)
                {
                    instructionEntries[instCounter].commit = (Int32.Parse(instructionEntries[instCounter - 1].commit) + 1).ToString();
                    state.rob.rob[state.rob.robPointer] = null;
                    state.rob.robPointer++;
                    if (state.rob.robPointer >= state.rob.rob.Length)
                        state.rob.robPointer = 0;
                    robCount.Add(count);
                }
                else
                {
                    instructionEntries[instCounter].commit = (Int32.Parse(instructionEntries[instCounter].write) + 1).ToString();
                    state.rob.rob[state.rob.robPointer] = null;
                    state.rob.robPointer++;
                    if (state.rob.robPointer >= state.rob.rob.Length)
                        state.rob.robPointer = 0;
                    robCount.Add(count);
                }

                PrintEntry(instructionEntries[instCounter]);

                count = globalCount + 1;
                instCounter++;
                if (instructionEntries[instructionEntries.Count - 1].commit != null)
                    done = true;
            }


        }

        private static void PrintEntry(InstructionEntry instructionEntry)
        {
            Console.WriteLine(String.Format("{0,-22}{1,7}{2,9}{3,10}{4,7}{5,8}{6,9}" ,
                            instructionEntry.instruction, instructionEntry.fetch, instructionEntry.decode,
                            instructionEntry.execute, instructionEntry.read, instructionEntry.write,
                            instructionEntry.commit));
        }

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
