using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class Cinema
    {
        public int               cinemaID     { get; set; }
        public string            cinemaName   { get; set; }
        public bool?             isActive     { get; set; }
        public List<Hall>        halls        = new List<Hall>();
        public List<Posting>     postings     = new List<Posting>();
    }
}
