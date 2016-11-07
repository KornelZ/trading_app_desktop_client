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
    public class GenreService : IService<dic_Genre>
    {
        private IUnitOfWorkFactory _factory;
        public GenreService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public Task<bool> Add(dic_Genre entity)
        {
            return null;
        }

        public Task<bool> Delete(dic_Genre entity)
        {
            return null;
        }

        public Task<dic_Genre> GetById(int id)
        {
            return null;
        }

        public async Task<IEnumerable<dic_Genre>> GetData(Expression<Func<dic_Genre, bool>> filter)
        {
            using (var unitOfWork = _factory.CreateUnitOfWork())
            {
                try
                {
                    var entities = await unitOfWork.GenreRepository.GetData(filter);

                    return entities;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.ToString());
                }
            }

            return null;
        }

        public Task<bool> Update(dic_Genre entity)
        {
            throw new NotImplementedException();
        }
    }
}
