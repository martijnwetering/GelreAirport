using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using GelreAirport.Data;

namespace GelreAirport.Client.Infrastructure
{
    public class Repository
    {
        private readonly GelreAirportDataAccesLayer _context = 
            new GelreAirportDataAccesLayer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public List<int> GetCounters()
        {
            return _context.GetCounters();
        }
    }
}