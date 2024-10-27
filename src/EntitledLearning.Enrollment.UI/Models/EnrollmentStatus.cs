// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using System.Runtime.Serialization;

namespace EntitledLearning.Enrollment.UI.Models;

public enum EnrollmentStatus
{
    [EnumMember(Value = "pending")]
    Pending,

    [EnumMember(Value = "active")]
    Active,

    [EnumMember(Value = "completed")]
    Completed,

    [EnumMember(Value = "dropped")]
    Dropped,
}

