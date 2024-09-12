// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using System.Runtime.Serialization;

namespace EntitledLearning.Enrollment.UI.Models;

public enum StudentType
{
    [EnumMember(Value = "onPremises")]
    OnPremise,

    [EnumMember(Value = "elAcademy")]
    EntitledLearningAcademy
}

