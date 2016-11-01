using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.UnitOfWork
{
    public abstract class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public abstract IUnitOfWork CreateUnitOfWork();
    }
}
