using System;

namespace MainService.Dtos.Item;

public class OwnedItemDto
{
     public Guid ItemId { get; set; }
    public string ItemName { get; set; }
    public int OwnedQuantity { get; set; }
    public int RequiredQuantity { get; set; }
}
