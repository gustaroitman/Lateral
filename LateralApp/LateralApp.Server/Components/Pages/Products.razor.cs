using Lateral.Application.Products;
using Lateral.Domain.Exceptions;
using LateralApp.Components.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace LateralApp.Components.Pages;

public partial class Products
{
    [Inject] private IProductService ProductService { get; set; } = default!;
    [Inject] private ToastService ToastService { get; set; } = default!;
    [Inject] private ILogger<Products> Logger { get; set; } = default!;

    private const int PageSize = 10;

    private List<ProductDto>? _products;
    private ProductDto _formDto = new();
    private ProductDto? _productToDelete;
    private Virtualize<ProductDto>? _virtualizeRef;

    private bool _showFormModal;
    private bool _isSaving;
    private string? _errorMessage;

    private string _searchText = string.Empty;
    private bool _showActiveOnly = true;
    private int _currentPage = 1;

    private IEnumerable<ProductDto> FilteredProducts =>
        _products?
            .Where(p => string.IsNullOrWhiteSpace(_searchText) ||
                        p.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase))
            .Where(p => !_showActiveOnly || p.IsActive)
        ?? [];

    private int _filteredCount => FilteredProducts.Count();
    private int _totalPages => Math.Max(1, (int)Math.Ceiling(_filteredCount / (double)PageSize));

    private ValueTask<ItemsProviderResult<ProductDto>> ProvideProductsAsync(ItemsProviderRequest request)
    {
        var paged = FilteredProducts
            .Skip((_currentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        var result = new ItemsProviderResult<ProductDto>(
            paged.Skip(request.StartIndex).Take(request.Count),
            paged.Count);

        return ValueTask.FromResult(result);
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadProductsAsync();
    }


    private async Task LoadProductsAsync()
    {
        var result = await ProductService.GetAllAsync();
        _products = result.ToList();
        Logger.LogInformation("Loaded {Count} products.", _products.Count);
    }

    private async Task OnSearchChanged(string? value)
    {
        _searchText = value ?? string.Empty;
        await ResetPageAsync();
    }

    private async Task OnActiveFilterChanged(bool value)
    {
        _showActiveOnly = value;
        await ResetPageAsync();
    }

    private async Task GoToPageAsync(int page)
    {
        _currentPage = Math.Clamp(page, 1, _totalPages);
        if (_virtualizeRef is not null)
            await _virtualizeRef.RefreshDataAsync();
    }

    private async Task ResetPageAsync()
    {
        _currentPage = 1;
        if (_virtualizeRef is not null)
            await _virtualizeRef.RefreshDataAsync();
    }

    private IEnumerable<int> GetPageNumbers()
    {
        const int maxVisible = 5;
        int half = maxVisible / 2;
        int start = Math.Max(1, _currentPage - half);
        int end = Math.Min(_totalPages, start + maxVisible - 1);
        start = Math.Max(1, end - maxVisible + 1);
        return Enumerable.Range(start, end - start + 1);
    }

    private void OpenAddModal()
    {
        _formDto = new ProductDto { IsActive = true };
        _showFormModal = true;
        _errorMessage = null;
    }

    private void OpenEditModal(ProductDto product)
    {
        _formDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity,
            IsActive = product.IsActive
        };
        _showFormModal = true;
        _errorMessage = null;
    }

    private void CloseFormModal() => _showFormModal = false;

    private async Task SaveProductAsync()
    {
        _isSaving = true;
        _errorMessage = null;
        bool isNew = _formDto.Id == Guid.Empty;
        try
        {
            if (isNew)
                await ProductService.AddAsync(_formDto);
            else
                await ProductService.UpdateAsync(_formDto);

            Logger.LogInformation(
                isNew ? "Product '{Name}' created successfully."
                       : "Product '{Name}' (Id: {Id}) updated successfully.",
                _formDto.Name, _formDto.Id);

            _showFormModal = false;
            await LoadProductsAsync();
            if (_virtualizeRef is not null)
                await _virtualizeRef.RefreshDataAsync();
            ToastService.Show(isNew ? "Product created successfully." : "Product updated successfully.");
        }
        catch (RepositoryException ex)
        {
            Logger.LogWarning(ex, "Failed to save product '{Name}'.", _formDto.Name);
            _errorMessage = ex.Message;
        }
        finally
        {
            _isSaving = false;
        }
    }

    private async Task SetActiveAsync(ProductDto product, bool isActive)
    {
        _isSaving = true;
        _errorMessage = null;
        try
        {
            var dto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                IsActive = isActive
            };
            await ProductService.UpdateAsync(dto);
            Logger.LogInformation(
                "Product '{Name}' (Id: {Id}) {Action}.",
                product.Name, product.Id, isActive ? "activated" : "deactivated");
            await LoadProductsAsync();
            if (_virtualizeRef is not null)
                await _virtualizeRef.RefreshDataAsync();
            ToastService.Show(isActive ? "Product activated." : "Product deactivated.");
        }
        catch (RepositoryException ex)
        {
            Logger.LogWarning(ex, "Failed to change active state for product '{Name}' (Id: {Id}).", product.Name, product.Id);
            _errorMessage = ex.Message;
        }
        finally
        {
            _isSaving = false;
        }
    }

    private void OpenDeleteConfirm(ProductDto product)
    {
        _productToDelete = product;
        _errorMessage = null;
    }

    private void CloseDeleteConfirm() => _productToDelete = null;

    private async Task ConfirmDeleteAsync()
    {
        if (_productToDelete is null) return;
        _isSaving = true;
        try
        {
            await ProductService.DeleteAsync(_productToDelete.Id);
            Logger.LogInformation("Product '{Name}' (Id: {Id}) deleted.", _productToDelete.Name, _productToDelete.Id);
            _productToDelete = null;
            await LoadProductsAsync();
            if (_virtualizeRef is not null)
                await _virtualizeRef.RefreshDataAsync();
            ToastService.Show("Product deleted.");
        }
        catch (RepositoryException ex)
        {
            Logger.LogWarning(ex, "Failed to delete product '{Name}' (Id: {Id}).", _productToDelete?.Name, _productToDelete?.Id);
            _errorMessage = ex.Message;
            _productToDelete = null;
        }
        finally
        {
            _isSaving = false;
        }
    }
}
