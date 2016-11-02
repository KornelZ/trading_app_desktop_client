﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.Services
{
    public interface IService<T>
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetData();
        // dodam jeszcze GetData z filterm
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
    }
}
