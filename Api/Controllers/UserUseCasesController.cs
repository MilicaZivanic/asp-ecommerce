using Application;
using Application.Commands;
using Application.Data_Transfer;
using Application.Queries;
using Application.Searches;
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
        public IActionResult Get([FromQuery] UserUseCaseSearch search, [FromServices] IGetUserUseCases query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // PUT api/<UserUseCasesController>/5
        [HttpPut]
        public IActionResult Put([FromBody] UserUseCaseDto dto, [FromServices] ISetUserUseCase command)
        {
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

    }
}
