using Application.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class UseCaseExecutor
    {
        private readonly IApplicationActor actor;

        public UseCaseExecutor(IApplicationActor actor)
        {
            this.actor = actor;
        }

        public void ExecuteCommand<TRequest>(ICommand<TRequest> command, TRequest request)
        {
            Console.WriteLine($"{DateTime.Now}: {actor.Identity} is trying to execute {command.Name} using data: " + $"{JsonConvert.SerializeObject(request)}");

            if (!actor.AllowedUseCases.Contains(command.Id))
            {
                throw new UnauthorizedUseCaseException(command, actor);
            }

            command.Execute(request);
        }
    }
}
