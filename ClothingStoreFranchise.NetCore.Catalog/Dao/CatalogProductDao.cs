using ClothingStoreFranchise.NetCore.Catalog.Model;
using ClothingStoreFranchise.NetCore.Common.EntityFramework;

namespace ClothingStoreFranchise.NetCore.Catalog.Dao
{
    public class CatalogProductDao : BaseAbstractEntitiesDao<CatalogProduct, long, CatalogContext>, ICatalogProductDao
    {
        public CatalogProductDao(CatalogContext contextContainer) : base(contextContainer)
        {
        }
    }
}
