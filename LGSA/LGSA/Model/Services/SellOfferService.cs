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
    public class SellOfferService : IService<sell_Offer>
    {
        private IUnitOfWorkFactory _factory;
        public SellOfferService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public async Task<bool> Add(sell_Offer entity)
        {
            bool success = true;
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    unitOfWork.StartTransaction();
                    unitOfWork.SellOfferRepository.Add(entity);
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

        public async Task<bool> Delete(sell_Offer entity)
        {
            bool success = true;
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    unitOfWork.StartTransaction();
                    unitOfWork.SellOfferRepository.Delete(entity);
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

        public async Task<sell_Offer> GetById(int id)
        {
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    var entity = await unitOfWork.SellOfferRepository.GetById(id);
                    return entity;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.ToString());
                }
            }
            return null;
        }

        public async Task<IEnumerable<sell_Offer>> GetData(Expression<Func<sell_Offer, bool>> filter)
        {
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    var entities = await unitOfWork.SellOfferRepository.GetData(filter);

                    return entities;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.ToString());
                }
            }
            return null;
        }

        public async Task<bool> Update(sell_Offer entity)
        {
            bool success = true;
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    unitOfWork.StartTransaction();
                    unitOfWork.SellOfferRepository.Update(entity);
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

