using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class Slot 
    {
        public int      slotID     { get; set; }
        public int      CinemaID   { get; set; }
        public Cinema   cinema     { get; set; }
        public int      hallID     { get; set; }
        public Hall     hall       { get; set; }
        public int      seatID     { get; set; }
        public Seat     seat       { get; set; }
        public int      filmID     { get; set; }
        public Film     film       { get; set; }
        public DateTime date       { get; set; }
    }
}
