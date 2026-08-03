using Lateral.Application.Dashboard;
using Microsoft.AspNetCore.Components;

namespace LateralApp.Components.Pages;

public partial class Dashboard
{
    [Inject] private IDashboardService DashboardService { get; set; } = default!;

    private DashboardStats? _stats;

    protected override async Task OnInitializedAsync()
    {
        _stats = await DashboardService.GetStatsAsync();
    }
}
