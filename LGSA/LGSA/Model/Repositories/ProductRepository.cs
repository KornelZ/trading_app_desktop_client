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
        public override bool Add(product entity)
        {
            Attach(_context, entity);
            return  base.Add(entity);
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

        public static void Attach(DbContext ctx, product entity)
        {
            if(entity.users != null)
            {
                ctx.Set<users>().Attach(entity.users);
            }
            if(entity.dic_condition != null)
            {
                ctx.Set<dic_condition>().Attach(entity.dic_condition);
            }
            if(entity.dic_Genre != null)
            {
                ctx.Set<dic_Genre>().Attach(entity.dic_Genre);
            }
            if(entity.dic_Product_type != null)
            {
                ctx.Set<dic_Product_type>().Attach(entity.dic_Product_type);
            }
        }
    }
}
