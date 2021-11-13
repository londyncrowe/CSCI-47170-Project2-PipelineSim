using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class ReorderBuffer
    {
        public string[] rob { get; set; }
        public int robPointer { get; set; }

        public ReorderBuffer()
        {
            this.rob = new string[Config.rob];
            this.robPointer = 0;
        }
    }
}
