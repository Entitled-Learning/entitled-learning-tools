using EntitledLearningTools.Models;
using ELDataAccessLibrary.StorageContracts;
using Riok.Mapperly.Abstractions;

namespace EntitledLearningTools;

[Mapper]
public partial class DataMapper
{
    public partial Student CopyStudent(Student student);
    public partial Guardian CopyGuardian(Guardian guardian);
    public partial StudentStorageContractV1 ToStudentStorageContractV1(Student student);
    public partial Student ToStudent(StudentStorageContractV1 student);
    public partial GuardianStorageContractV1 ToGuardianStorageContractV1(Guardian guardian);
    public partial CommunityPartnerStorageContractV1 ToCommunityPartnerStorageContractV1(CommunityPartner communityPartner);
    public partial CommunityPartnerContactStorageContractV1 ToCommunityPartnerContactStorageContractV1(CommunityPartnerContact communityPartnerContact);
}