// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace EntitledLearning.Enrollment.UI.Models;

public class InventoryItem
{
    public string? Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Cost { get; set; }

    public string? PhysicalLocation { get; set; }

    public DateTimeOffset? ExpirationDate { get; set; }

    public string? Sku { get; set; }

    public int? Quantity { get; set; }
}