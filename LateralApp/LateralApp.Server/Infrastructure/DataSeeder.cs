using System.Text.Json;
using Lateral.Application.Products;
using Lateral.Domain.Exceptions;

namespace LateralApp.Server.Infrastructure;

public static class DataSeeder
{
    private sealed record SeedItem(string Name, decimal Price, int Quantity, bool IsActive);

    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public static async Task SeedProductsAsync(IServiceProvider services, string webRootPath)
    {
        var seedPath = Path.Combine(webRootPath, "data", "products-seed.json");
        if (!File.Exists(seedPath))
            return;

        var json = await File.ReadAllTextAsync(seedPath);
        var items = JsonSerializer.Deserialize<List<SeedItem>>(json, JsonOptions);

        if (items is null || items.Count == 0)
            return;

        using var scope = services.CreateScope();
        var productService = scope.ServiceProvider.GetRequiredService<IProductService>();

        var existing = (await productService.GetAllAsync()).Select(p => p.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (var item in items.Where(i => !existing.Contains(i.Name)))
        {
            try
            {
                await productService.AddAsync(new ProductDto
                {
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    IsActive = item.IsActive
                });
            }
            catch (RepositoryException)
            {
                throw;
            }
        }
    }
}
