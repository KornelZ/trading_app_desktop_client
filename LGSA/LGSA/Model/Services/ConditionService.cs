using LGSA.Model.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LGSA.Model.Services
{
    public class ConditionService : IService<dic_condition>
    {
        private IUnitOfWorkFactory _factory;
        public ConditionService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public Task<bool> Add(dic_condition entity)
        {
            return null;
        }

        public Task<bool> Delete(dic_condition entity)
        {
            return null;
        }

        public async Task<IEnumerable<dic_condition>> GetData(Expression<Func<dic_condition, bool>> filter)
        {
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    var entities = await unitOfWork.ConditionRepository.GetData(filter);

                    return entities;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.ToString());
                }
            }

            return null;
        }

        public Task<bool> Update(dic_condition entity)
        {
            return null;
        }

        Task<dic_condition> IService<dic_condition>.GetById(int id)
        {
            return null;
        }
    }
}
