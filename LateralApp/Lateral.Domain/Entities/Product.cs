using System.ComponentModel.DataAnnotations;

namespace Lateral.Domain.Entities;

public class Product : BaseEntity
{
    public Product() { }

    public Product(Guid id) : base(id) { }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "999999")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    public bool IsActive { get; set; }
}
