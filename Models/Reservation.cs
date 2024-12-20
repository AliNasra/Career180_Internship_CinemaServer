﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class Reservation: IComparable<Reservation>
    {
        public int        reservationID    { get; set; }
        public int        userID           { get; set; }
        public User       reservingUser    { get; set; }
        public int        postingID        { get; set; }
        public Posting    posting          { get; set; }
        public int        seatID           { get; set; }
        public bool?      isActive         { get; set; }
        public Seat       seat             { get; set; }
        public DateTime   reservationDate  { get; set; }

        public string toString(Posting posting,Cinema cinema)
        {
            return $"Reservation of ID {this.reservationID} in {cinema.cinemaName} cinema in {posting.operationDate:dddd, MMMM d, yyyy h:mm tt} with a fee of {posting.operationFee}";
        }
        public int CompareTo(Reservation other)
        {
            return this.posting.operationDate.CompareTo(other.posting.operationDate);
        }
    }
}
