using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using GelreAirport.Client.Infrastructure;

namespace GelreAirport.Client.Controllers
{
    [RoutePrefix("api/checkin")]
    public class CheckInController : ApiController
    {
        private readonly Repository _repo = new Repository();

        [Route("counters")]
        public IHttpActionResult GetCounters()
        {
            var counters = _repo.GetCounters();
            if (counters.Count > 0)
            {
                return Ok(counters);
            }
            return NotFound();
        }
    }
}
