using ClothingStoreFranchise.NetCore.Catalog.Model;
using ClothingStoreFranchise.NetCore.Common.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ClothingStoreFranchise.NetCore.Catalog.Dao.Impl
{
    public class CatalogProductDao : BaseAbstractEntitiesDao<CatalogProduct, long, CatalogContext>, ICatalogProductDao
    {
        public CatalogProductDao(CatalogContext contextContainer) : base(contextContainer)
        {
        }

        /*protected override IQueryable<CatalogProduct> QueryTemplate()
        {
            return base.QueryTemplate()
                .Include(o => o.Subcategory);
        }*/
    }
}
