using ClothingStoreFranchise.NetCore.Common.Constants;
using ClothingStoreFranchise.NetCore.Common.Events.Impl;
using ClothingStoreFranchise.NetCore.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Dto.Events
{
    public class CreateProductEvent : IntegrationEvent, IEntityDto<long>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public string PictureUrl { get; set; }

        public ClothingSizeType ClothingSizeType { get; set; }

        public long Key()
        {
            return Id;
        }
    }
}
