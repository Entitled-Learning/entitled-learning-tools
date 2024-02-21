using System;

namespace ELDataAccessLibrary.StorageContracts;

public record InventoryItemStorageContractV1 : StorageContractBase
{
    public Guid? Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Cost { get; set; }
    
    public string? PhysicalLocation { get; set; }

    public DateTimeOffset? ExpirationDate { get; set; }

    public string? Sku { get; set; }

    public int? Quantity { get; set; }

    public string? ContractVersion { get; } = nameof(InventoryItemStorageContractV1);
}

