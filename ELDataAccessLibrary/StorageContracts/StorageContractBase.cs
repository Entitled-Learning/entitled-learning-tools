using System;

namespace ELDataAccessLibrary.StorageContracts;

public record StorageContractBase
{

    public DateTimeOffset CreatedOn { get; set; }

    public DateTimeOffset? UpdatedOn { get; set; }
}