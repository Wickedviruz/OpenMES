using OpenMES.Domain.Common;

namespace OpenMES.Domain.Production;

/// <summary>
/// Represents something that is manufactured.
/// Has a Routing that defines how it is produced.
/// </summary>
public class Product : AggregateRoot
{
    public Guid Id { get; private set; }
    public string ProductNumber { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public string UnitOfMeasure { get; private set; } = default!;
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Product() { }

    public static Result<Product> Create(string productNumber, string name, string unitOfMeasure, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(productNumber))
            return Result.Failure<Product>("Product number is required.");

        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Product>("Product name is required.");

        if (string.IsNullOrWhiteSpace(unitOfMeasure))
            return Result.Failure<Product>("Unit of measure is required.");

        var product = new Product
        {
            Id = Guid.NewGuid(),
            ProductNumber = productNumber.Trim().ToUpper(),
            Name = name.Trim(),
            Description = description?.Trim(),
            UnitOfMeasure = unitOfMeasure.Trim(),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        return Result.Success(product);
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}