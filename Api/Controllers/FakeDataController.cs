
using Bogus;
using Domain;
using EfDataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakeDataController : ControllerBase
    {

        private readonly EcomShopContext _context;

        public FakeDataController(EcomShopContext context)
        {
            _context = context;
        }

        // GET: api/<FakeDataController>
        [HttpGet]
        public void Get()
        {

        }

        // GET api/<FakeDataController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FakeDataController>
        [HttpPost]
        public void Post()
        {

        }

        // PUT api/<FakeDataController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FakeDataController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
