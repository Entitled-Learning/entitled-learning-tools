using System;
using System.Runtime.Serialization;

namespace EntitledLearningTools.Models;

public enum StudentType
{
    [EnumMember(Value = "onPremises")]
    OnPremise,

    [EnumMember(Value = "elAcademy")]
    EntitledLearningAcademy
}

