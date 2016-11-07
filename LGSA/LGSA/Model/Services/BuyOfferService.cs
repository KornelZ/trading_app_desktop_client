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
    public class BuyOfferService : IService<buy_Offer>
    {
        private IUnitOfWorkFactory _factory;
        public BuyOfferService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public async Task<bool> Add(buy_Offer entity)
        {
            bool success = true;
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    unitOfWork.StartTransaction();
                    unitOfWork.BuyOfferRepository.Add(entity);
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

        public async Task<bool> Delete(buy_Offer entity)
        {
            bool success = true;
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    unitOfWork.StartTransaction();
                    unitOfWork.BuyOfferRepository.Delete(entity);
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

        public async Task<buy_Offer> GetById(int id)
        {
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    var entity = await unitOfWork.BuyOfferRepository.GetById(id);
                    return entity;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.ToString());
                }
            }
            return null;
        }

        public async Task<IEnumerable<buy_Offer>> GetData(Expression<Func<buy_Offer, bool>> filter)
        {
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    var entities = await unitOfWork.BuyOfferRepository.GetData(filter);

                    return entities;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.ToString());
                }
            }
            return null;
        }

        public async Task<bool> Update(buy_Offer entity)
        {
            bool success = true;
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    unitOfWork.StartTransaction();
                    unitOfWork.BuyOfferRepository.Update(entity);
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
