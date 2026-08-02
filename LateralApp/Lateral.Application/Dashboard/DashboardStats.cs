namespace Lateral.Application.Dashboard;

public record DashboardStats(
    int TotalProducts,
    int ActiveProducts,
    decimal InventoryValue);
