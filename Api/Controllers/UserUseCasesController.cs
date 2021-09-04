using Application;
using Application.Commands;
using Application.Data_Transfer;
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
    public class UserUseCasesController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public UserUseCasesController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<UserUseCasesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserUseCasesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserUseCasesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserUseCasesController>/5
        [HttpPut]
        public IActionResult Put([FromBody] UserUseCaseDto dto, [FromServices] ISetUserUseCase command)
        {
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<UserUseCasesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
