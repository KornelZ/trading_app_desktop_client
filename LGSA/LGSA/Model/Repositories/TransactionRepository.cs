using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.Repositories
{
    public class TransactionRepository : Repository<transactions>
    {
        public TransactionRepository(DbContext context) : base(context)
        {
        }
        public override transactions Add(transactions entity)
        {
            Attach(_context, entity);
            return base.Add(entity);
        }
        public override async Task<IEnumerable<transactions>> GetData(Expression<Func<transactions, bool>> filter)
        {
            return await _context.Set<transactions>()
                .Include(transactions => transactions.users)
                .Include(transactions => transactions.users1)
                .Include(transactions => transactions.dic_Transaction_status)
                .Include(transactions => transactions.buy_Offer)
                .Include(transactions => transactions.buy_Offer.product)
                .Include(transactions => transactions.buy_Offer.product.dic_condition)
                .Include(transactions => transactions.buy_Offer.product.dic_Genre)
                .Include(transactions => transactions.buy_Offer.product.dic_Product_type)
                .Include(transactions => transactions.sell_Offer)
                .Include(transactions => transactions.sell_Offer.product)
                .Include(transactions => transactions.sell_Offer.product.dic_condition)
                .Include(transactions => transactions.sell_Offer.product.dic_Genre)
                .Include(transactions => transactions.sell_Offer.product.dic_Product_type)
                .Where(filter).Take(100).ToListAsync();
        }

        public static void Attach(DbContext ctx, transactions entity)
        {
            if(entity.users != null)
            {
                if(entity.users.ID != 0)
                {
                    ctx.Set<users>().Attach(entity.users);
                }
            }
            if(entity.users1 != null)
            {
                if(entity.users1.ID != 0)
                {
                    ctx.Set<users>().Attach(entity.users1);
                }
            }
            if(entity.buy_Offer != null)
            {
                if(entity.buy_Offer.ID != 0)
                {
                    ctx.Set<buy_Offer>().Attach(entity.buy_Offer);
                    BuyOfferRepository.Attach(ctx, entity.buy_Offer);
                }
            }
            if(entity.sell_Offer != null)
            {
                if(entity.sell_Offer.ID != 0)
                {
                    ctx.Set<sell_Offer>().Attach(entity.sell_Offer);
                    SellOfferRepository.Attach(ctx, entity.sell_Offer);
                }
            }
            if(entity.dic_Transaction_status != null)
            {
                if(entity.dic_Transaction_status.ID != 0)
                {
                    ctx.Set<dic_Transaction_status>().Attach(entity.dic_Transaction_status);
                }
            }
        }
    }
}
