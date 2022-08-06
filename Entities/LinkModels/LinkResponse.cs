using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.LinkModels
{
    //con esta clase vamos a saber si nuestra respuesta tiene links. Si es así, vamos a utilizar la propiedad
    //LinkedEntities. De lo contrario, vamos a utilizar la propiedad ShapedEntities.
    public class LinkResponse
    {
        public bool HasLinks { get; set; }
        public List<Entity> ShapedEntities { get; set; }
        public LinkCollectionWrapper<Entity> LinkedEntities { get; set; }
        public LinkResponse()
        {
            LinkedEntities = new LinkCollectionWrapper<Entity>();
            ShapedEntities = new List<Entity>();
        }

    }
}
