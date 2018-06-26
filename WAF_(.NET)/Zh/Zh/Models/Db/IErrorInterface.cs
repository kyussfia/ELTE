using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zh.Models;

namespace Zh.Models.Db
{
    public interface IErrorInterface
    {
        IQueryable<Error> GetLast30Errors();
        Error GetError(int errId);
        Boolean IsErrorExist(int errId);

        Boolean createError(ViewModel.NewErrorViewModel model);
    }
}
