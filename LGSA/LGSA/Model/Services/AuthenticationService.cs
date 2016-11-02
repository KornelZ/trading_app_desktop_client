﻿using LGSA.Model.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LGSA.Model.Services
{
    public class AuthenticationService : IService<users_Authetication>
    {
        private IUnitOfWorkFactory _factory;
        public AuthenticationService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public async Task<bool> Add(users_Authetication entity)
        {
            bool success = false;
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    success = unitOfWork.AuthenticationRepository.Add(entity);
                    await unitOfWork.Save();
                    unitOfWork.Commit();
                }
                catch(Exception e)
                {
                    unitOfWork.Rollback();
                    success = false;
                   // MessageBox.Show(e.InnerException.ToString());
                }
            }
            return success;
        }

        public async Task<bool> Delete(users_Authetication entity)
        {
            bool success = true;
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    unitOfWork.AuthenticationRepository.Delete(entity);
                    await unitOfWork.Save();
                    unitOfWork.Commit();
                }
                catch (Exception e)
                {
                    unitOfWork.Rollback();
                    success = false;
                   // MessageBox.Show(e.InnerException.ToString());
                }
            }
            return success;
        }

        public async Task<users_Authetication> GetById(int id)
        {
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    var entity = await unitOfWork.AuthenticationRepository.GetById(id);

                    unitOfWork.Commit();
                    return entity;
                }
                catch (Exception e)
                {
                    unitOfWork.Rollback();
                   // MessageBox.Show(e.InnerException.ToString());
                }
            }
            return null;
        }

        public async Task<IEnumerable<users_Authetication>> GetData()
        {
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    var entities = await unitOfWork.AuthenticationRepository.GetData();

                    unitOfWork.Commit();
                    return entities;
                }
                catch (Exception e)
                {
                    unitOfWork.Rollback();
                   // MessageBox.Show(e.InnerException.ToString());
                }
            }
            return null;
        }

        public async Task<bool> Update(users_Authetication entity)
        {
            bool success = true;
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    unitOfWork.AuthenticationRepository.Update(entity);
                    await unitOfWork.Save();
                    unitOfWork.Commit();
                }
                catch (Exception e)
                {
                    unitOfWork.Rollback();
                    success = false;
                    //MessageBox.Show(e.InnerException.ToString());
                }
            }
            return success;
        }
    }
}
