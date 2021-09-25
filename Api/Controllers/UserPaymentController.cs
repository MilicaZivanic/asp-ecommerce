using Application;
using Application.Commands.UserPaymentCommands;
using Application.Data_Transfer;
using Microsoft.AspNetCore.Authorization;
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
    public class UserPaymentController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public UserPaymentController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        [Authorize]
        // POST api/<UserPaymentController>
        [HttpPost]
        public IActionResult Post([FromBody] UserPaymentDto dto, [FromServices] ICreateUserPaymentCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [Authorize]
        // PUT api/<UserPaymentController>/5
        [HttpPut]
        public IActionResult Put([FromBody] UserPaymentDto dto, [FromServices] IUpdateUserPaymentCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        [Authorize]
        // DELETE api/<UserPaymentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteUserPaymentCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
