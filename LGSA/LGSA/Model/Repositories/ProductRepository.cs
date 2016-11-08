using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.Repositories
{
    class ProductRepository : Repository<product>
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<product>> GetData(Expression<Func<product, bool>> filter)
        {
            return await _context.Set<product>()
                .Include(product => product.users)
                .Include(product => product.dic_condition)
                .Include(product => product.dic_Genre)
                .Include(product => product.dic_Product_type)
                .Where(filter).ToListAsync();
        }
    }
}
