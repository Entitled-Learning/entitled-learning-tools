using EntitledLearningTools.Models;
using ELDataAccessLibrary.StorageContracts;
using Riok.Mapperly.Abstractions;

namespace EntitledLearningTools;

[Mapper(ThrowOnMappingNullMismatch = false)]
public partial class DataMapper
{
    public partial StudentStorageContractV1 ToStudentStorageContractV1(Student student);
    public partial GuardianStorageContractV1 ToGuardianStorageContractV1(Guardian guardian);
    public partial CommunityPartnerStorageContractV1 ToCommunityPartnerStorageContractV1(CommunityPartner communityPartner);
    public partial CommunityPartnerContactStorageContractV1 ToCommunityPartnerContactStorageContractV1(CommunityPartnerContact communityPartnerContact);
}