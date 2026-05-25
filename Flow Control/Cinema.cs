namespace EntertainmentVenue.Cinema
{
    internal class Cinema
    {
        public enum PriceCategory
        {
            Child = 0,
            Youth = 1,
            Adult = 2,
            Senior = 3,
            Centurion = 4
        }

        public struct Ticket
        {
            public PriceCategory Category { get; set; }
            public decimal Price { get; set; }

            public Ticket(PriceCategory category)
            {
                Category = category;
                Price = GetPrice(category);
            }

            public static decimal TotalTicketPrice(Ticket[] tickets)
            {
                decimal totalPrice = 0.0m;
                foreach (var ticket in tickets)
                {
                    totalPrice += ticket.Price;
                }
                return totalPrice;
            }
        }

        public struct Movie
        {
            public string Title { get; set; }
            public string Genre { get; set; }
            public TimeSpan Duration { get; set; }
            public Movie(string title, string genre, TimeSpan duration)
            {
                Title = title;
                Genre = genre;
                Duration = duration;
            }
        }

        public class Saloon
        {
            public struct Seat
            {
                public int Row { get; set; }
                public int Column { get; set; }
                public bool IsOccupied { get; set; }
            }


            public string Name
            {
                get { return field; }
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new ArgumentException("Saloon name cannot be null or empty.");
                    }
                    field = value;
                }
            }

            public int Rows { get; init; }
            public int Columns { get; init; }
            public int Capacity { get; init; }

            public Schedule Schedule
            {
                get
                {
                    return field;
                }
                set
                {
                    if (value == null)
                    {
                        throw new ArgumentException("Schedule cannot be null.");
                    }
                    field = value;
                }
            }

            public Saloon(string name, int rows, int columns, Schedule schedule)
            {
                Name = name;
                Rows = rows;
                Columns = columns;
                Capacity = rows * columns;
                Schedule = schedule;
                foreach (var screening in schedule.Screenings)
                {
                    screening.Saloon = this;
                }
            }
        }

        public string Name
        {
            get
            {
                return field;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Cinema name cannot be null or empty.");
                }
                field = value;
            }
        }

        public Saloon[] Saloons
        {
            get
            {
                return field;
            }
            set
            {
                if (value == null || value.Length == 0)
                {
                    throw new ArgumentException("Saloons array cannot be null or empty.");
                }
                field = value;
            }
        }

        public Cinema(string name, Saloon[] saloons)
        {
            Name = name;
            Saloons = saloons;
        }

        public class Screening
        {
            public Movie Movie { get; init; }
            public DateTime StartTime { get; init; }
            public DateTime EndTime { get; init; }
            private Saloon.Seat[,]? _seats;
            public Saloon.Seat[,] Seats 
            { 
                get => _seats ?? throw new InvalidOperationException("Seats has not been assigned to this saloon");
                set
                {
                    if (value == null)
                    {
                        throw new ArgumentException("Seats array cannot be null.");
                    }
                    _seats = value;
                }
            }
            private Saloon? _saloon;
            public Saloon Saloon
            {
                get => _saloon ?? throw new InvalidOperationException("Saloon has not been assigned to this screening.");
                set
                {
                    _saloon = value ?? throw new ArgumentNullException(nameof(value));
                    Seats = new Saloon.Seat[_saloon.Rows, _saloon.Columns];
                }
            }
            public Screening(Movie movie, DateTime showTime)
            {
                Movie = movie;
                StartTime = showTime;
                EndTime = showTime.Add(movie.Duration);
            }

            /// <summary>
            /// Checks if selected seat is free and if it is, occupies it.
            /// </summary>
            /// <param name="row"></param>
            /// <param name="column"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            public bool OccupySeat(uint row, uint column)
            {
                if (row < 0 || row >= Saloon.Rows || column < 0 || column >= Saloon.Columns)
                {
                    throw new ArgumentOutOfRangeException("Row and column must be within the bounds of the saloon.");
                }
                if (Seats[row, column].IsOccupied)
                {
                    return false;
                }
                else
                {
                    Seats[row, column].IsOccupied = true;
                    return true;
                }
            }

            /// <summary>
            /// ASCII art displaying seatmap of the saloon. X = occupied, O = free.
            /// </summary>
            public void DisplaySeatingArrangement()
            {
                Console.WriteLine($"Seating arrangement for {Saloon.Name}:");
                for (int i = 0; i < Saloon.Rows + 1; i++)
                {
                    for (int j = 0; j < Saloon.Columns + 1; j++)
                    {
                        if (i == 0)
                        {
                            Console.Write($"{j} ");
                        }
                        else if (j == 0)
                        {
                            Console.Write($"{i} ");
                        }
                        else
                        {
                            Console.Write(Seats[i - 1, j - 1].IsOccupied ? "X " : "O ");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }

        public class Schedule
        {
            public Screening[] Screenings { get; }
            public List<Movie> Movies { get; }
            public Schedule(Screening[] screenings)
            {
                Screenings = screenings;
                Movies = new List<Movie>();
                foreach (var screening in screenings)
                {
                    if (!Movies.Contains(screening.Movie))
                    {
                        Movies.Add(screening.Movie);
                    }
                }
            }

            /// <summary>
            /// Checks if the movie can be scheduled at the specified start time without overlapping with existing screenings. If it can be scheduled, it adds the new screening to the schedule.
            /// </summary>
            /// <param name="movie"></param>
            /// <param name="startTime"></param>
            /// <returns></returns>
            public bool ScheduleMovie(Movie movie, DateTime startTime)
            {
                foreach (var screening in Screenings)
                {
                    if (startTime > screening.StartTime && startTime < screening.EndTime)
                    {
                        Console.WriteLine($"The booking of movie: {movie.Title} at {startTime} starts during the movie {screening.Movie.Title} showing between {screening.StartTime} and {screening.EndTime}");
                        return false;
                    }
                    else if (startTime.Add(movie.Duration) > screening.StartTime && startTime.Add(movie.Duration) < screening.EndTime)
                    {
                        Console.WriteLine($"The booking of movie: {movie.Title} at {startTime} overlaps with the movie {screening.Movie.Title} showing between {screening.StartTime} and {screening.EndTime}");
                        return false;
                    }
                }
                Console.WriteLine($"{movie.Title} is now scheduled at {startTime}");
                return true;
            }
        }


        /// <summary>
        /// Return all movies that are being shown in any of the saloons (no duplicates, matched by title)
        /// </summary>
        /// <returns></returns>
        public Movie[] GetMovies()
        {
            return Saloons.SelectMany(saloon => saloon.Schedule.Movies).Distinct().ToArray();
        }

        /// <summary>
        /// Return all saloons that are showing the specified movie (matched by title)
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public Saloon[] GetSaloonsShowingMovie(Movie movie)
        {
            if (Saloons == null || Saloons.Length == 0)
            {
                return Array.Empty<Saloon>();
            }

            return Saloons
                .Where(s => s.Schedule.Movies != null && s.Schedule.Movies.Any(m => string.Equals(m.Title, movie.Title, StringComparison.OrdinalIgnoreCase)))
                .ToArray();
        }

        /// <summary>
        /// Return all screenings for the specified movie in the specified saloon
        /// </summary>
        /// <param name="movie"></param>
        /// <param name="saloon"></param>
        /// <returns></returns>
        public Screening[] GetScreeningsForMovieInSaloon(Movie movie, Saloon saloon)
        {
            if (saloon == null || saloon.Schedule == null || saloon.Schedule.Screenings == null)
            {
                return Array.Empty<Screening>();
            }

            return saloon.Schedule.Screenings
                .Where(sc => string.Equals(sc.Movie.Title, movie.Title, StringComparison.OrdinalIgnoreCase))
                .ToArray();
        }


        public static string GetCategoryAsString(PriceCategory category)
        {
            if (category == PriceCategory.Child)
            {
                return "Child";
            }
            else if (category == PriceCategory.Youth)
            {
                return "Youth";
            }
            else if (category == PriceCategory.Senior)
            {
                return "Senior";
            }
            else if(category == PriceCategory.Adult)
            {
                return "Adult";
            }
            else if(category == PriceCategory.Centurion)
            {
                return "Centurion";
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(category), "Invalid price category");
            }
        }

        /// <summary>
        /// Returns the price depending on price category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static decimal GetPrice(PriceCategory category)
        {
            switch (category)
            {
                case PriceCategory.Child:
                    return 00.00m;
                case PriceCategory.Youth:
                    return 80.00m;
                case PriceCategory.Adult:
                    return 120.00m;
                case PriceCategory.Senior:
                    return 90.00m;
                case PriceCategory.Centurion:
                    return 00.00m;
                default:
                    throw new ArgumentOutOfRangeException(nameof(category), "Invalid price category");
            }
        }
    }

}