using ClothingStoreFranchise.NetCore.Common.Events.Impl;
using ClothingStoreFranchise.NetCore.Common.Types;
using System.Collections.Generic;

namespace ClothingStoreFranchise.NetCore.Catalog.Dto.Events
{
    public class DeleteProductEvent : IntegrationEvent, IEntityDto<long>
    {
        public long Id { get; set; }

        public DeleteProductEvent(long id)
        {
            Id = id;
        }

        public long Key()
        {
            return Id;
        }
    }
}
