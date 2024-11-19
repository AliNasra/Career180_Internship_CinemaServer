using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Services
{
    public class PostingService
    {
        public static Posting getPosting(ApplicationDbContext context, int postingID)
        {
            Posting? posting = context.Postings.Where(x=>x.postingID == postingID).FirstOrDefault();
            return posting;
        }
        public static void addPosting(ApplicationDbContext context, User user, Film film, Cinema cinema, Hall hall, DateTime premierTime, double registrationFee)
        {
            Posting newPosting = new Posting
            {
                hallID        = hall.hallID,
                filmID        = film.filmID,
                cinemaID      = cinema.cinemaID,
                operationDate = premierTime,
                operationFee  = registrationFee,
                postingUserID = user.userID
            };
            context.Postings.Add(newPosting);
            context.SaveChanges();
        }
        public static List<int> checkavailableSeats(ApplicationDbContext context, Posting posting)
        {
            var availableSeats = context.Postings.Join(
                context.Seats,
                p=>p.hallID,
                s=>s.hallID,
                (p, s) => new { p, s }
                ).
                Where(x=>x.s.isActive == true && x.p.postingID == posting.postingID).
                OrderBy(x=>x.s.seatID).
                Select(z=>z.s.seatID).
                ToList();
            return availableSeats;
        }
        public static bool checkifCanPost(ApplicationDbContext context, Film film, Cinema cinema, Hall hall, DateTime premierTime)
        {
            var cinemaHallCheck = context.Halls.Where(x => x.cinemaID == cinema.cinemaID && x.hallID == hall.hallID && x.isActive == true && x.cinema.isActive == true).Any();
            if (!cinemaHallCheck) {
                return false;
            }
            var clashVariable = context.Postings.Join(
                context.Halls,
                p => p.hallID,
                h => h.hallID,
                (p, s) => new { 
                    premierDate     = p.operationDate,
                    HallID          = p.hallID,
                    FilmID          = p.filmID,
                    isPostingActive = p.isActive
                }
                )
                .Join(
                context.Films,
                ph => ph.FilmID,
                f  => f.filmID,
                (ph, f) => new { 
                    PremierDate     = ph.premierDate,
                    filmDuration    = f.slotCount,
                    hallid          = ph.HallID,
                    IsPostingActive = ph.isPostingActive
                }
                ).Where(x => (((x.PremierDate < premierTime && premierTime < x.PremierDate.AddHours(x.filmDuration) && x.PremierDate.AddHours(x.filmDuration) < premierTime.AddHours(film.slotCount)) 
                || ( premierTime < x.PremierDate && premierTime < x.PremierDate.AddHours(x.filmDuration) && premierTime.AddHours(film.slotCount) < x.PremierDate.AddHours(x.filmDuration) )
                || (x.PremierDate < premierTime && premierTime.AddHours(film.slotCount) < x.PremierDate.AddHours(x.filmDuration)))
                && (hall.hallID == x.hallid) && (x.IsPostingActive == true)))
                .Any();                
            return !clashVariable;
        }
        public static List<Posting> getPendingPostings(ApplicationDbContext context)
        {
            DateTime currentTime = DateTime.Now;
            var      postings    = context.Postings.Where(x=> x.operationDate > currentTime).ToList();
            return postings;
        }

        public static List<int> getPendingPostings(ApplicationDbContext context, User user)
        {
            DateTime currentTime = DateTime.Now;
            var postings = context.Postings.Where(x => x.operationDate > currentTime && x.postingUserID == user.userID).Select(g=>g.postingID).ToList();
            return postings;
        }
        public static void deletePosting(ApplicationDbContext context, int postingID)
        {
            Posting? posting = context.Postings.Where(x => x.postingID == postingID).SingleOrDefault();
            if (posting == null)
            {
                Console.WriteLine("Please check your input");
            }
            if (posting.operationDate < DateTime.Now)
            {
                Console.WriteLine("You can't delete a posting following the start of the show!");
            }
            List<Reservation> reservations = context.Reservations.Where(x => x.postingID == postingID && x.isActive == true && x.reservingUser.isActive == true).ToList();
            foreach (var reservation in reservations)
            {
                reservation.isActive              = false;
                reservation.reservingUser.deposit = reservation.reservingUser.deposit + posting.operationFee;
            }
            int reservationCount = reservations.Count;
            context.SaveChanges();
            Console.WriteLine($"Posting with ID {postingID} was deleted. {reservationCount} reservation{((reservationCount>1)?"s":"")} {((reservationCount > 1) ? "were" : "was")} deleted");
        }
    }
}
