using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class Hall
    {
        public int               hallID         { get; set; }
        public int               cinemaID       { get; set; }
        public Cinema            cinema         { get; set; }
        public int               capacity       { get; set; }
        public bool?             isActive       { get; set; }
        public List<Seat>        seats          = new List<Seat>();
        public List<Posting>     postings       = new List<Posting>();

    }
}
