using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.Repositories
{
    public class SellOfferRepository : Repository<sell_Offer>
    {
        public SellOfferRepository(DbContext context) : base(context)
        {
        }
        public override sell_Offer Add(sell_Offer entity)
        {
            Attach(_context, entity);

            return base.Add(entity);
        }
        public override async Task<IEnumerable<sell_Offer>> GetData(Expression<Func<sell_Offer, bool>> filter)
        {
            return await _context.Set<sell_Offer>()
                .Include(sell_Offer => sell_Offer.users)
                .Include(sell_Offer => sell_Offer.product)
                .Include(sell_Offer => sell_Offer.dic_Offer_status)
                .Include(sell_Offer => sell_Offer.product.dic_condition)
                .Include(sell_Offer => sell_Offer.product.dic_Product_type)
                .Include(sell_Offer => sell_Offer.product.dic_Genre)
                .Where(filter).Take(100).ToListAsync();
        }

        public override async Task<sell_Offer> GetById(int id)
        {
            return await _context.Set<sell_Offer>()
                .Include(sell_Offer => sell_Offer.users)
                .Include(sell_Offer => sell_Offer.product)
                .Include(sell_Offer => sell_Offer.product.dic_Genre)
                .Include(sell_Offer => sell_Offer.product.dic_condition)
                .Include(sell_Offer => sell_Offer.product.dic_Product_type)
                .Include(sell_Offer => sell_Offer.dic_Offer_status)
                .FirstOrDefaultAsync(b => b.seller_id == id);
        }

        public static void Attach(DbContext ctx, sell_Offer entity)
        {
            if (entity.users != null)
            {
                ctx.Set<users>().Attach(entity.users);
            }
            if (entity.dic_Offer_status != null)
            {
                ctx.Set<dic_Offer_status>().Attach(entity.dic_Offer_status);
            }
            if (entity.product != null)
            {
                ctx.Set<product>().Attach(entity.product);
                ProductRepository.Attach(ctx, entity.product);
            }
        }
    }
}
