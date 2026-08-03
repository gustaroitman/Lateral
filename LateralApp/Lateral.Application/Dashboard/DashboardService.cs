using Lateral.Application.Products;

namespace Lateral.Application.Dashboard;

public class DashboardService(IProductService productService) : IDashboardService
{
    public async Task<DashboardStats> GetStatsAsync(CancellationToken cancellationToken = default)
    {
        var products = (await productService.GetAllAsync(cancellationToken)).ToList();

        return new DashboardStats(
            products.Count,
            products.Count(p => p.IsActive),
            products.Sum(p => p.Price * p.Quantity));
    }
}
