using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using GelreAirport.Data.Models;

namespace GelreAirport.Data.Factories
{
    public class PassengerFactory
    {
        public object CreateDataShapedObject(Passenger passenger, List<string> fields)
        {
            if (!fields.Any())
            {
                return passenger;
            }

            ExpandoObject result = new ExpandoObject();
            foreach (var field in fields)
            {
                var fieldValue = passenger.GetType()
                    .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                    .GetValue(passenger, null);

                ((IDictionary<String, Object>)result).Add(field, fieldValue);
            }

            return result;
        }
    }
}