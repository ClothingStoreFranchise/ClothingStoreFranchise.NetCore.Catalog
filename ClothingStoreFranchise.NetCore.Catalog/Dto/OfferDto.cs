using ClothingStoreFranchise.NetCore.Catalog.Model;
using ClothingStoreFranchise.NetCore.Common.Extensible;
using ClothingStoreFranchise.NetCore.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Dto
{
    public class OfferDto : BaseExtensibleEntityDto, IEntityDto<long>
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Discount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public long? CategoryId { get; set; }

        public CatalogProduct CatalogProductId { get; set; }

        public long Key() => Id;

        public override string ExtensibleEntityName => typeof(Offer).Name;
    }
}
