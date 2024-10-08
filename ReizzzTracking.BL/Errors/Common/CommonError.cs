using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.Errors.Common
{
    public class CommonError
    {
        public const string IdInputMismatch = "The Id of the item you want to update and the view model's id are not the same. Id input mismatch";
        public const string NotFoundWithId = "There's no {0} with id = {1}";
    }
}
