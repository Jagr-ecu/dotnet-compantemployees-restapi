using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    //clase heredada de notfound, en este si no ecuentra compañia envia el mensjae que pusimos
    public sealed class CompanyNotFoundException : NotFoundException
    {
        public CompanyNotFoundException(Guid companyId)
            : base($"La compañia con id: {companyId} no existe en la base de datos")
        {
        }
    }
}
