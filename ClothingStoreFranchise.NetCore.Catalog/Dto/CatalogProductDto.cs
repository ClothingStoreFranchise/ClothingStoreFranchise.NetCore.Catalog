using ClothingStoreFranchise.NetCore.Common.Events.Impl;
using ClothingStoreFranchise.NetCore.Common.Types;
using System.Collections.Generic;

namespace ClothingStoreFranchise.NetCore.Catalog.Dto
{
    public class CatalogProductDto : IntegrationEvent, IEntityDto<long>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Stock { get; set; }

        public decimal UnitPrice { get; set; }

        public string PictureUrl { get; set; }

        public OfferDto CurrentOffer { get; set; }

        public ICollection<OfferDto> Offers { get; set; }

        //public override string ExtensibleEntityName => typeof(CatalogProduct).Name;

        public long Key() => Id;
    }
}
