using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Services
{
    public class ReservationService
    {
       public static void addReservation(ApplicationDbContext context,User user, Posting posting, Seat seat)
       {
            Reservation newReservation = new Reservation {
                userID           = user.userID,
                seatID           = seat.seatID,
                postingID        = posting.postingID,
                reservationDate  = DateTime.Now,
            };
            context.Reservations.Add(newReservation);
            context.SaveChanges();
       }

        public static void deleteReservation(ApplicationDbContext context, User user, int  reservationID)
        {
            context.Users.Attach(user);
            Reservation? reservation = context.Reservations.Where(x => x.reservationID == reservationID && x.userID == user.userID & x.isActive == true).SingleOrDefault();
            if (reservation == null)
            {
                Console.WriteLine("Please check your input!");
                return;
            }
            if (reservation.posting.operationDate < DateTime.Now)
            {
                Console.WriteLine("You can't delete a reservation following the start of the show.");
                return;
            }
            reservation.isActive = false;
            user.deposit         = user.deposit + reservation.posting.operationFee;
            context.SaveChanges();
            Console.WriteLine($"Reservation with ID {reservationID} was deleted successfully and paid fees were refunded");
        }

        public static bool canMakeReservation(ApplicationDbContext context, User user, Posting posting, Seat seat)
        {
            int filmDuration = context.Films.Join(context.Postings,
                f => f.filmID,
                p => p.filmID,
                (f, p) => new
                {
                    filmDuration = f.slotCount,
                    PostingID = p.postingID
                }).Where(x => x.PostingID == posting.postingID).Select(x=>x.filmDuration).FirstOrDefault(); 
            bool reservationCheck = context.Postings.Join(
               context.Reservations,
               p => p.postingID,
               r => r.postingID,
               (p, r) => new {
                   reservingUserID     = r.userID,
                   isReservationActive = r.isActive,
                   premierDate         = p.operationDate,
                   SeatID              = r.seatID,
                   FilmID              = p.filmID,
                   PostingID           = p.postingID
               })
               .Join(
               context.Films,
               pr => pr.FilmID,
               f => f.filmID,
               (pr, f) => new {
                   ReservingUserID     = pr.reservingUserID,
                   filmDuration        = f.slotCount,
                   IsReservationActive = pr.isReservationActive,
                   PremierDate         = pr.premierDate,
                   Seatid              = pr.SeatID,
                   Postingid           = pr.PostingID
               }
               ).Where(x =>
               ((x.ReservingUserID == user.userID) && ((x.PremierDate < posting.operationDate && posting.operationDate < x.PremierDate.AddHours(x.filmDuration) && x.PremierDate.AddHours(x.filmDuration) < posting.operationDate.AddHours(filmDuration))
               || (posting.operationDate < x.PremierDate && posting.operationDate < x.PremierDate.AddHours(x.filmDuration) && posting.operationDate.AddHours(filmDuration) < x.PremierDate.AddHours(x.filmDuration))
               || (x.PremierDate < posting.operationDate && posting.operationDate.AddHours(filmDuration) < x.PremierDate.AddHours(x.filmDuration)))
               ) || (x.Postingid == posting.postingID && x.Seatid == seat.seatID && x.IsReservationActive == true)).Any();
            return !reservationCheck;
        }
        public static List<Reservation> getAllReservations(ApplicationDbContext context)
        {
            return context.Reservations.ToList();
        }

        public static List<Reservation> getUserReservations(ApplicationDbContext context, User user)
        {
            return context.Reservations.Where(x=>x.userID == user.userID).ToList();
        }
        public static List<Reservation> getPendingUserReservations(ApplicationDbContext context, User user)
        {
            return context.Reservations.Where(x => x.userID == user.userID && DateTime.Now <x.posting.operationDate).ToList();
        }

        public static List<int> getPendingUserReservationsID(ApplicationDbContext context, User user)
        {
            return context.Reservations.Where(x => x.userID == user.userID && DateTime.Now < x.posting.operationDate).Select(g=>g.reservationID).ToList();
        }

        public static void resolveReservations(ApplicationDbContext context, User user)
        {
            if (user.isAdmin == true)
            {
                List<int> reservations = getPendingUserReservationsID(context, user);
                List<int> postings     = PostingService.getPendingPostings(context, user);
                foreach(int reservation in reservations)
                {
                    deleteReservation(context,user, reservation);
                }
                foreach (int posting in postings)
                {
                    PostingService.deletePosting(context, posting);
                }
            }
            else
            {
                List<int> postings = PostingService.getPendingPostings(context, user);
                foreach (int posting in postings)
                {
                    PostingService.deletePosting(context, posting);
                }
            }
        }

    }
}
