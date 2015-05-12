using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using GelreAirport.Data.Infrastructure;
using GelreAirport.Data.Models;

namespace GelreAirport.Client.Controllers
{
    [RoutePrefix("api")]
    public class CountersController : ApiController
    {
        private readonly PassengerRepository _passengerRepo;

        public CountersController()
        {
            var connectionFactory = new ConnectionFactory("DefaultConnection");
            var context = new AdoNetContext(connectionFactory);
            _passengerRepo = new PassengerRepository(context);
        }

        [Route("counters")]
        public IHttpActionResult GetCounters()
        {
            var counters = _passengerRepo.GetCounters();
            if (counters.Any())
            {
                return Ok(counters);
            }
            return NotFound();
        }
    }
}
