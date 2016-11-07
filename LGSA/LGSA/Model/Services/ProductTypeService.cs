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
    public class ProductTypeService : IService<dic_Product_type>
    {
        private IUnitOfWorkFactory _factory;
        public ProductTypeService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }
        public Task<bool> Add(dic_Product_type entity)
        {
            return null;
        }

        public Task<bool> Delete(dic_Product_type entity)
        {
            return null;
        }

        public Task<dic_Product_type> GetById(int id)
        {
            return null;
        }

        public async Task<IEnumerable<dic_Product_type>> GetData(Expression<Func<dic_Product_type, bool>> filter)
        {
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    var entities = await unitOfWork.ProductTypeRepository.GetData(filter);

                    return entities;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.ToString());
                }
            }

            return null;
        }

        public Task<bool> Update(dic_Product_type entity)
        {
            throw new NotImplementedException();
        }
    }
}
