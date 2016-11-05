using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LGSA.Model.UnitOfWork;

namespace LGSA.Model.Services
{
    //public class UserService : IService<users>
    //{
    //    private IUnitOfWorkFactory _factory;
    //    public UserService(IUnitOfWorkFactory factory)
    //    {
    //        _factory = factory;
    //    }

    //    public async Task<bool> Add(users entity)
    //    {
    //        bool success = false;
    //        using (var unitOfWork = _factory.CreateUnitOfWork())
    //        {
    //            try
    //            {
    //                unitOfWork.StartTransaction();
    //                success = unitOfWork.UserRepository.Add(entity);
    //                await unitOfWork.Save();
    //                unitOfWork.Commit();
    //            }
    //            catch (Exception e)
    //            {
    //                unitOfWork.Rollback();
    //                success = false;
    //                MessageBox.Show(e.InnerException.ToString());
    //            }
    //        }
    //        return success;
    //    }

    //    public async Task<bool> Delete(users entity)
    //    {
    //        bool success = true;
    //        using (var unitOfWork = _factory.CreateUnitOfWork())
    //        {
    //            try
    //            {
    //                unitOfWork.StartTransaction();
    //                unitOfWork.UserRepository.Delete(entity);
    //                await unitOfWork.Save();
    //                unitOfWork.Commit();
    //            }
    //            catch (Exception e)
    //            {
    //                unitOfWork.Rollback();
    //                success = false;
    //                MessageBox.Show(e.InnerException.ToString());
    //            }
    //        }
    //        return success;
    //    }

    //    public async Task<users> GetById(int id)
    //    {
    //        using (var unitOfWork = _factory.CreateUnitOfWork())
    //        {
    //            try
    //            {
    //                var entity = await unitOfWork.UserRepository.GetById(id);

    //                return entity;
    //            }
    //            catch (Exception e)
    //            {
    //                MessageBox.Show(e.InnerException.ToString());
    //            }
    //        }
    //        return null;
    //    }

    //    public async Task<IEnumerable<users>> GetData()
    //    {
    //        using (var unitOfWork = _factory.CreateUnitOfWork())
    //        {
    //            try
    //            {
    //                var entities = await unitOfWork.UserRepository.GetData();

    //                return entities;
    //            }
    //            catch (Exception e)
    //            {
    //                MessageBox.Show(e.InnerException.ToString());
    //            }
    //        }
    //        return null;
    //    }

    //    public async Task<bool> Update(users entity)
    //    {
    //        bool success = true;
    //        using (var unitOfWork = _factory.CreateUnitOfWork())
    //        {
    //            try
    //            {
    //                unitOfWork.StartTransaction();
    //                unitOfWork.UserRepository.Update(entity);
    //                await unitOfWork.Save();
    //                unitOfWork.Commit();
    //            }
    //            catch (Exception e)
    //            {
    //                unitOfWork.Rollback();
    //                success = false;
    //                MessageBox.Show(e.InnerException.ToString());
    //            }
    //        }
    //        return success;
    //    }
    //}
}
