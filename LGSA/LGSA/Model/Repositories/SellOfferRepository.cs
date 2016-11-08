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

        public override async Task<IEnumerable<sell_Offer>> GetData(Expression<Func<sell_Offer, bool>> filter)
        {
            return await _context.Set<sell_Offer>()
                .Include(sell_Offer => sell_Offer.users)
                .Include(sell_Offer => sell_Offer.product)
                .Include(sell_Offer => sell_Offer.dic_Offer_status)
                .Include(sell_Offer => sell_Offer.product.dic_condition)
                .Include(sell_Offer => sell_Offer.product.dic_Product_type)
                .Include(sell_Offer => sell_Offer.product.dic_Genre)
                .Where(filter).ToListAsync();
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
    }
}
