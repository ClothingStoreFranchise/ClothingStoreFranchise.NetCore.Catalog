using ClothingStoreFranchise.NetCore.Common.Constants;
using ClothingStoreFranchise.NetCore.Common.Events.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Dto.Events
{
    public class UpdateProductEvent : IntegrationEvent
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public string PictureUrl { get; set; }

        public ClothingSizeType ClothingSizeType { get; set; }

        public int Ab { get; set; }
    }
}
