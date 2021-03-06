﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            string airline = "", DateTime? departureDate = null, string fields = "")
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
                if (departureDate.HasValue)
                {
                    searchParams.DepartureDate = departureDate.Value;
                }

                var passengers = _passengerRepo.FindAll(searchParams);
                if (!passengers.Any()) return NotFound();

                List<string> lstOfFields = new List<string>();

                if (!string.IsNullOrWhiteSpace(fields))
                {
                    lstOfFields = fields.ToLower().Split(',').ToList();
                }

                var passengerResult = passengers.Select(p => _factory.CreateDataShapedObject(p, lstOfFields));

                return Ok(passengerResult);
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

        [Route("passenger/{passengerNumber:int}/{flightNumber:int}")]
        [HttpGet]
        public IHttpActionResult Passenger(int passengerNumber, int flightNumber, string fields = "")
        {
            try
            {
                var passenger = _passengerRepo.FindOne(passengerNumber, flightNumber);
                if (passenger != null)
                {
                    List<string> lstOfFields = new List<string>();

                    if (!string.IsNullOrWhiteSpace(fields))
                    {
                        lstOfFields = fields.ToLower().Split(',').ToList();
                    }

                    var passengerResult = _factory.CreateDataShapedObject(passenger, lstOfFields);
                    return Ok(passengerResult);
                }
                else
                {
                    return NotFound();
                }
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

        [Route("passenger/{passengerNumber:int}/{flightNumber:int}/checkin")]
        [HttpPost]
        public IHttpActionResult CheckInPassenger(Passenger passenger)
        {
            try
            {
                var dateTimeUpdated = _passengerRepo.CheckIn(passenger);
                return Ok(dateTimeUpdated);
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    return BadRequest(ex.Message);
                }

                return BadRequest();
            }
        }
    }
}
