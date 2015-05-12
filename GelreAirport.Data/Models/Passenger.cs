using System;

namespace GelreAirport.Data.Models
{
    public class Passenger
    {
        public int PassengerNumber { get; set; }
        public int FlightNumber { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DepartureDate { get; set; }

        public DateTime CheckInTime { get; set; }
        public string Seat { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}