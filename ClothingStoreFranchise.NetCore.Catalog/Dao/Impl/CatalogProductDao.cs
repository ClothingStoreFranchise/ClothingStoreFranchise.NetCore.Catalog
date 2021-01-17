using ClothingStoreFranchise.NetCore.Catalog.Model;
using ClothingStoreFranchise.NetCore.Common.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Dao.Impl
{
    public class CatalogProductDao : BaseAbstractEntitiesDao<CatalogProduct, long, CatalogContext>, ICatalogProductDao
    {
        public CatalogProductDao(CatalogContext contextContainer) : base(contextContainer)
        {
        }

        public async Task<ICollection<CatalogProduct>> GetNovelties()
        {
            return await QueryTemplate().OrderByDescending(c => c.Id).Take(4).ToListAsync();
        }

        protected override IQueryable<CatalogProduct> QueryTemplate()
        {
            return base.QueryTemplate()
                .Include(o => o.Subcategory.CategoryBelonging);
        }
    }
}
