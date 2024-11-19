using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Services
{
    public class SeatService
    {

        public static void addSeat(ApplicationDbContext context, int hallID)
        {
            Seat newSeat = new Seat
            {
                hallID = hallID,
            };
            context.Seats.Add(newSeat);
            context.SaveChanges();
        }
        public static Seat getSeat(ApplicationDbContext context, int seatID)
        {
            Seat? seat = context.Seats.Where(x => x.seatID == seatID).FirstOrDefault();
            return seat;
        }
        public static void deleteSeat(ApplicationDbContext context, int seatID)
        {
            Seat? seat = context.Seats.Where(x => x.seatID == seatID).FirstOrDefault();
            if (seat == null)
            {
                Console.WriteLine("Please check your inputs");
                return;
            }
            seat.isActive = false;
            context.SaveChanges();
        }
    }
}
