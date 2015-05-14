using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GelreAirport.Data.Infrastructure;
using GelreAirport.Data.Models;

namespace GelreAirport.Client.Controllers
{
    [RoutePrefix("api")]
    public class BaggageController : ApiController
    {
        private readonly BaggageRepository _baggageRepo;

        public BaggageController()
        {
            var connectionFactory = new ConnectionFactory("DefaultConnection");
            var context = new AdoNetContext(connectionFactory);
            _baggageRepo = new BaggageRepository(context);
        }

        [Route("baggage/{passengerNumber:int}/{flightNumber:int}")]
        [HttpGet]
        public IHttpActionResult FindAll(int passengerNumber, int flightNumber)
        {
            try
            {
                var baggage = _baggageRepo.FindAll(passengerNumber, flightNumber);
                if (baggage != null)
                {
                    return Ok(baggage);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                
                return InternalServerError();
            }
        }
    }
}
