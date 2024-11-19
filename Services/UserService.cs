using Azure;
using ConsoleApp1.Helpers;
using ConsoleApp1.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Services
{
    public class UserService
    {
        public static List<User> getOrdinaryUsers(ApplicationDbContext context)
        {
            List<User> ordinaryCustomers = context.Users.Where(x => x.isAdmin != true).ToList();
            return ordinaryCustomers;
        }
        public static List<User> getAdmins(ApplicationDbContext context)
        {
            List<User> VIPCustomers = context.Users.Where(x => x.isAdmin == true).ToList();
            return VIPCustomers;
        }

        public static User retrieveUser(ApplicationDbContext context, String username)
        {
            User? user = context.Users.SingleOrDefault(u => u.userName == username);
            return user;
        }
        public static User retrieveUser(ApplicationDbContext context, int userID)
        {
            User? customer = context.Users.SingleOrDefault(u => u.userID == userID);
            return customer;
        }
        public static User checkUser(ApplicationDbContext context, string username, string password)
        {

            User? user = context.Users.SingleOrDefault(u => u.userName == username);
            if (user == null)
            {
                return null;
            }
            else
            {
                bool checkPassword = EncryptionServices.DecryptPassword(user.password) == password ? true : false;
                return (checkPassword ? user : null);
            }
        }
        public static bool checkUser(ApplicationDbContext context, string username)
        {
            bool checkCustomerExistence = context.Users.Any(u => u.userName == username && u.isActive == true);
            return !checkCustomerExistence;
        }
        public static void registerAdmin(ApplicationDbContext context, string userName, string password, string email, string birthdate)
        {
            string encryptedPassword = EncryptionServices.EncryptPassword(password);
            DateTime birthDate = DateTime.Parse(birthdate);
            User adminUSer = new User
            {
                userName = userName,
                password = encryptedPassword,
                email = email,
                birthDate = birthDate,
                isAdmin = true

            };
            context.Users.Add(adminUSer);
            context.SaveChanges();
            OperationService.addOperation(context, adminUSer.userID, "SignUp");
            Console.WriteLine("Registration Completed Successfully");
        }
        public static void registerOrdinaryUser(ApplicationDbContext context, string userName, string password, string email, string birthdate)
        {
            string encryptedPassword = EncryptionServices.EncryptPassword(password);
            DateTime birthDate = DateTime.Parse(birthdate);
            User ordinaryUser = new User
            {
                userName = userName,
                password = encryptedPassword,
                email = email,
                birthDate = birthDate,
                isAdmin = false,
            };
            context.Users.Add(ordinaryUser);
            context.SaveChanges();
            OperationService.addOperation(context, ordinaryUser.userID, "SignUp");
        }

        public static void signIn(ApplicationDbContext context, User user)
        {
            OperationService.addOperation(context, user.userID, "SignIn");
            Console.WriteLine("Welcome On Board!");
        }
        public static void signOut(ApplicationDbContext context, User user)
        {
            OperationService.addOperation(context, user.userID, "SignOut");
            Console.WriteLine("Signed out successfully. Goodbye :)");
        }
        public static void getDepositInfo(ApplicationDbContext context, User user)
        {
            Console.WriteLine($"Your account has {user.deposit} dollars");
            OperationService.addOperation(context, user.userID, "DepositInquiry");
        }
        public static void depositMoney(ApplicationDbContext context, User user, double amount)
        {
            context.Users.Attach(user);
            user.deposit = user.deposit + amount;
            OperationService.addOperation(context, user.userID, "Deposit", amount);
            Console.WriteLine("Operation completed successfully!");
        }
        public static void listOperations(ApplicationDbContext context, User user)
        {
            OperationService.listOperation(context, user.userID);
            OperationService.addOperation(context, user.userID, "ListingOperations");
        }

        public static void listValidPostings(ApplicationDbContext context,User user)
        {
            List<Posting> postings = PostingService.getPendingPostings(context);
            foreach (Posting posting in postings)
            {
                Console.WriteLine(posting.toString());
            }
            OperationService.addOperation(context, user.userID, "ListingPostings");
        }

        public static void listWatchedFilms(ApplicationDbContext context, User user)
        {
            Dictionary<string, object> watchedFilms = new Dictionary<string, object>();
            FilmService.getWatchedFilms(context, user);
            OperationService.addOperation(context, user.userID, "ListingMovies");
        }
        public static void addFilm(ApplicationDbContext context, User admin, string filmTitle, string filmDescription, string filmType, int filmDuration)
        {
            FilmService.addFilm(context, admin, filmTitle, filmDescription, filmType, filmDuration);
            OperationService.addOperation(context, admin.userID, "AddingFilm");
            Console.WriteLine($"The film {filmTitle} was added successfully!");
        }
        public static void addCinema(ApplicationDbContext context, User admin, string cinemaName, int hallCount, List<int> hallCapacities)
        {
            CinemaService.addCinema(context, cinemaName, hallCount, hallCapacities);
            OperationService.addOperation(context, admin.userID, "AddingCinema");
        }
        public static void makePosting(ApplicationDbContext context, User user, int filmID, int cinemaID, int hallID, DateTime premierTime, double registrationFee)
        {
            Film film = FilmService.getFilm(context, filmID);
            Cinema cinema = CinemaService.getCinema(context, cinemaID);
            Hall hall = HallService.getHall(context, hallID);
            if (film == null || cinema == null || hall == null)
            {
                Console.WriteLine("Please check your input!");
            }
            if (PostingService.checkifCanPost(context, film, cinema, hall, premierTime))
            {
                PostingService.addPosting(context, user, film, cinema, hall, premierTime, registrationFee);
                OperationService.addOperation(context, user.userID, "MakingPosting");
            }
        }
        public static void makeReservation(ApplicationDbContext context, User user, int postingID,int seatID)
        {
            Posting posting = PostingService.getPosting(context, postingID);
            Seat seat       = SeatService.getSeat(context, seatID);
            if (posting == null || seat == null)
            {
                Console.WriteLine("Please check your input!");
            }
            if (ReservationService.canMakeReservation(context, user, posting,seat))
            {
                if (user.deposit < posting.operationFee)
                {
                    ReservationService.addReservation(context, user, posting, seat);
                    context.Users.Attach(user);
                    user.deposit = user.deposit - posting.operationFee;
                    context.SaveChanges();
                    OperationService.addOperation(context, user.userID, "MakingReservation");
                }
                else
                {
                    Console.WriteLine("Please check your balance!");
                }              
            }
            else
            {
                Console.WriteLine("Please check your input!");
            }
        }
        public static void listCurrentReservations(ApplicationDbContext context, User user) { 
            List<Reservation> reservations = ReservationService.getUserReservations(context, user);
            foreach (Reservation reservation in reservations)
            {
                Console.WriteLine(reservation.toString());
            }
            OperationService.addOperation(context, user.userID, "ListingPersonalReservations");
        }
        public static void removeFilm(ApplicationDbContext context, User user, int filmID)
        {
            FilmService.deleteFilm(context, filmID);
            OperationService.addOperation(context, user.userID, "DeletingFilm");
        }       
        public static void removeCinema(ApplicationDbContext context, User user, int cinemaID)
        {
            CinemaService.deleteCinema(context, cinemaID);
            OperationService.addOperation(context, user.userID, "DeletingCinema");
        }     
        public static void deletePosting(ApplicationDbContext context, User user, int postingID)
        {
            PostingService.deletePosting(context, postingID);
            OperationService.addOperation(context, user.userID, "DeletingPosting");
        }
        public static void deleteReservation(ApplicationDbContext context, User user , int reservationID)
        {
            ReservationService.deleteReservation(context,user, reservationID);
            OperationService.addOperation(context, user.userID, "DeletingReservation");
        }

        public static async Task<bool> deleteUser(ApplicationDbContext context, User user)
        {
            context.Users.Attach(user);
            int pendingReservations = ReservationService.getPendingUserReservationsID(context, user).Count;
            int waitSeconds = 30;
            string? userResponse = null;
            if (pendingReservations > 0)
            {
                Console.WriteLine($"You have {pendingReservations} pending transaction(s). Do you want to proceed? y/n");
                Console.WriteLine($"Please answer within {waitSeconds} seconds!");
                CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                userResponse = await ReservationMenu.ReadInputAsync(cts.Token);
                if (userResponse == null)
                {
                    Console.WriteLine($"Time expired! You didn't enter anything within {waitSeconds} seconds.");
                    return false;
                }
                else if (userResponse == "y")
                {
                }
                else if (userResponse == "n")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Please type either y or n to declare your decision");
                    return false;
                }
            }
            ReservationService.resolveReservations(context, user);
            user.isActive = false;
            context.SaveChanges();
            Console.WriteLine("Account Deleted Successfully");
            return true;
        }

    }
}
