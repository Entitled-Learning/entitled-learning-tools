using System;
using System.Runtime.Serialization;

namespace EntitledLearningTools.Models;

public enum DataTypes
{
    [EnumMember(Value = "students")]
    Students,

    [EnumMember(Value = "communityPartners")]
    CommunityPartners,

    [EnumMember(Value = "communityPartnerContacts")]
    CommunityPartnerContacts
}

