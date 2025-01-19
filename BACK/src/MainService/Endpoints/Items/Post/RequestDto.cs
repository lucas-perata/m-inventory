using System;
using System.ComponentModel.DataAnnotations;
using MainService.Entities.Enums;

namespace MainService.Endpoints.Items;

public class RequestDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public ItemCategory Category { get; set; }

    [Required]
    public MeasurementUnit Unit { get; set; }

    public string ImageUrl { get; set; }
    public int DefaultThreshold { get; set; } = 1;
    public string SKU { get; set; }
}
