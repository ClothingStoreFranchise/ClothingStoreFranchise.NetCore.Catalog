using ClothingStoreFranchise.NetCore.Catalog.Model;
using ClothingStoreFranchise.NetCore.Common.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Dao
{
    public interface ICategoryDao : IDao<Category, long>
    {
        Task<ICollection<Category>> FindAllParentCategories();
    }
}
