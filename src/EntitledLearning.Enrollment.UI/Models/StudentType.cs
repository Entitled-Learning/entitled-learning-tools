// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using System.Runtime.Serialization;

namespace EntitledLearning.Tools.UI.Models;

public enum StudentType
{
    [EnumMember(Value = "onPremises")]
    OnPremise,

    [EnumMember(Value = "elAcademy")]
    EntitledLearningAcademy
}

