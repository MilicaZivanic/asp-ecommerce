using Application.Data_Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CategoryCommands
{
    public interface ICreateCategoryCommand : ICommand<CategoryDto>
    {
    }
}
