// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using EntitledLearning.Tools.UI.Models;
using EntitledLearning.Data.StorageContracts;
using Riok.Mapperly.Abstractions;
using EntitledLearning.Data.Models;

namespace EntitledLearning.Tools.UI;

[Mapper]
public partial class DataMapper
{
    public partial Student Copy(Student student);
    public partial InventoryItem Copy(InventoryItem item);
    public partial Guardian Copy(Guardian guardian);
    public partial StudentStorageContractV1 ToStudentStorageContractV1(Student student);
    public partial Guardian ToGuardian(StudentGuardian guardian);
    public partial Student ToStudent(StudentStorageContractV1 student);
    public partial InventoryItem ToInventoryItem(InventoryItemStorageContractV1 item);
    public partial GuardianStorageContractV1 ToGuardianStorageContractV1(Guardian guardian);
    public partial CommunityPartnerStorageContractV1 ToCommunityPartnerStorageContractV1(CommunityPartner communityPartner);
    public partial CommunityPartnerContactStorageContractV1 ToCommunityPartnerContactStorageContractV1(CommunityPartnerContact communityPartnerContact);
    public partial InventoryItemStorageContractV1 ToInventoryItemStorageContractV1(InventoryItem item);
}