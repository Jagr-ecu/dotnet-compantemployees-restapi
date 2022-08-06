using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class MaxAgeRangeBadRequestException : BadRequestException
    {
        public MaxAgeRangeBadRequestException()
            :base("MaxAge no puede ser menor que MinAge ")
        {

        }
    }
}
