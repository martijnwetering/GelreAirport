using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Web.Http;
using GelreAirport.Data.Infrastructure;
using GelreAirport.Data.Models;
using Newtonsoft.Json;

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

        [Route("baggage/{passengerNumber:int}/{flightNumber:int}/checkin")]
        [HttpPost]
        public IHttpActionResult CheckInBaggage([FromBody]BaggageItem item, [FromUri]int passengerNumber, [FromUri]int flightNumber )
        {
            try
            {
                _baggageRepo.CheckInBaggageItem(item, passengerNumber, flightNumber);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    return BadRequest(ex.Message);
                }   

                return InternalServerError();
            }
        }

        [Route("baggage/{passengerNumber:int}/{flightNumber:int}/{id:int}")]
        [HttpDelete]
        public IHttpActionResult RemoveBaggage(int passengerNumber, int flightNumber, int id)
        {
            try
            {
                _baggageRepo.RemoveBaggageItem(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }
    }
}
