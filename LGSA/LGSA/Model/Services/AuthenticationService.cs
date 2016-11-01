using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows;

namespace LGSA.Model.Services
{
    //testowy serwis
    public class AuthenticationService : IService<users_Authetication>
    {
        private UnitOfWork.IUnitOfWorkFactory _factory;

        public AuthenticationService(UnitOfWork.IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }
        public async Task<bool> Add(users_Authetication entity)
        {
            bool entityExists = false;
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    entityExists = unitOfWork.Context.users_Authetication.Include("users1")
                        .Any(e => e.users1.Last_Name == entity.users1.Last_Name);

                    if(entityExists != true)
                    {
                        unitOfWork.Context.users_Authetication.Add(entity);
                        await unitOfWork.Save();
                        unitOfWork.Commit();
                    }
                }
                catch(Exception e)
                {
                    unitOfWork.Rollback();
                    MessageBox.Show(e.ToString());
                    return false;
                }
            }
            return !entityExists;
        }

        public bool Delete(users_Authetication entity)
        {
            return false;
        }

        public async Task<IEnumerable<users_Authetication>> GetData()
        {
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                var x = await unitOfWork.Context.users_Authetication.Include("users1").Take(100).ToListAsync();
                return x;
            }
        }

        public bool Update(users_Authetication entity)
        {
            return false;
        }
    }
}
