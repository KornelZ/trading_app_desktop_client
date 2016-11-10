using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.Repositories
{
    public class BuyOfferRepository : Repository<buy_Offer>
    {
        public BuyOfferRepository(DbContext context) : base(context)
        {
        }

        public override buy_Offer Add(buy_Offer entity)
        {
            Attach(_context, entity);
            return base.Add(entity);
        }
        public override bool Update(buy_Offer entity)
        {
            Attach(_context, entity);
            return base.Update(entity);
        }
        public override async Task<IEnumerable<buy_Offer>> GetData(Expression<Func<buy_Offer, bool>> filter)
        {
            return await _context.Set<buy_Offer>()
                .Include(buy_Offer => buy_Offer.users)
                .Include(buy_Offer => buy_Offer.product)
                .Include(buy_Offer => buy_Offer.dic_Offer_status)
                .Include(buy_Offer => buy_Offer.product.dic_condition)
                .Include(buy_Offer => buy_Offer.product.dic_Product_type)
                .Include(buy_Offer => buy_Offer.product.dic_Genre)
                .Where(filter).ToListAsync();
        }

        public override async Task<buy_Offer> GetById(int id)
        {
            return await _context.Set<buy_Offer>()
                .Include(buy_Offer => buy_Offer.users)
                .Include(buy_Offer => buy_Offer.product)
                .Include(buy_Offer => buy_Offer.product.dic_Genre)
                .Include(buy_Offer => buy_Offer.product.dic_condition)
                .Include(buy_Offer => buy_Offer.product.dic_Product_type)
                .Include(buy_Offer => buy_Offer.dic_Offer_status)
                .FirstOrDefaultAsync(b => b.buyer_id == id);
        }

        public static void Attach(DbContext ctx, buy_Offer entity)
        {
            if(entity.users != null)
            {
                ctx.Entry(entity.users).State = EntityState.Modified;
            }
            if(entity.dic_Offer_status != null)
            {
                ctx.Entry(entity.dic_Offer_status).State = EntityState.Modified;
            }
            if(entity.product != null)
            {
                if(entity.product.ID != 0)
                {
                    ctx.Entry(entity.product).State = EntityState.Modified;
                }
               
                ProductRepository.Attach(ctx, entity.product);
            }
        }
    }
}
