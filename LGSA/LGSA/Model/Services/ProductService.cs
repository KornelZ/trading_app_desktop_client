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
    public class ProductService : IService<product>
    {
        private IUnitOfWorkFactory _factory;
        public ProductService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public async Task<bool> Add(product entity)
        {
            bool success = true;
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    unitOfWork.StartTransaction();
                    unitOfWork.ProductRepository.Add(entity);
                    await unitOfWork.Save();
                    unitOfWork.Commit();
                }
                catch (Exception e)
                {
                    unitOfWork.Rollback();
                    success = false;
                    MessageBox.Show(e.InnerException.ToString());
                }
            }
            return success;
        }

        public async Task<bool> Delete(product entity)
        {

            bool success = true;
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    unitOfWork.StartTransaction();
                    unitOfWork.ProductRepository.Delete(entity);
                    await unitOfWork.Save();
                    unitOfWork.Commit();
                }
                catch (Exception e)
                {
                    unitOfWork.Rollback();
                    success = false;
                    MessageBox.Show(e.InnerException.ToString());
                }
            }
            return success;
        }

        public async Task<product> GetById(int id)
        {

            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    product entity = await unitOfWork.ProductRepository.GetById(id);
                    return entity;
                }
                catch (Exception e)
                {
                    unitOfWork.Rollback();
                    MessageBox.Show(e.InnerException.ToString());
                }
            }
            return null;
        }

        public virtual async Task<IEnumerable<product>> GetData(Expression<Func<product, bool>> filter)
        {
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    var entities = await unitOfWork.ProductRepository.GetData(filter);

                    return entities;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.ToString());
                }
            }
            return null;
        }

        public virtual async Task<bool> Update(product entity)
        {
            bool success = true;
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    unitOfWork.StartTransaction();
                    unitOfWork.ProductRepository.Update(entity);
                    await unitOfWork.Save();
                    unitOfWork.Commit();
                }
                catch (Exception e)
                {
                    unitOfWork.Rollback();
                    success = false;
                    MessageBox.Show(e.InnerException.ToString());
                }
            }
            return success;
        }
    }
    
}
