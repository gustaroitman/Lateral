namespace Lateral.Application.Dashboard;

public interface IDashboardService
{
    Task<DashboardStats> GetStatsAsync(CancellationToken cancellationToken = default);
}
