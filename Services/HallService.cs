using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Services
{
    public class HallService
    {
        public static void addHall(ApplicationDbContext context, int cinemaID,int capacity)
        {
            Hall newHall = new Hall
            {
                cinemaID = cinemaID,
                capacity = capacity
            };
            context.Halls.Add(newHall);
            context.SaveChanges();
            for (int seatCounter = 0; seatCounter < capacity; seatCounter++)
            {
                SeatService.addSeat(context, newHall.hallID);
            }
        }
        public static Hall getHall(ApplicationDbContext context, int hallID)
        {
            Hall? hall = context.Halls.Where(x => x.hallID == hallID).FirstOrDefault();
            return hall;
        }
        public static void deleteHall(ApplicationDbContext context, int hallID)
        {
            Hall? hall = context.Halls.Where(x => x.hallID == hallID && x.isActive == true).SingleOrDefault();
            if (hall == null)
            {
                Console.WriteLine("Please check your inputs");
                return;
            }
            var seats = context.Seats.Where(x => x.hallID == hallID && x.isActive == true).Select(s => s.seatID).ToList();
            foreach (var seat in seats)
            {
                SeatService.deleteSeat(context, seat);
            }
        }

    }
}
