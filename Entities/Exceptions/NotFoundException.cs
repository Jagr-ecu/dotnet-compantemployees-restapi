using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    //Clase que se usa para los errores de notfound, en el cual se enviamos un mensaje 
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException(string message)
            :base(message) 
        { }
    }
}
