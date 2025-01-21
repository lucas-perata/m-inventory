using System;

namespace MainService.Dtos.Item;

public class InsufficientItemsDto
{
    public Guid ItemId { get; set; }
    public string ItemName { get; set; }
    public int OwnedQuantity { get; set; }
    public int RequiredQuantity { get; set; }
    public int MissingQuantity { get; set; }
}
