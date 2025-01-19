using System;
using System.ComponentModel.DataAnnotations;
using MainService.Entities.Enums;

namespace api.Endpoints.Items.Update;

public class RequestDto
{
    [Required]
    public Guid Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    public string Description { get; set; }
    public ItemCategory Category { get; set; }
    public MeasurementUnit Unit { get; set; }
    public string ImageUrl { get; set; }
    public string SKU { get; set; }
}
