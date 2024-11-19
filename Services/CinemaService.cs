using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Services
{
    public class CinemaService
    {
        public static void addCinema(ApplicationDbContext context, string cinemaName, int hallCount, List<int> hallCapacity)
        {
            if (!verifyCapacityInput(hallCount, hallCapacity)) { 
                Console.WriteLine("Please check your input!");
                return;
            }
            Cinema newCinema = new Cinema
            {
                cinemaName = cinemaName,
            };
            context.Cinemas.Add(newCinema);
            context.SaveChanges();
            for (int hallCounter = 0; hallCounter < hallCapacity.Count; hallCounter++)
            {
                HallService.addHall(context, newCinema.cinemaID, hallCapacity[hallCounter]);
            }
            Console.WriteLine($"The {cinemaName} cinema was added successfully");
        }
        public static Cinema getCinema(ApplicationDbContext context, int cinemaID)
        {
            Cinema? cinema = context.Cinemas.Where(x => x.cinemaID == cinemaID).FirstOrDefault();
            return cinema;
        }
        public static bool verifyCapacityInput(int hallCount, List<int> hallCapacities)
        {
            if (hallCount != hallCapacities.Count)
            {
                return false;
            }
            for (int hallCounter = 0; hallCounter < hallCount; hallCounter++) {
                if (hallCapacities.ElementAt(hallCounter) < 0)
                {
                    return false;
                }
            }
            return true;
        }
        public static void deleteCinema(ApplicationDbContext context, int cinemaID)
        {
            Cinema? cinema = context.Cinemas.Where(x => x.cinemaID == cinemaID).FirstOrDefault();
            if (cinema == null)
            {
                Console.WriteLine("Please check your input!");
            }
            var postings    = context.Postings.Where(x => x.isActive == true && DateTime.Now < x.operationDate && x.cinemaID == cinemaID).Select(g=>g.postingID).ToList();
            var halls       = context.Halls.Where(x => x.isActive == true && x.cinemaID == cinemaID).Select(g => g.hallID).ToList();
            foreach (var posting in postings)
            {
                PostingService.deletePosting(context, posting);
            }
            foreach (var hall in halls)
            {
                HallService.deleteHall(context, hall);
            }
        }
    }
}

