
namespace ELDataAccessLibrary.StorageContracts;

public record CommunityPartnerStorageContractV1 : ContractBase
{
    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }
    
    public string? EmailAddress { get; set; }
    
    public string? AddressLine1 { get; set; }
    
    public string? AddressLine2 { get; set; }
    
    public string? City { get; set; }
    
    public string? State { get; set; }
    
    public string? ZipCode { get; set; }

    public string? ContractVersion { get; } = "CommunityPartnerStorageContractV1";
}
