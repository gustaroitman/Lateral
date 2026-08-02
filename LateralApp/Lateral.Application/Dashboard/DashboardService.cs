using Lateral.Domain.Interfaces;

namespace Lateral.Application.Dashboard;

public class DashboardService(IProductRepository productRepository) : IDashboardService
{
    public async Task<DashboardStats> GetStatsAsync(CancellationToken cancellationToken = default)
    {
        var products = await productRepository.GetAllAsync(cancellationToken);
        var list = products.ToList();

        var total = list.Count;
        var active = list.Count(p => p.IsActive);
        var inventoryValue = list.Sum(p => p.Price * p.Quantity);

        return new DashboardStats(total, active, inventoryValue);
    }
}
