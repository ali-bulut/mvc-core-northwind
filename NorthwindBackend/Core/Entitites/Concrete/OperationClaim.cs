using System;
using System.Collections.Generic;
using System.Text;
using Core.Entitites;

namespace Core.Entitites.Concrete
{
    public class OperationClaim:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
