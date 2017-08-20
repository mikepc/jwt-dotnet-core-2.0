using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JwtDemo.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [Authorize()]
        // POST api/values
        [HttpPost]
        public Task<string> Post([FromBody]string value)
        {
           return Task.FromResult("Aww yeah, we're in");
        }

        [Authorize()]
        // PUT api/values/5
        [HttpPut("{id}")]
        public Task<string> Put(int id, [FromBody]string value)
        {
            return Task.FromResult("Authorized");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
