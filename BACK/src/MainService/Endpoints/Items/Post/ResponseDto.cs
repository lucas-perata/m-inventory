using System;
using System.ComponentModel.DataAnnotations;
using MainService.Entities.Enums;

namespace MainService.Endpoints.Items;

public class ResponseDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public ItemCategory Category { get; set; }

    public MeasurementUnit Unit { get; set; }

    public string ImageUrl { get; set; }

    public int DefaultThreshold { get; set; }
    public string SKU { get; set; }
}
