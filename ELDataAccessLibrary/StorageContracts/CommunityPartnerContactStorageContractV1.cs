using System;

namespace ELDataAccessLibrary.StorageContracts;

public record CommunityPartnerContactStorageContractV1 : PersonBaseStorageContractV1
{
    public string? CommunityPartnerName { get; set; }
}

