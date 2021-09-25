using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core
{
    public class AnonymousActor : IApplicationActor
    {
        public int Id => 2;

        public string Identity => "Anonymus";

        public IEnumerable<int> AllowedUseCases => new List<int> { 4, 8, 2, 9, 12, 13, 14, 16, 17, 18, 19, 20 };
    }
}
