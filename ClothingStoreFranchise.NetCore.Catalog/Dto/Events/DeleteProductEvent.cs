using ClothingStoreFranchise.NetCore.Common.Events.Impl;
using System.Collections.Generic;

namespace ClothingStoreFranchise.NetCore.Catalog.Dto.Events
{
    public class DeleteProductEvent : IntegrationEvent
    {
        public long Id { get; set; }

        public DeleteProductEvent(long id)
        {
            Id = id;
        }
    }
}
