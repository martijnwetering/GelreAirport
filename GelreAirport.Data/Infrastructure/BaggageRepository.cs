using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GelreAirport.Data.Models;

namespace GelreAirport.Data.Infrastructure
{
    public class BaggageRepository : Repository<BaggageItem>
    {
        public BaggageRepository(AdoNetContext context) : base(context)
        {
            
        }

        public IEnumerable<BaggageItem> FindAll(int passengerNumber, int flightNumber)
        {
            var baggage = new List<BaggageItem>();

            using (var command = Context.CreateCommand())
            {
                command.CommandText =
                    @"SELECT * FROM [Object] WHERE passagiernummer = @passengerNumber AND vluchtnummer = @flightNumber";
                command.Parameters.Add("@passengerNumber", SqlDbType.Int).Value = passengerNumber;
                command.Parameters.Add("@flightNumber", SqlDbType.Int).Value = flightNumber;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new BaggageItem()
                        {
                            PassengerNumber = (int)reader["passagiernummer"],
                            FlightNumber = (int)reader["vluchtnummer"],
                            Weight = (int)reader["gewicht"],
                            TimeStamp = (DateTime)reader["ts"]
                        };
                        baggage.Add(item);
                    }
                }
            }

            return baggage;
        } 
    }
}
