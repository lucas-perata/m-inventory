using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BACK.Entities.Enums;

namespace BACK.Entities;

[Table("Items")]
public class Item
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public ItemCategory Category { get; set; }

    [Required]
    public MeasurementUnit Unit { get; set; }

    public string ImageUrl { get; set; }

    [Required]
    public int DefaultThreshold { get; set; } = 1;
    public string SKU { get; set; }

    // NAV 
    public ICollection<ProjectItem> ProjectItems { get; set; } = new List<ProjectItem>();
}
