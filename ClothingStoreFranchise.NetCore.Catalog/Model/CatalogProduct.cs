using ClothingStoreFranchise.NetCore.Catalog.Dto;
using ClothingStoreFranchise.NetCore.Common.Extensible;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Model
{
    public class CatalogProduct : ExtensibleEntity<long>
    {
        public string Name { get; set; }

        public int Stock { get; set; }

        public decimal UnitPrice { get; set; }

        public string PictureUrl { get; set; }

        public Offer CurrentOffer { get; set; }

        public ICollection<Offer> Offers { get; set; }

        public long? CatalogProductId { get; set; }

        public override long GetAppId()
        {
            throw new NotImplementedException();
        }
    }
}
