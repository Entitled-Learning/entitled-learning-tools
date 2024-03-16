// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using System.Runtime.Serialization;

namespace EntitledLearningTools.Models;

public enum DataTypes
{
    [EnumMember(Value = "students")]
    Students,

    [EnumMember(Value = "communityPartners")]
    CommunityPartners,

    [EnumMember(Value = "communityPartnerContacts")]
    CommunityPartnerContacts,

    [EnumMember(Value = "inventory")]
    Inventory
}

