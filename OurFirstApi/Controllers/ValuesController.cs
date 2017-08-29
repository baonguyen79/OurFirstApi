using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OurFirstApi.Models;

namespace OurFirstApi.Controllers
{
    [RoutePrefix("api/employee")]

    public class ValuesController : ApiController
    {
        // GET api/values
        [HttpGet]
        [Route("otherapi/zalues")]    
        //[HttpGet, Route("api/Employee/name/{FirstName}")]

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet, Route("{randomNumber}")]
        public string Get(int randomNumber)
        {
            return "value";
        }

        // POST api/values
        [HttpPost, Route("")]
        public HttpResponseMessage Post(EmployeeListResult employee)
        {
            Console.WriteLine($"{employee.FirstName} {employee.LastName}");

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // PUT api/values/5
        [HttpPut, Route("{id}")]
        public HttpResponseMessage Put(int id, EmployeeListResult employee)
        {
            Console.WriteLine($"{employee.FirstName} {employee.LastName}");

            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

        // DELETE api/values/5
        [HttpDelete, Route("{id}")]
        public void Delete(int id)
        {
        }
    }
}
