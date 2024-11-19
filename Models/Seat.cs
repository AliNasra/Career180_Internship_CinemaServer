using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class Seat
    {
        public int               seatID       { get; set; }
        public int               hallID       { get; set; }
        public bool?             isActive     { get; set; }
        public Hall              hall         { get; set; }
        public List<Reservation> reservations = new List<Reservation>();
    }
}
