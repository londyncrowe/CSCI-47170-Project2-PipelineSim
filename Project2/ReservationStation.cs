using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class ReservationStation
    {
        public string[] adder { get; set; }
        public string[] mul { get; set; }
        public string[] load { get; set; }
        public string[] store { get; set; }

        public ReservationStation()
        {
            this.adder = new string[Config.adder];
            this.mul = new string[Config.mul];
            this.load = new string[Config.load];
            this.store = new string[Config.store];
        }
    }
}
