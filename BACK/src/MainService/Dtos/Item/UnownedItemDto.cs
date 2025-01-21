using System;

namespace MainService.Dtos.Item;

public class UnownedItemDto
{
    public Guid ProjectItemId { get; set; }
    public string ItemName { get; set; }
    public int RequiredQuantity { get; set; }
}
