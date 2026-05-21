using EntertainmentVenue;
using System.Security.Cryptography;

namespace Flow_Control
{
    internal class Program
    {
        private static bool _running = true;
        private Cinema SFBio = new Cinema();
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
            Console.WriteLine("\nWelcome to the main menu! Please choose an option by typing in desired number");
            Console.WriteLine("0: Quit");
            Console.WriteLine("1: Check price category");
            Console.WriteLine("2: Calculate total price for a group");
            string choice = Console.ReadLine() ?? "";
            switch (choice)
            {
                case "0":
                    Quit();
                    break;
                case "1":
                    AgeCheck();
                    break;
                case "2":
                    CalculateGroupPrice();
                    break;
                default:
                    Console.WriteLine("Invalid option, press any key to return to the main menu");
                    Console.ReadLine();
                    break;
            }
        }

        private static void Quit()
        {
            _running = false;
        }

        private static Cinema.PriceCategory AgeCheck(bool userFeedback = true)
        {
            Console.WriteLine("Age:");
            string input = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid input, please enter a valid age");
                AgeCheck();
            }
            else
            {
                uint age = uint.Parse(input);
                if (age < 20)
                {
                    if(userFeedback)
                    {
                        Console.WriteLine($"Age {age} is eligible for the youth price: {Cinema.GetPrice(Cinema.PriceCategory.Youth)}");
                    }
                    return Cinema.PriceCategory.Youth;
                }
                else if (age > 64)
                {
                    if (userFeedback)
                    {
                        Console.WriteLine($"Age {age} is eligible for the senior price: {Cinema.GetPrice(Cinema.PriceCategory.Senior)}");
                    }
                    return Cinema.PriceCategory.Senior;
                }
                else
                {
                    if (userFeedback)
                    {
                        Console.WriteLine($"Age {age} is eligible for the adult price: {Cinema.GetPrice(Cinema.PriceCategory.Adult)}");
                    }
                    return Cinema.PriceCategory.Adult;
                }
            }
            return Cinema.PriceCategory.Adult;
        }

        private static void CalculateGroupPrice()
        {
            Console.WriteLine("\nHow many people are you?");
            int nrOfTickets = int.Parse(Console.ReadLine() ?? "");
            Cinema.Ticket[] tickets = new Cinema.Ticket[nrOfTickets];

            for (int i = 0; i < nrOfTickets; i++)
            {
                Console.WriteLine($"Please enter the age of person {i+1}");
                Cinema.PriceCategory priceCategory = AgeCheck(false);
                tickets[i] = new Cinema.Ticket(priceCategory);
                decimal ticketPrice = tickets[i].Price;
                Console.WriteLine($"Ticket of category {Cinema.GetCategoryAsString(priceCategory)} has been added");
            }
            Console.WriteLine($"The total price for {tickets.Length} tickets is: {Cinema.Ticket.TotalTicketPrice(tickets)}");
        }
    }
}
