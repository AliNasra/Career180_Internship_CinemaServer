using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Services;
using ConsoleApp1.Models;

namespace ConsoleApp1.Helpers
{
    class ReservationMenu
    {
        
        private string[] NormalOperationList = { "Inquire about your current deposit", "Deposit money", "List current film postings", "Make a reservation", "Delete a reservation", "List current film reservations", "List watched movies", "List Operations", "Delete account", "Exit"};
        private string[] AdminOperationList  = { "Inquire about your current deposit", "Deposit money", "List current film postings", "Make a reservation", "Delete a reservation", "List current film reservations", "List watched movies", "Add a cinema", "Add a film", "Make a film posting", "Remove a cinema", "Remove a film", "Remove a film posting", "List Operations", "Delete Account", "Exit" };

        public void printNormalOptionList()
        {
            for (int i = 1; i <= this.NormalOperationList.Length; i++)
            {
                Console.WriteLine($"{i}-> {NormalOperationList[i - 1]}");
            }

        }

        public void printAdminOptionList()
        {
            for (int i = 1; i <= this.AdminOperationList.Length; i++)
            {
                Console.WriteLine($"{i}-> {AdminOperationList[i - 1]}");
            }

        }

        string[] readUserCredentials()
        {
            Console.WriteLine("Please Enter your credentials");
            Console.WriteLine("Username:");
            string userName = Console.ReadLine();
            Console.WriteLine("Password:");
            string password = Console.ReadLine();
            string[] userData = { userName, password };
            return userData;
        }

        string[] readUserRegistrationCredentials()
        {
            Console.WriteLine("Please Enter your credentials!");
            Console.WriteLine("Username:");
            string userName = Console.ReadLine().Trim();
            if (!VerificationService.isValidUserName(userName))
            {
                Console.WriteLine("The username should be a single word!");
                return null;
            }
            Console.WriteLine("Password:");
            string password = Console.ReadLine().Trim();
            if (!VerificationService.isValidPassword(password))
            {
                Console.WriteLine("Invalid Password!");
                return null;
            }
            Console.WriteLine("Email:");
            string email = Console.ReadLine().Trim();
            if (!VerificationService.isValidEmail(email))
            {
                Console.WriteLine("Invalid Email!");
                return null;
            }
            Console.WriteLine("Birthdate in MM/DD/YYYY format:");
            string birthdate = Console.ReadLine().Trim();
            if (!VerificationService.isValidBirthDate(birthdate))
            {
                Console.WriteLine("Invalid Birth Date Entry");
                return null;
            }
            Console.WriteLine("User Category\n  Admin-> 1:\n  Ordinary->2");
            string userCategory = Console.ReadLine().Trim();
            if (!VerificationService.isValidCategory(userCategory))
            {
                Console.WriteLine("Invalid User Category");
                return null;
            }
            string[] userData = { userName, password, email, birthdate, userCategory };
            return userData;
        }
        int handleMainMenuOptions()
        {
            string inputedOption = Console.ReadLine();
            if (inputedOption == "1")
            {
                return 1;
            }
            else if (inputedOption == "2")
            {
                return 2;
            }
            else if (inputedOption == "3")
            {
                return 3;
            }
            else
            {
                return 0;
            }
        }
        

        async Task handleLogin()
        {
            string[] loginData = readUserCredentials();
            using (var context = new ApplicationDbContext())//connectionString
            {
                User loggedInUser = UserService.checkUser(context,loginData[0], loginData[1]);

                if (loggedInUser != null)
                {
                    if (loggedInUser.isAdmin)
                    {
                        await processVIPUser(loggedInUser);
                    }
                    else
                    {
                        processOrdinaryUser(loggedInUser);
                    }
                }
                else
                {
                    Console.WriteLine("Check Your Credentials");
                }
            }           
        }

        void handleSignUp()
        {
            string[] registeredData = readUserRegistrationCredentials();
            if (registeredData == null)
            {
                Console.WriteLine("Registration Failed!");
                return;
            }
            using (var context = new ApplicationDbContext())//connectionString
            {
                if (UserService.checkUser(context, registeredData[0]))
                {
                    if (registeredData[4] == "1")
                    {
                        UserService.registerAdmin(context,registeredData[0], registeredData[1], registeredData[2], registeredData[3]);
                    }
                    else
                    {
                        UserService.registerOrdinaryUser(context,registeredData[0], registeredData[1], registeredData[2], registeredData[3]);
                    }

                }
                else
                {
                    Console.WriteLine("Please pick a unique username");
                }
            }         
        }

        public async Task handleUser()
        {
            while (true)
            {
                Console.WriteLine("Select Operation\n1- Register\n2- Login\n3- Exit");
                int selectedOption = handleMainMenuOptions();
                if (selectedOption == 1)
                {

                    handleSignUp();
                }
                else if (selectedOption == 2)
                {
                    await handleLogin();
                }
                else if (selectedOption == 3)
                {

                    return;
                }
                else
                {
                    Console.WriteLine("Please insert a proper input");
                }


            }
        }

        #region Processing Normal Users
        async Task processOrdinaryUser(User loggedinUser)
        {
            using (var context = new ApplicationDbContext())//connectionString
            {
                UserService.signIn(context, loggedinUser);
                while (true)
                {
                    printNormalOptionList();
                    string option = Console.ReadLine();
                    if (option == "1")
                    {
                        UserService.getDepositInfo(context, loggedinUser);
                    }
                    else if (option == "2")
                    {
                        double numericalDepositInput = VerificationService.getPositiveDoubleInput("Enter the amount to be deposited:");
                        if (numericalDepositInput < 0) {
                            Console.WriteLine("Please insert a positive number!");
                        }
                        else
                        {
                            UserService.depositMoney(context,loggedinUser, numericalDepositInput);
                        }
                    }
                    else if (option == "3")
                    {
                        UserService.listValidPostings(context, loggedinUser);
                    }
                    else if (option == "4")
                    {
                        int numericalPostingID = VerificationService.getPositiveIntegerInput("Enter the posting's ID:");
                        int numericalSeatID    = VerificationService.getPositiveIntegerInput("Enter the seat's ID:");
                        if (numericalPostingID < 0 || numericalSeatID < 0)
                        {
                            Console.WriteLine("Please insert a positive integer!");
                        }
                        else
                        {
                            UserService.makeReservation(context, loggedinUser, numericalPostingID, numericalSeatID);
                        }
                    }
                    else if (option == "5")
                    {
                        int numericalReservationID = VerificationService.getPositiveIntegerInput("Enter the reservation's ID:");
                        if (numericalReservationID < 0)
                        {
                            Console.WriteLine("Please insert a positive integer!");
                        }
                        else
                        {
                            UserService.deleteReservation(context, loggedinUser, numericalReservationID);
                        }
                    }
                    else if (option == "6")
                    {
                        UserService.listCurrentReservations(context, loggedinUser);
                    }
                    else if (option == "7")
                    {
                        UserService.listWatchedFilms(context, loggedinUser);
                    }
                    else if (option == "8")
                    {
                        UserService.listOperations(context, loggedinUser);
                    }
                    else if (option == "9")
                    {
                        bool result = await UserService.deleteUser(context, loggedinUser);
                        if (result == true)
                        {
                            break;
                        }

                    }
                    else if (option == "10")
                    {
                        UserService.signOut(context, loggedinUser);
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Please insert an input between 1 & {NormalOperationList.Length}");
                    }
                }
            }
        }
        #endregion
        #region Processing Admin Users
        async Task processVIPUser(User loggedinUser)
        {
            using (var context = new ApplicationDbContext())//connectionString
            {
                UserService.signIn(context, loggedinUser);
                while (true)
                {
                    printAdminOptionList();
                    string option = Console.ReadLine();
                    if (option == "1")
                    {
                        UserService.getDepositInfo(context, loggedinUser);
                    }
                    else if (option == "2")
                    {
                        double numericalDepositInput = VerificationService.getPositiveDoubleInput("Enter the amount to be deposited:");
                        if (numericalDepositInput < 0)
                        {
                            Console.WriteLine("Please insert a positive number!");
                        }
                        else
                        {
                            UserService.depositMoney(context, loggedinUser, numericalDepositInput);
                        }
                    }
                    else if (option == "3")
                    {
                        UserService.listValidPostings(context, loggedinUser);
                    }
                    else if (option == "4")
                    {
                        int numericalPostingID = VerificationService.getPositiveIntegerInput("Enter the posting's ID:");
                        int numericalSeatID    = VerificationService.getPositiveIntegerInput("Enter the seat's ID:");
                        if (numericalPostingID < 0 || numericalSeatID < 0)
                        {
                            Console.WriteLine("Please insert a positive integer!");
                        }
                        else
                        {
                            UserService.makeReservation(context, loggedinUser, numericalPostingID, numericalSeatID);
                        }
                    }
                    else if (option == "5")
                    {
                        int numericalReservationID = VerificationService.getPositiveIntegerInput("Enter the reservation's ID:");
                        if (numericalReservationID < 0)
                        {
                            Console.WriteLine("Please insert a positive integer!");
                        }
                        else
                        {
                            UserService.deleteReservation(context, loggedinUser, numericalReservationID);
                        }
                    }
                    else if (option == "6")
                    {
                        UserService.listCurrentReservations(context, loggedinUser);
                    }
                    else if (option == "7")
                    {
                        UserService.listWatchedFilms(context, loggedinUser);
                    }
                    else if (option == "8")
                    {
                        Console.WriteLine("Insert the cinema's name:");
                        string cinemaName = Console.ReadLine();
                        int hallCount = VerificationService.getPositiveIntegerInput("Enter the hall count:");
                        if (hallCount < 1) {
                            Console.WriteLine("Please insert a valid integer");
                            break;
                        }
                        List<int> hallCapacities = new List<int>();
                        Console.WriteLine("Please insert each hall's capacity:");
                        bool checkCapacityConsistency = true;
                        for (int hallCounter = 0; hallCounter < hallCount; hallCounter++)
                        {
                            int hallcapacity = VerificationService.getPositiveIntegerInput($"Hall No.{hallCounter + 1}:");
                            if (hallcapacity <1)
                            {
                                Console.WriteLine("Please insert a valid integer");
                                checkCapacityConsistency = false;
                                break;
                            }
                            else
                            {
                                hallCapacities.Add(hallcapacity);
                            }
                        }
                        if (checkCapacityConsistency == true)
                        {
                            UserService.addCinema(context, loggedinUser,cinemaName,hallCount,hallCapacities);
                        }
                    }
                    else if (option == "9")
                    {
                        Console.WriteLine("Insert the film's name:");
                        string filmName         = Console.ReadLine();
                        Console.WriteLine("Insert the film's description:");
                        string filmDescription  = Console.ReadLine();
                        Console.WriteLine("Insert the film's genre:");
                        string filmType         = Console.ReadLine();
                        Console.WriteLine("Insert the film's duration in hours:");
                        double filmDuration     = VerificationService.getPositiveDoubleInput();
                        if (filmDuration <= 0)
                        {
                            Console.WriteLine("Please insert a valid duration");
                        }
                        else
                        {
                            UserService.addFilm(context, loggedinUser, filmName, filmDescription, filmType,(int)Math.Ceiling(filmDuration));
                        }                      
                    }
                    else if (option == "10")
                    {
                        int filmID            = VerificationService.getPositiveIntegerInput("Enter the film's ID:");
                        if (filmID >= 0)
                        {
                            int cinemaID = VerificationService.getPositiveIntegerInput("Enter the cinema's ID:");
                            if (cinemaID >= 0)
                            {
                                int hallID = VerificationService.getPositiveIntegerInput("Enter the hall's ID:");
                                if (hallID >= 0)
                                {
                                    Console.WriteLine("Insert the premier time:");
                                    DateTime premierTime = VerificationService.readPremierDate();
                                    if (premierTime > DateTime.MinValue)
                                    {
                                        double reservationFee = VerificationService.getPositiveDoubleInput("Enter the registration's fee:");
                                        if (reservationFee >= 0)
                                        {
                                            UserService.makePosting(context, loggedinUser, filmID, cinemaID, hallID, premierTime, reservationFee);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Please insert a valid fee!");
                                        }                                      
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please insert a valid time!");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Please insert a valid integer value!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please insert a valid integer value!");
                            }                           
                        }
                        else
                        {
                            Console.WriteLine("Please insert a valid integer value!");
                        }
                    }
                    else if (option == "11")
                    {
                        int cinemaID = VerificationService.getPositiveIntegerInput("Enter the cinema's ID:");
                        if (cinemaID < 0)
                        {
                            Console.WriteLine("Please insert a integer");
                        }
                        else
                        {
                            UserService.removeCinema(context, loggedinUser, cinemaID);
                        }
                    }
                    else if (option == "12")
                    {
                        int filmID = VerificationService.getPositiveIntegerInput("Enter the film's ID:");
                        if (filmID < 0)
                        {
                            Console.WriteLine("Please insert a integer");
                        }
                        else
                        {
                            UserService.removeFilm(context,loggedinUser, filmID);
                        }
                    }
                    else if (option == "13")
                    {
                        int postingID = VerificationService.getPositiveIntegerInput("Enter the post's ID:");
                        if (postingID < 0)
                        {
                            Console.WriteLine("Please insert a integer");
                        }
                        else
                        {
                            UserService.deletePosting(context, loggedinUser, postingID);
                        }
                    }
                    else if (option == "14")
                    {
                        UserService.listOperations(context, loggedinUser);
                    }
                    else if (option == "15")
                    {
                        bool result = await UserService.deleteUser(context, loggedinUser);
                        if (result == true)
                        {
                            break;
                        }
                    }
                    else if (option == "16")
                    {
                        UserService.signOut(context, loggedinUser);
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Please insert an input between 1 & {AdminOperationList.Length}");
                    }
                }
            }
        }
        #endregion
        public static async Task<string?> ReadInputAsync(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    if (Console.KeyAvailable)
                    {
                        return Console.ReadLine();
                    }
                    Thread.Sleep(50);
                }
                return null;
            }, token);
        }


    }
}
