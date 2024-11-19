using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Services
{
    public class FilmService
    {
        public static void addFilm(ApplicationDbContext context, User Admin, string filmTitle, string filmDescription, string filmType, int filmDuration)
        {
            Film newFilm = new Film
            {
                filmTitle       = filmTitle,
                description     = filmDescription,
                filmType        = filmType,
                slotCount       = filmDuration,
                insertingAdminID = Admin.userID,
            };
            context.Films.Add(newFilm);
            context.SaveChanges();
        }

        public static void removeFilm(ApplicationDbContext context, int filmID)
        {
            Film film = context.Films.Where(x=>x.filmID == filmID).FirstOrDefault();
            if (film != null) {
                film.isActive = false;
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("The film wasn't found!");
            }
        }
        public static void getWatchedFilms(ApplicationDbContext context, User user)
        {
            DateTime currentTime = DateTime.Now;
            var watchedFilms = context.Reservations.Join(
                context.Postings,
                r => r.postingID,
                p => p.postingID,
                (r, p) => new { r, p }).
                Join(
                context.Films,
                rp => rp.p.filmID,
                f => f.filmID,
                (rp, f) => new
                { rp, f })
                .Where(x => x.rp.r.isActive == true && x.rp.r.userID == user.userID && x.rp.p.operationDate.AddHours(x.f.slotCount) < currentTime)
                .GroupBy(x => x.f.filmTitle).ToDictionary(
                g => g.Key,
                g => g.Select(
                    x => new
                    {
                        x.rp.p.operationDate,
                        x.rp.p.cinema.cinemaName
                    }
                    )
                ).ToList();
            foreach (var film in watchedFilms)
            {
                Console.WriteLine($"{film.Key}");
                foreach (var filmDetail in film.Value)
                {
                    Console.WriteLine($"Watched in {filmDetail.cinemaName} on {(DateTime)filmDetail.operationDate:dddd, MMMM d, yyyy h:mm tt}");
                }
            }
        }
        public static Film getFilm(ApplicationDbContext context, int filmID)
        {
            Film? film = context.Films.Where(x => x.filmID == filmID).FirstOrDefault();
            return film;
        }
        public static void deleteFilm(ApplicationDbContext context, int filmID)
        {
            Film? film = context.Films.Where(x => x.filmID == filmID).FirstOrDefault();
            if (film == null)
            {
                Console.WriteLine("Please check your input");
                return;
            }
            var postings = context.Postings.Where(x => x.filmID == filmID && x.isActive == true && x.operationDate < DateTime.Now).Select(g=>g.postingID).ToList();
            foreach (var postingID in postings)
            {
                PostingService.deletePosting(context, postingID);
            }
        }

    }
}
