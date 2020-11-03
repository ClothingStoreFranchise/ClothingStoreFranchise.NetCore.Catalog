using ClothingStoreFranchise.NetCore.Catalog.Model;
using ClothingStoreFranchise.NetCore.Common.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Dao.Impl
{
    public class CategoryDao : BaseAbstractEntitiesDao<Category, long, CatalogContext>, ICategoryDao
    {
        public CategoryDao(CatalogContext contextContainer) : base(contextContainer)
        {
        }

        public async Task<ICollection<Category>> FindAllParentCategories()
        {
            return await base.FindWhereAsync(catetegory => catetegory.CategoryBelonging == null);
        }

        protected override IQueryable<Category> QueryTemplate()
        {
            return base.QueryTemplate()
                .Include(o => o.CatalogProducts);
        }
    }
}
