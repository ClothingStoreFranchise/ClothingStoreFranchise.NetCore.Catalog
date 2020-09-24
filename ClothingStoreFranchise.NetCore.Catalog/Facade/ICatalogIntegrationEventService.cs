using ClothingStoreFranchise.NetCore.Common.Events.Impl;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Facade.Impl
{
    public interface ICatalogIntegrationEventService
    {
        Task SaveEventAndCatalogContextChangesAsync(IntegrationEvent evt);
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
