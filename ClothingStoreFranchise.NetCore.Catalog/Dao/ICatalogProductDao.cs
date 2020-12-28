using ClothingStoreFranchise.NetCore.Catalog.Model;
using ClothingStoreFranchise.NetCore.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Dao
{
    public interface ICatalogProductDao : IDao<CatalogProduct, long>
    {
        Task<ICollection<CatalogProduct>> GetNovelties();
    }
}
