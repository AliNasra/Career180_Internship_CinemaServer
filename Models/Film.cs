using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class Film

    {
        public int               filmID            { get; set; }
        public string            filmTitle         { get; set; }
        public string            description       { get; set; }
        public string            filmType          { get; set; }
        public int               slotCount         { get; set; }
        public int               insertingAdminID  { get; set; }
        public bool?             isActive          { get; set; }
        public User              insertingAdmin    { get; set; }
        public List<Posting>     postings          = new List<Posting>();

    }
}
