using System;

namespace GelreAirport.Data.Models
{
    public class SearchParams
    {
        public string PassengerName { get; set; }
        public int FlightNumber { get; set; }
        public string Destination { get; set; }
        public string Airline { get; set; }
        public DateTime? DepartureDate { get; set; }
    }
}