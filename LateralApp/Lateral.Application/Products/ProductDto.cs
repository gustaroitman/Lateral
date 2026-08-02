using System.ComponentModel.DataAnnotations;

namespace Lateral.Application.Products;

public class ProductDto
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, 999999, ErrorMessage = "Price must be between 0.01 and 999,999.")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
    public int Quantity { get; set; }

    public bool IsActive { get; set; }
}
