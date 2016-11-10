using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.Repositories
{
    public class ProductRepository : Repository<product>
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }
        public override product Add(product entity)
        {
            Attach(_context, entity);
            return  base.Add(entity);
        }

        public override bool Update(product entity)
        {
            Attach(_context, entity);
            return base.Update(entity);
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
                ctx.Entry(entity.users).State = EntityState.Modified;
            }
            if(entity.dic_condition != null)
            {
                ctx.Entry(entity.dic_condition).State = EntityState.Modified;
            }
            if(entity.dic_Genre != null)
            {
                ctx.Entry(entity.dic_Genre).State = EntityState.Modified;
            }
            if(entity.dic_Product_type != null)
            {
                ctx.Entry(entity.dic_Product_type).State = EntityState.Modified;
            }
        }
    }
}
