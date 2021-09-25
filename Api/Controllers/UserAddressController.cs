using Application;
using Application.Commands.UserAddressCommands;
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
    public class UserAddressController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;
        public UserAddressController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        [Authorize]
        // POST api/<UserAddressController>
        [HttpPost]
        public IActionResult Post([FromBody] UserAddressDto dto, [FromServices] ICreateUserAddressCommand command )
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [Authorize]
        // PUT api/<UserAddressController>/5
        [HttpPut]
        public IActionResult Put([FromBody] UserAddressDto dto, [FromServices] IUpdateUserAddressCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        [Authorize]
        // DELETE api/<UserAddressController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteUserAddressCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
