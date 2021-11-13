using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class State
    {
        public ReservationStation res { get; set; }
        public ReorderBuffer rob { get; set; }

        public State()
        {
            this.res = new ReservationStation();
            this.rob = new ReorderBuffer();
        }
    }
}
