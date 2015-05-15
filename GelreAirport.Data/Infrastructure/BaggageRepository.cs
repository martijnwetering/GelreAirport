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
                            TimeStamp = (DateTime)reader["ts"],
                            Id = (int)reader["volgnummer"]
                        };
                        baggage.Add(item);
                    }
                }
            }

            return baggage;
        }

        public void CheckInBaggageItem(BaggageItem item, int passengerNumber, int flightNumber)
        {
            if (item.Weight == 0) throw new ArgumentException();

            using (var command = Context.CreateCommand())
            {
                command.CommandText = "procAddObject";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@passagiernummer", SqlDbType.Int).Value = passengerNumber;
                command.Parameters.Add("@vluchtnummer", SqlDbType.Int).Value = flightNumber;
                command.Parameters.Add("@gewicht", SqlDbType.Int).Value = item.Weight;

                command.ExecuteNonQuery();
            }
        }

        public void RemoveBaggageItem(int id)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandText = "DELETE [Object] WHERE volgnummer = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                command.ExecuteNonQuery();
            }
        }
    }
}
