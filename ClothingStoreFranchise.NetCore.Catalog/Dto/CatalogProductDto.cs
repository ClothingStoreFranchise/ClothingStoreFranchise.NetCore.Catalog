using ClothingStoreFranchise.NetCore.Catalog.Model;
using ClothingStoreFranchise.NetCore.Common.Events.Impl;
using ClothingStoreFranchise.NetCore.Common.Types;
using System.Collections.Generic;

namespace ClothingStoreFranchise.NetCore.Catalog.Dto
{
    public class CatalogProductDto : IEntityDto<long>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public string PictureUrl { get; set; }

        public OfferDto CurrentOffer { get; set; }

        public ICollection<OfferDto> Offers { get; set; }

        public long SubcategoryId { get; set; }

        public string SubcategoryName { get; set; }

        public string CategoryName { get; set; }

        public long Key() => Id;
    }
}
