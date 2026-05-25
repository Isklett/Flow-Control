using EntertainmentVenue.Cinema;

namespace Flow_Control
{
    internal class Program
    {
        private static bool _running = true;

        private static Cinema.Movie[] _allMovies = CinemaMockData.GetAllAvailableMovies();
        private static Cinema.Screening[] _allScreenings = CinemaMockData.GetAllScreenings();

        private static Cinema.Saloon[] _bkSaloons = { CinemaMockData.Saloon1, CinemaMockData.Saloon2 };
        private static Cinema.Saloon[] _bpSaloons = { CinemaMockData.Saloon1 };

        private static Cinema[] _cinemas = { new Cinema("Filmstaden Bergakungen", _bkSaloons), new Cinema("Biopalatset", _bpSaloons) };

        static void Main(string[] args)
        {
            while (_running)
            {
                MainMenu();
            }
            Environment.Exit(0);
        }

        private static void MainMenu()
        {
            Console.WriteLine("\nWelcome to the main menu! Please choose an option by typing in desired number.");
            Console.WriteLine("0: Quit.");
            Console.WriteLine("1: Check price category by age.");
            Console.WriteLine("2: Calculate total price for a group.");
            Console.WriteLine("3: Summon 10 parrots. (repeats your input)");
            Console.WriteLine("4: Locate the third word in a sentence. (requires 3+ words to work)");
            Console.WriteLine("5: Enter booking system");
            string choice = Console.ReadLine() ?? "";
            bool isValid = int.TryParse(choice, out int choiceNr);
            if(isValid)
            {
                switch (choiceNr)
                {
                    case 0:
                        Quit();
                        break;
                    case 1:
                        AgeCheck();
                        break;
                    case 2:
                        CalculateGroupPrice();
                        break;
                    case 3:
                        SummonParrots();
                        break;
                    case 4:
                        LocateThirdWord();
                        break;
                    case 5:
                        BookingSystem();
                        break;
                    default:
                        Console.WriteLine("Invalid option, press any key to return to the main menu.");
                        Console.ReadLine();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid option, press any key to return to the main menu.");
                Console.ReadLine();
            }
        }

        private static void Quit()
        {
            _running = false;
        }


        /// <summary>
        /// Returns age group depending on the age of the user.
        /// </summary>
        /// <param name="userFeedback">
        /// If true, prints the price category and price to the console.
        /// If false, only returns the price category.
        /// </param>
        /// <returns></returns>
        private static Cinema.PriceCategory AgeCheck(bool userFeedback = true)
        {
            if (userFeedback) Console.Write("Age:");
            string input = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid input, please enter a valid age.");
                AgeCheck(userFeedback);
            }
            else
            {
                bool validAge = uint.TryParse(input, out uint age);
                if (validAge)
                {
                    if (age < 5)
                    {
                        if (userFeedback)
                        {
                            Console.WriteLine($"Age {age} is eligible to watch the movie for free if accompanied by an adult.");
                        }
                        return Cinema.PriceCategory.Child;
                    }
                    else if (age >= 5 && age < 20)
                    {
                        if (userFeedback)
                        {
                            Console.WriteLine($"Age {age} is eligible for the youth price: {Cinema.GetPrice(Cinema.PriceCategory.Youth)} kr.");
                        }
                        return Cinema.PriceCategory.Youth;
                    }
                    else if (age >= 64 && age < 100)
                    {
                        if (userFeedback)
                        {
                            Console.WriteLine($"Age {age} is eligible for the senior price: {Cinema.GetPrice(Cinema.PriceCategory.Senior)} kr.");
                        }
                        return Cinema.PriceCategory.Senior;
                    }
                    else if (age >= 100)
                    {
                        if (userFeedback)
                        {
                            Console.WriteLine($"Age {age} is eligible to watch the movie for free.");
                        }
                        return Cinema.PriceCategory.Centurion;
                    }
                    else
                    {
                        if (userFeedback)
                        {
                            Console.WriteLine($"Age {age} is eligible for the adult price: {Cinema.GetPrice(Cinema.PriceCategory.Adult)} kr.");
                        }
                        return Cinema.PriceCategory.Adult;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter a valid age.");
                    AgeCheck();
                }
            }
            return Cinema.PriceCategory.Adult;
        }


        /// <summary>
        /// Lets the user input the number of people in their group and then the age of each person. It will then calculate the total price for the group based on the age categories of each person and print it out to the console.
        /// </summary>
        private static void CalculateGroupPrice()
        {
            Console.Write("\nHow many people are you? ");
            bool validNrOfTickets = uint.TryParse(Console.ReadLine() ?? "", out uint nrOfTickets);
            if (validNrOfTickets)
            {
                Cinema.Ticket[] tickets = new Cinema.Ticket[nrOfTickets];

                for (int i = 0; i < nrOfTickets; i++)
                {
                    Console.Write($"Please enter the age of person {i + 1}: ");
                    Cinema.PriceCategory priceCategory = AgeCheck(false);
                    tickets[i] = new Cinema.Ticket(priceCategory);
                    decimal ticketPrice = tickets[i].Price;
                    Console.WriteLine($"Ticket of category {Cinema.GetCategoryAsString(priceCategory)} has been added.");
                }
                Console.WriteLine($"The total price for {tickets.Length} tickets is: {Cinema.Ticket.TotalTicketPrice(tickets)} kr.");
            }
            else
            {
                Console.WriteLine("Invalid input, please enter a valid number of tickets.");
                CalculateGroupPrice();
            }
        }


        /// <summary>
        /// Lets the user type in a sentence and then repeats that sentence 10 times.
        /// </summary>
        private static void SummonParrots()
        {
            Console.Write("\nWhat do you want the parrots to say? ");
            string input = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(input))
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.Write($"Parrot {i + 1} says: {input}, ");
                }
            }
            else
            {
                Console.WriteLine("Invalid input, there was nothing the parrots could repeat.");
            }
        }


        /// <summary>
        /// Lets the user type in a sentence and then locates the third word in that sentence and prints it out to the console.
        /// </summary>
        private static void LocateThirdWord()
        {
            Console.Write("\nPlease enter a sentence with at least three words: ");
            var input = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (words.Length >= 3)
                {
                    Console.WriteLine($"The third word in the sentence is: {words[2]}.");
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter a sentence with at least three words.");
                    LocateThirdWord();
                }
            }
            else
            {
                Console.WriteLine("Invalid input, please enter some text.");
                LocateThirdWord();
            }
        }


        /// <summary>
        /// Initiates the booking system
        /// </summary>
        private static void BookingSystem()
        {
            Cinema selectedCinema = _cinemas[0];
            Cinema.Saloon selectedSaloon = _bkSaloons[0];
            Cinema.Movie selectedMovie = _allMovies[0];
            Cinema.Screening selectedScreening = _allScreenings[0];
            int currentPage = 0;
            ListCinemas();

            void ListCinemas()
            {
                currentPage = 0;
                Console.WriteLine("\nWelcome to the booking system! Please choose a location by typing in the corresponding number.");
                Console.WriteLine("0: Back");
                for (int i = 0; i < _cinemas.Length; i++)
                {
                    Console.WriteLine($"{i+1}: {_cinemas[i].Name}");
                }
                string choice = Console.ReadLine() ?? "";
                bool validChoice = int.TryParse(choice, out int cinemaNr);
                if(validChoice && cinemaNr > 0 && cinemaNr <= _cinemas.Length)
                {
                    selectedCinema = _cinemas[cinemaNr - 1];
                    Console.WriteLine($"You have selected {selectedCinema.Name}. Here are the movies currently showing:");
                    ListMovies(selectedCinema);
                }
                else if (validChoice && cinemaNr == 0)
                {
                    Back();
                }
                else
                {
                    Console.WriteLine("Invalid input, please select a valid cinema number.");
                    ListCinemas();
                }
            }

            void ListMovies(Cinema cinema)
            {
                currentPage = 1;
                Cinema.Movie[] movieList = cinema.GetMovies();
                Console.WriteLine("0: Back");
                for (int i = 0; i < movieList.Length; i++)
                {
                    Console.WriteLine($"{i+1}: {movieList[i].Title}");
                }
                string choice = Console.ReadLine() ?? "";
                bool validChoice = int.TryParse(choice, out int movieNr);
                if (validChoice && movieNr > 0 && movieNr <= movieList.Length)
                {
                    selectedMovie = movieList[movieNr - 1];
                    Console.WriteLine($"You have selected {selectedMovie.Title}. Here are the saloons showing that movie:");
                    ListSaloons(selectedMovie);
                }
                else if (validChoice && movieNr == 0)
                {
                    Back();
                }
                else
                {
                    Console.WriteLine("Invalid input, please select a valid cinema number.");
                    ListMovies(selectedCinema);
                }
            }

            void ListSaloons(Cinema.Movie selectedMovie)
            {
                currentPage = 2;
                Cinema.Saloon[] saloonsShowingMovie = selectedCinema.GetSaloonsShowingMovie(selectedMovie);
                Console.WriteLine("0: Back");
                for (int i = 0; i < saloonsShowingMovie.Length; i++)
                {
                    Console.WriteLine($"{i + 1}: {saloonsShowingMovie[i].Name}");
                }
                string choice = Console.ReadLine() ?? "";
                bool validChoice = int.TryParse(choice, out int saloonNr);
                if (validChoice && saloonNr > 0 && saloonNr <= saloonsShowingMovie.Length)
                {
                    selectedSaloon = saloonsShowingMovie[saloonNr - 1];
                    Console.WriteLine($"You have selected {selectedSaloon.Name}. Here are the available screenings:");
                    ListScreenings(selectedSaloon);

                }
                else if(validChoice && saloonNr == 0)
                {
                    Back();
                }
                else
                {
                    Console.WriteLine("Invalid input, please select a valid saloon number.");
                    ListSaloons(selectedMovie);
                }
            }

            void ListScreenings(Cinema.Saloon saloon)
            {
                currentPage = 3;
                Cinema.Screening[] movieScreenings = selectedCinema.GetScreeningsForMovieInSaloon(selectedMovie, saloon);
                Console.WriteLine("0: Back");
                for (int i = 0; i < movieScreenings.Length; i++)
                {
                    Console.WriteLine($"{i + 1}: {movieScreenings[i].StartTime}");
                }
                string choice = Console.ReadLine() ?? "";
                bool validChoice = int.TryParse(choice, out int saloonNr);
                if (validChoice && saloonNr > 0 && saloonNr <= movieScreenings.Length)
                {
                    selectedScreening = movieScreenings[saloonNr - 1];
                    Console.WriteLine($"You have selected {selectedScreening.StartTime}.");
                    BookSeat();
                }
                else if (validChoice && saloonNr == 0)
                {
                    Back();
                }
                else
                {
                    Console.WriteLine("Invalid input, please select a valid saloon number.");
                    ListSaloons(selectedMovie);
                }
            }

            void BookSeat(bool displaySeats = true)
            {
                currentPage = 4;
                if(displaySeats)
                    selectedScreening.DisplaySeatingArrangement();
                Console.WriteLine("Please enter the seat you would like to book (row, column) or type 0 to go back to previous menu:");
                string input = Console.ReadLine() ?? "";
                string[] seatInput = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
                if (seatInput[0] == "0")
                {
                    Back();
                    return;
                }
                else if (seatInput.Length == 2)
                {
                    bool validRow = uint.TryParse(seatInput[0], out uint row);
                    bool validColumn = uint.TryParse(seatInput[1], out uint column);
                    if (validRow && validColumn || row == 0 || column == 0)
                    {
                        bool bookingSuccessful = selectedScreening.OccupySeat(row - 1, column - 1);
                        if (bookingSuccessful)
                        {
                            Console.WriteLine($"You have successfully booked seat ({row}, {column}) for the screening at {selectedScreening.StartTime} for the movie {selectedMovie.Title} in {selectedSaloon.Name}.");
                            BookingSystem();
                        }
                        else
                        {
                            Console.WriteLine($"Sorry, seat ({row}, {column}) is already occupied. Please choose another seat.");
                            BookSeat(false);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please enter a valid seat in the format 'row,column'.");
                        BookSeat(false);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter a valid seat in the format 'row,column'.");
                    BookSeat();
                }
            }

            //Returns to previous page
            void Back()
            {
                switch(currentPage)
                {
                    case 0:
                        MainMenu();
                        break;
                    case 1:
                        ListCinemas();
                        break;
                    case 2:
                        ListMovies(selectedCinema);
                        break;
                    case 3:
                        ListSaloons(selectedMovie);
                        break;
                    case 4:
                        ListScreenings(selectedSaloon);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
