using System;

namespace ELDataAccessLibrary.StorageContracts;

public record StorageContractBase : GeneratedId
{

    public DateTimeOffset CreatedOn { get; set; }

    public DateTimeOffset? UpdatedOn { get; set; }
}