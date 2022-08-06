using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class CollectionByIdsBadRequestException : Exception
    {
        public CollectionByIdsBadRequestException()
            : base("Error en el recuento de numero de compañias en comparacion con los ids")
        {
        }
    }
}
