using System;

namespace EntertainmentVenue
{
    public class Cinema
    {
        public enum PriceCategory
        {
            Youth = 0,
            Adult = 1,
            Senior = 2
        }

        public Cinema()
        {
        }
        public struct Ticket
        {
            public Cinema.PriceCategory Category { get; set; }
            public decimal Price { get; set; }

            public Ticket(Cinema.PriceCategory category)
            {
                Category = category;
                Price = Cinema.GetPrice(category);
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

        public static string GetCategoryAsString(PriceCategory category)
        {
            if (category == PriceCategory.Youth)
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
            else
            {
                throw new ArgumentOutOfRangeException(nameof(category), "Invalid price category");
            }
        }

        public static decimal GetPrice(PriceCategory category)
        {
            switch (category)
            {
                case PriceCategory.Youth:
                    return 80.00m;
                case PriceCategory.Adult:
                    return 120.00m;
                case PriceCategory.Senior:
                    return 90.00m;
                default:
                    throw new ArgumentOutOfRangeException(nameof(category), "Invalid price category");
            }
        }
    }

}