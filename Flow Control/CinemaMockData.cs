
namespace EntertainmentVenue.Cinema
{
    internal static class CinemaMockData
    {
        public static Cinema.Movie[] GetAllAvailableMovies()
        {
            List<Cinema.Movie> returnList = new List<Cinema.Movie>(0);
            returnList.AddRange(movieList.ToList());
            returnList.AddRange(movieList2.ToList());
            return returnList.ToArray();
        }

        public static Cinema.Screening[] GetAllScreenings()
        {
            List<Cinema.Screening> returnList = new List<Cinema.Screening>(0);
            returnList.AddRange(saloon1Screenings.ToList());
            returnList.AddRange(saloon2Screenings.ToList());
            return returnList.ToArray();
        }

        private static Cinema.Movie[] movieList =
        {
            new Cinema.Movie("Avengers", "Action", new TimeSpan(2, 23, 0)),
            new Cinema.Movie("Oppenheimer", "Thriller", new TimeSpan(3, 0, 0)),
            new Cinema.Movie("Brother Bear", "Adventure", new TimeSpan(1, 25, 0)),
            new Cinema.Movie("The Notebook", "Romance", new TimeSpan(2, 1, 0)),
            new Cinema.Movie("The Shawshank Redemption", "Drama", new TimeSpan(2, 22, 0))
        };

        private static Cinema.Movie[] movieList2 =
        {
            new Cinema.Movie("The Lion King", "Animation", new TimeSpan(1, 28, 0)),
            new Cinema.Movie("Inception", "Sci-Fi", new TimeSpan(2, 28, 0)),
            new Cinema.Movie("The Godfather", "Crime", new TimeSpan(2, 55, 0)),
            new Cinema.Movie("Pulp Fiction", "Crime", new TimeSpan(2, 34, 0)),
            new Cinema.Movie("The Dark Knight", "Action", new TimeSpan(2, 32, 0))
        };

        private static Cinema.Screening[] saloon1Screenings =
        {
            new Cinema.Screening(movieList[0], new DateTime(2026, 7, 1, 18, 0, 0)),
            new Cinema.Screening(movieList[1], new DateTime(2026, 7, 1, 21, 30, 0)),
            new Cinema.Screening(movieList[2], new DateTime(2026, 7, 2, 17, 0, 0)),
            new Cinema.Screening(movieList[3], new DateTime(2026, 7, 2, 20, 30, 0)),
            new Cinema.Screening(movieList[4], new DateTime(2026, 7, 3, 19, 0, 0)),
            new Cinema.Screening(movieList2[0], new DateTime(2026, 7, 4, 17, 30, 0)),
            new Cinema.Screening(movieList2[1], new DateTime(2026, 7, 4, 21, 0, 0)),
            new Cinema.Screening(movieList2[4], new DateTime(2026, 7, 5, 18, 30, 0)),
            new Cinema.Screening(movieList[1], new DateTime(2026, 7, 5, 22, 0, 0)),
            new Cinema.Screening(movieList2[1], new DateTime(2026, 7, 6, 17, 0, 0)),
            new Cinema.Screening(movieList2[0], new DateTime(2026, 7, 6, 20, 30, 0)),
            new Cinema.Screening(movieList[0], new DateTime(2026, 7, 7, 18, 0, 0)),
            new Cinema.Screening(movieList[2], new DateTime(2026, 7, 7, 21, 30, 0)),
            new Cinema.Screening(movieList[2], new DateTime(2026, 7, 8, 17, 0, 0)),
        };

        private static Cinema.Screening[] saloon2Screenings =
        {
            new Cinema.Screening(movieList2[0], new DateTime(2026, 7, 1, 17, 0, 0)),
            new Cinema.Screening(movieList2[1], new DateTime(2026, 7, 1, 20, 30, 0)),
            new Cinema.Screening(movieList2[2], new DateTime(2026, 7, 2, 18, 0, 0)),
            new Cinema.Screening(movieList2[3], new DateTime(2026, 7, 2, 21, 30, 0)),
            new Cinema.Screening(movieList2[4], new DateTime(2026, 7, 3, 20, 0, 0)),
            new Cinema.Screening(movieList[0], new DateTime(2026, 7, 4, 18, 30, 0)),
            new Cinema.Screening(movieList[3], new DateTime(2026, 7, 4, 22, 0, 0)),
            new Cinema.Screening(movieList2[4], new DateTime(2026, 7, 5, 19, 0, 0)),
            new Cinema.Screening(movieList[1], new DateTime(2026, 7, 5, 22, 30, 0)),
            new Cinema.Screening(movieList2[1], new DateTime(2026, 7, 6, 18, 0, 0)),
            new Cinema.Screening(movieList2[4], new DateTime(2026, 7, 6, 21, 30, 0))
        };

        private static Cinema.Schedule saloon1Schedule = new Cinema.Schedule(saloon1Screenings);
        private static Cinema.Schedule saloon2Schedule = new Cinema.Schedule(saloon2Screenings);
        public static Cinema.Saloon Saloon1 = new Cinema.Saloon("Saloon 1", 8, 10, saloon1Schedule);
        public static Cinema.Saloon Saloon2 = new Cinema.Saloon("Saloon 2", 10, 12, saloon2Schedule);
    }
}
