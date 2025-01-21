using System;
using MainService.Entities.Enums;

namespace MainService.Dtos.Item;

public class ItemInventoryDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public ItemCategory Category { get; set; }

    public MeasurementUnit Unit { get; set; }

    public string ImageUrl { get; set; }

    public int DefaultThreshold { get; set; } = 1;
    public string SKU { get; set; }
    public int Quantity { get; set; }
}
