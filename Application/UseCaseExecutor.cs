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

        }
    }
}
