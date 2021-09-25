using Application;
using Application.Commands.DiscountCommands;
using Application.Data_Transfer;
using Application.Queries;
using Application.Searches;
using Microsoft.AspNetCore.Http;
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
    public class DiscountsController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public DiscountsController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET api/<DiscountsController>
        [HttpGet]
        public IActionResult Get([FromBody] DiscountSearch search, [FromServices] IGetDiscountsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<DiscountsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetDiscountQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST api/<DiscountsController>
        [HttpPost]
        public IActionResult Post([FromBody] DiscountDto dto, [FromServices] ICreateDiscountCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<DiscountsController>/5
        [HttpPut]
        public IActionResult Put([FromBody] DiscountDto dto, [FromServices] IUpdateDiscountCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<DiscountsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteDiscountCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
