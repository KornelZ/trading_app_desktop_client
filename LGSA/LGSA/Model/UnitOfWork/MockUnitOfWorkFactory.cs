using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.UnitOfWork
{
    public class MockUnitOfWorkFactory : UnitOfWorkFactory
    {
        public override IUnitOfWork CreateUnitOfWork()
        {
            return new MockUnitOfWork();
        }
    }
}
