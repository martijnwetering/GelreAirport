﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using GelreAirport.Data.Factories;
using GelreAirport.Data.Infrastructure;
using GelreAirport.Data.Models;

namespace GelreAirport.Client.Controllers
{
    [RoutePrefix("api")]
    public class PassengersController : ApiController
    {
        private readonly PassengerFactory _factory;
        private readonly PassengerRepository _passengerRepo;

        public PassengersController()
        {
            _factory = new PassengerFactory();
            var connectionFactory = new ConnectionFactory("DefaultConnection");
            var context = new AdoNetContext(connectionFactory);
            _passengerRepo = new PassengerRepository(context);
        }

        [Route("passengers")]
        [HttpGet]
        public IHttpActionResult Passengers(string passengerName = "", int flightNumber = 0, string destination = "", 
            string airline = "", string departureDate = "", string fields = "")
        {
            try
            {
                var searchParams = new SearchParams()
                {
                    PassengerName = passengerName,
                    FlightNumber = flightNumber,
                    Destination = destination,
                    Airline = airline
                };
                if (!string.IsNullOrWhiteSpace(departureDate))
                {
                    searchParams.DepartureDate = DateTime.Parse(departureDate);
                }

                var passengers = _passengerRepo.FindAll(searchParams);
                if (passengers == null) return NotFound();

                List<string> lstOfFields = new List<string>();

                if (fields != null)
                {
                    lstOfFields = fields.ToLower().Split(',').ToList();
                }

                var passengerResult = passengers.Select(p => _factory.CreateDataShapedObject(p, lstOfFields));

                return Ok(passengerResult);
            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
            
        }

        [Route("passenger/{passengerNumber:int}/{flightNumber:int}")]
        public IHttpActionResult Passenger(int passengerNumber, int flightNumber)
        {
            return Ok();
        }
    }
}
