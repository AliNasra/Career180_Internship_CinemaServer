using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class Posting: IComparable<Posting>
    {
        public int               postingID       { get; set; }
        public int               hallID          { get; set; }
        public Hall              hall            { get; set; }
        public int               filmID          { get; set; }
        public Film              film            { get; set; }      
        public int               cinemaID        { get; set; }
        public Cinema            cinema          { get; set; }
        public DateTime          operationDate   { get; set; }
        public double            operationFee    { get; set; }
        public bool?             isActive        { get; set; }
        public int               postingUserID   { get; set; }
        public User              postingUser     { get; set; }
        public List<Reservation> reservations    = new List<Reservation>();

        public string toString()
        {
            return $"Posting of ID {this.postingID} in cinema with ID {this.cinemaID}  in {this.operationDate:dddd, MMMM d, yyyy h:mm tt} with a fee of {this.operationFee}";
        }
        public int CompareTo(Posting other)
        {
            return this.operationDate.CompareTo(other.operationDate);
        }
    }  
}
