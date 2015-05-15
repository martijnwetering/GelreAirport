using System;

namespace GelreAirport.Data.Models
{
    public class BaggageItem
    {
        public int Id { get; set; }
        public int PassengerNumber { get; set; }
        public int FlightNumber { get; set; }
        public int Weight { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}